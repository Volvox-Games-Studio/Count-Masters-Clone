using Berkay._Scripts;
using DG.Tweening;
using Emre;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string BattleTrigger = "BattleTrigger";
    
    
    [SerializeField, Min(0f)] private float formatDuration;
    [SerializeField, Min(0f)] private float dieFormatDuration;
    [SerializeField] private float moveSpeed;


    public Vector3 LocalPosition => transform.localPosition;
    

    private Tween moveTween;


    private void Start()
    {
        DOTween.SetTweensCapacity(1000, 50);
    }


    public void OnInteract(Interactable interactable)
    {
        if (interactable.CompareTag(BattleTrigger))
        {
            GetComponentInParent<PlayerGroupController>().BeginBattle(interactable.transform.position);
        }
    }
    
    public void Kill()
    {
        moveTween?.Kill();
        PlayerSpawner.Remove(this);
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

    public void Move(Vector3 position, float delay = 0f)
    {
        moveTween?.Kill();

        moveTween = transform.DOMove(position, moveSpeed);

        moveTween
            .SetSpeedBased()
            .SetDelay(delay);
    }
}