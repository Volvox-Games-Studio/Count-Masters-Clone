using DG.Tweening;
using Emre;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Min(0f)] private float formatDuration;
    [SerializeField, Min(0f)] private float dieFormatDuration;


    public Vector3 LocalPosition => transform.localPosition;
    

    private Tween moveTween;


    private void Start()
    {
        DOTween.SetTweensCapacity(1000, 50);
    }


    public void Kill()
    {
        moveTween?.Kill();
        PlayerSpawner.players.Remove(this);
        Destroy(gameObject);
        GameEvents.RaisePlayerDied(this);
    }
    
    public void Format(Vector3 localPosition, bool afterDied)
    {
        var duration = afterDied ? dieFormatDuration : formatDuration;
        var ease = afterDied ? Ease.Linear : Ease.OutBack;
        
        moveTween?.Kill();
        moveTween = transform.DOLocalMove(localPosition, duration);
                
        moveTween
            .SetEase(ease);
    }
}