using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerGroupState
{
    Waiting,
    Walking,
    Fighting
}

public class PlayerGroupMover : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float horizontalSpeed;
    
    [Header("References")]
    [SerializeField] private FloatingJoystick joystick;

    private float horizontalInput;


    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (PlayerGroupController.PlayerGroupState == PlayerGroupState.Walking)
        {
            MoveForward();
            MoveHorizontal();
        }
    }
    
    private void MoveForward()
    {
        transform.Translate( Vector3.forward * verticalSpeed * Time.deltaTime);
    }

    private void MoveHorizontal()
    {
        horizontalInput = joystick.Horizontal;
        
        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime);
    }
}
