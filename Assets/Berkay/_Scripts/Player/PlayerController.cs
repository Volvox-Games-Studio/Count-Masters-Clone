using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerSpawner.players.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            PlayerSpawner.players.Remove(this);
            Destroy(gameObject);
        }
    }
}