using System;
using DG.Tweening;
using Emre;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Tween moveTween;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            moveTween?.Kill();
            PlayerSpawner.players.Remove(this);
            Destroy(gameObject);
            GameEvents.RaisePlayerDied(this);
        }
    }


    public void DoLocalMove(Vector3 position)
    {
        moveTween?.Kill();
        moveTween = transform.DOLocalMove(position, 0.5f);
        
        moveTween
            .SetEase(Ease.OutBack);
    }
}