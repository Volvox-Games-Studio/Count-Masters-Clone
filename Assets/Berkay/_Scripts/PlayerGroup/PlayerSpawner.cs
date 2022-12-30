using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Emre;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerController playerPrefab;
        [SerializeField] private float distanceFactor;
        [SerializeField] private float radius;
        public static List<PlayerController> players = new List<PlayerController>();
        private int oldPlayerCount;
        private int initialPlayerCount = 1;

        
        private void Start()
        {
            oldPlayerCount = PlayerGroupController.GroupCount;

            for (int i = 0; i < initialPlayerCount; i++)
            {
                SpawnNewPlayer();
            }
        }

        private void OnEnable()
        {
            GameEvents.OnDoorDashed += InstantiatePlayers;
            GameEvents.OnPlayerDied += OnPlayerDied;
        }

        private void OnDisable()
        {
            GameEvents.OnDoorDashed -= InstantiatePlayers;
            GameEvents.OnPlayerDied -= OnPlayerDied;
        }


        private void OnPlayerDied(GameEventResponse response)
        {
            FormatPlayers();
        }
        
        
        private void InstantiatePlayers(GameEventResponse gameEventResponse)
        {
            if (gameEventResponse.gateOperator == GateOperator.Add)
            {
                for (int i = 0; i < gameEventResponse.gateValue; i++)
                {
                    SpawnNewPlayer();
                }
            }
            else
            {
                for (int i = 0; i < oldPlayerCount * gameEventResponse.gateValue - oldPlayerCount ; i++)
                {
                    SpawnNewPlayer();
                }
            }

            oldPlayerCount = PlayerGroupController.GroupCount;
            FormatPlayers();
        }

        private void SpawnNewPlayer()
        {
            var spawnPos = transform.position;
            var newPlayer = Instantiate(playerPrefab, spawnPos, Quaternion.identity, transform);
            players.Add(newPlayer);
        }
        
        private void FormatPlayers()
        {
            for (int i = 0; i < players.Count; i++)
            {
                var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
                var z = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * radius);
            
                var position = new Vector3(x, 0f, z);
                var player = players[i];

                player.DoLocalMove(position);
            }
        }
    }
