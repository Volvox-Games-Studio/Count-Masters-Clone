using System;
using System.Collections;
using System.Collections.Generic;
using Emre;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerController playerPrefab;
        public static List<PlayerController> players = new List<PlayerController>();
        private int oldPlayerCount;

        private void Start()
        {
            oldPlayerCount = PlayerGroupController.GroupCount;
        }

        private void OnEnable()
        {
            GameEvents.OnDoorDashed += InstantiatePlayers;
        }

        private void OnDisable()
        {
            GameEvents.OnDoorDashed -= InstantiatePlayers;
        }

        private void InstantiatePlayers(GameEventResponse gameEventResponse)
        {
            if (gameEventResponse.gateOperator == GateOperator.Add)
            {
                for (int i = 0; i < gameEventResponse.gateValue; i++)
                {
                    var randX = Random.Range(-5, 5);
                    var randZ = Random.Range(-2, 2);
                    var randVec = new Vector3(randX, 0, randZ);
                    var spawnPos = players[0].transform.position + randVec;
                    Instantiate(playerPrefab, spawnPos, Quaternion.identity, transform);
                }
            }
            else
            {
                for (int i = 0; i < oldPlayerCount * gameEventResponse.gateValue - oldPlayerCount ; i++)
                {
                    var randX = Random.Range(-5, 5);
                    var randZ = Random.Range(-2, 2);
                    var randVec = new Vector3(randX, 0, randZ);
                    var spawnPos = players[0].transform.position + randVec;
                    Instantiate(playerPrefab, spawnPos, Quaternion.identity, transform);
                }
            }

            oldPlayerCount = PlayerGroupController.GroupCount;
        }
    }
