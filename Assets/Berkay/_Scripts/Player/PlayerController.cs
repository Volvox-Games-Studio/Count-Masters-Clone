using DG.Tweening;
using Emre;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Tween moveTween;


    public void Kill()
    {
        moveTween?.Kill();
        PlayerSpawner.players.Remove(this);
        Destroy(gameObject);
        GameEvents.RaisePlayerDied(this);
    }
    
    public void DoLocalMove(Vector3 position)
    {
        moveTween?.Kill();
        moveTween = transform.DOLocalMove(position, 0.5f);
        
        moveTween
            .SetEase(Ease.OutBack);
    }
}