using System;
using Berkay._Scripts.PlayerGroup;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerSpawner.players.Add(this);
    }

    private void TryMoveMiddle()
    {
        
    }

   
}