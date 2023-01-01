using System.Collections;
using System.Collections.Generic;
using Emre;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
        private const int MaxSpawnCountPerFrame = 50;
        private const float XMax = 7.5f;
        
        
        [SerializeField] private PlayerController playerPrefab;
        [SerializeField] private float distanceFactor;
        [SerializeField] private float radiusFactor;
        [SerializeField] private float dieFormatDelay;
        public static List<PlayerController> players = new List<PlayerController>();
        private float lastPlayerDiedTime;
        private bool formatedAfterPlayerDied = true;


        public static float LeftBorder
        {
            get
            {
                if (Size <= 0) return -XMax;
                
                var record = players[0].LocalPosition.x;

                for (int i = 1; i < Size; i++)
                {
                    var x = players[i].LocalPosition.x;
                    
                    if (x > record) continue;

                    record = x;
                }

                return -XMax - record;
            }
        }
        
        public static float RightBorder
        {
            get
            {
                if (Size <= 0) return XMax;
                
                var record = players[0].LocalPosition.x;

                for (int i = 1; i < Size; i++)
                {
                    var x = players[i].LocalPosition.x;
                    
                    if (x < record) continue;

                    record = x;
                }

                return XMax - record;
            }
        }
        

        private static float Radius => GroupUtils.IndexToLocalPosition(Size - 1).magnitude;
        private static int Size => players.Count;
        

        private void Awake()
        {
            GameEvents.OnDoorDashed += OnDoorDashed;
            GameEvents.OnPlayerDied += OnPlayerDied;
            GameEvents.OnStartUnitsUpgraded += OnStartUnitsUpgraded;
            GameEvents.OnBattleEnd += OnBattleEnd;
        }

        private void OnDestroy()
        {
            GameEvents.OnDoorDashed -= OnDoorDashed;
            GameEvents.OnPlayerDied -= OnPlayerDied;
            GameEvents.OnStartUnitsUpgraded -= OnStartUnitsUpgraded;
            GameEvents.OnBattleEnd -= OnBattleEnd;
        }
        
        private void Update()
        {
            TryFormatAfterPlayerDied();
        }


        public static void Remove(PlayerController player)
        {
            players.Remove(player);

            if (Size <= 0)
            {
                if (PlayerGroupController.PlayerGroupState == PlayerGroupState.Fighting)
                {
                    GameEvents.RaiseBattleEnd(false);
                }

                else
                {
                    GameEvents.RaiseGameOver(GameOverReason.NoPlayerLeft);
                }
            }
            
            GameEvents.RaisePlayerGroupSizeChanged(Size, Radius);
        }
        
        
        private void OnStartUnitsUpgraded(GameEventResponse response)
        {
            var spawnCount = response.startUnits - Size;
            
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnNewPlayer();
            }
            
            FormatPlayers(false);
            GameEvents.RaisePlayerGroupSizeChanged(Size, Radius);
        }
        
        private void OnPlayerDied(GameEventResponse response)
        {
            lastPlayerDiedTime = Time.time;
            formatedAfterPlayerDied = false;
        }

        private void OnDoorDashed(GameEventResponse gameEventResponse)
        {
            UpdatePlayerCount(gameEventResponse.gateOperator, gameEventResponse.gateValue);
        }

        private void OnBattleEnd(GameEventResponse response)
        {
            if (response.isVictory)
            {
                FormatPlayers(false);
                return;
            }
            
            GameEvents.RaiseGameOver(GameOverReason.LosingBattle);
        }


        private bool TryFormatAfterPlayerDied()
        {
            var timeElapsedAfterLastPlayerDied = Time.time - lastPlayerDiedTime;

            if (!formatedAfterPlayerDied && timeElapsedAfterLastPlayerDied > dieFormatDelay)
            {
                FormatPlayers(true);
                formatedAfterPlayerDied = true;
                GameEvents.RaisePlayerGroupSizeChanged(Size, Radius);
                return true;
            }

            return false;
        }
        
        private void UpdatePlayerCount(GateOperator gateOperator, int gateValue)
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                var oldPlayerCount = players.Count;
                var newPlayerCount = gateOperator switch
                {
                    GateOperator.Add => oldPlayerCount + gateValue,
                    GateOperator.Mult => oldPlayerCount * gateValue
                };
            
                var deltaPlayerCount = newPlayerCount - oldPlayerCount;
            
                for (int i = 0; i < deltaPlayerCount; i++)
                {
                    SpawnNewPlayer();

                    if (i % MaxSpawnCountPerFrame == 0) yield return null;
                }
                
                FormatPlayers(false);
                GameEvents.RaisePlayerGroupSizeChanged(newPlayerCount, Radius);
            }
        }
        
        private void SpawnNewPlayer()
        {
            var spawnPos = transform.position;
            var newPlayer = Instantiate(playerPrefab, spawnPos, Quaternion.identity, transform);
            players.Add(newPlayer);
        }
        
        private void FormatPlayers(bool afterDied)
        {
            for (int i = 0; i < Size; i++)
            {
                var player = players[i];
                var localPosition = GroupUtils.IndexToLocalPosition(i);
                player.Format(localPosition, afterDied);
            }
        }
}
