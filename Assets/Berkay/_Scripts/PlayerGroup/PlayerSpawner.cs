using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Emre;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpawner : MonoBehaviour
{
        private const int MaxSpawnCountPerFrame = 50;
        
        
        [SerializeField] private PlayerController playerPrefab;
        [SerializeField] private float distanceFactor;
        [SerializeField] private float radius;
        [SerializeField] private float dieFormatDelay;
        public static List<PlayerController> players = new List<PlayerController>();
        private int oldPlayerCount;
        private int initialPlayerCount = 1;
        private float lastPlayerDiedTime;
        private bool formatedAfterPlayerDied = true;

        
        private void Start()
        {
            for (int i = 0; i < initialPlayerCount; i++)
            {
                SpawnNewPlayer();
            }
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
                
                GameEvents.RaisePlayerGroupExpanded(newPlayerCount);
                FormatPlayers(false);
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
            for (int i = 0; i < players.Count; i++)
            {
                var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
                var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);
            
                var position = new Vector3(x, 0f, z);
                var player = players[i];

                player.Format(position, afterDied);
            }
        }
    }
