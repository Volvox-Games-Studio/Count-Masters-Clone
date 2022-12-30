using System;
using Emre;
using UnityEngine;


public class PlayerGroupController : MonoBehaviour
{ 
    public static PlayerGroupState PlayerGroupState;
    public static int GroupCount;
    
    private void Start()
    {
        PlayerGroupState = PlayerGroupState.Waiting;
    }

    public void StartGame() //START GAME BUTTON'DAN CAGİRİLACAK ARKADAS
    {
        PlayerGroupState = PlayerGroupState.Walking;
    }

    private void OnDoorDeshed(GameEventResponse gameEventResponse)
    {
        if (gameEventResponse.gateOperator == GateOperator.Mult)
        {
            GroupCount *= gameEventResponse.gateValue;
        }
        else
        {
            GroupCount += gameEventResponse.gateValue;
        }
    }

    private void OnEnable()
    {
        GameEvents.OnDoorDashed += OnDoorDeshed;
    }

    private void OnDisable()
    {
        GameEvents.OnDoorDashed -= OnDoorDeshed;
    }

    /*private void Update() //TEST CODE
    {
        GroupCount = PlayerSpawner.players.Count;
        Debug.Log(GroupCount);
    }*/
}
