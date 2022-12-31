using System;
using Emre;
using UnityEngine;


public class PlayerGroupController : MonoBehaviour
{ 
    public static PlayerGroupState PlayerGroupState;

    
    private void Start()
    {
        PlayerGroupState = PlayerGroupState.Waiting;
    }

    public void StartGame() //START GAME BUTTON'DAN CAGİRİLACAK ARKADAS
    {
        PlayerGroupState = PlayerGroupState.Walking;
    }
}
