using System;
using System.Collections;
using System.Collections.Generic;
using Emre;
using UnityEngine;

namespace Berkay._Scripts.PlayerGroup
{
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
                     Instantiate(playerPrefab, transform.position, Quaternion.identity, transform);
                }
            }
            else
            {
                for (int i = 0; i < oldPlayerCount * gameEventResponse.gateValue - oldPlayerCount ; i++)
                {
                    Instantiate(playerPrefab, transform.position, Quaternion.identity, transform);
                }
            }

            oldPlayerCount = PlayerGroupController.GroupCount;
        }
    }
}