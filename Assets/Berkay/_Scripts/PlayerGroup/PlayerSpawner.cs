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
        private int oldPlayerCount;
        private int initialPlayerCount = 1;
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
        

        private float Radius => IndexToLocalPosition(Size - 1).magnitude;
        private static int Size => players.Count;
        
        
        private void Start()
        {
            for (int i = 0; i < initialPlayerCount; i++)
            {
                SpawnNewPlayer();
            }
            
            GameEvents.RaisePlayerGroupSizeChanged(Size, Radius);
        }

        private void Update()
        {
            TryFormatAfterPlayerDied();
        }

        private void OnEnable()
        {
            GameEvents.OnDoorDashed += OnDoorDashed;
            GameEvents.OnPlayerDied += OnPlayerDied;
        }

        private void OnDisable()
        {
            GameEvents.OnDoorDashed -= OnDoorDashed;
            GameEvents.OnPlayerDied -= OnPlayerDied;
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
                var localPosition = IndexToLocalPosition(i);
                player.Format(localPosition, afterDied);
            }
        }

        private Vector3 IndexToLocalPosition(int index)
        {
            var x = distanceFactor * Mathf.Sqrt(index) * Mathf.Cos(index * radiusFactor);
            var z = distanceFactor * Mathf.Sqrt(index) * Mathf.Sin(index * radiusFactor);
            return new Vector3(x, 0f, z);
        }
    }
