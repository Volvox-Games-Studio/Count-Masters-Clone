using Berkay._Scripts;
using DG.Tweening;
using Emre;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string BattleTrigger = "BattleTrigger";
    private const string FinishTrigger = "FinishTrigger";
    private const string LadderBlock = "LadderBlock";
    private const string ChestEnter = "ChestEnter";


    [SerializeField] private AudioClip dieSound;
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
        
        else if (interactable.CompareTag(FinishTrigger))
        {
            GameEvents.RaiseReachedFinishLine();
        }
        
        else if (interactable.CompareTag(LadderBlock))
        {
            var playerLeft = transform.parent.childCount - 1;
            transform.parent = null;
            CameraManager.SetLadderFocus(interactable.transform.position);

            if (playerLeft <= 0)
            {
                GameEvents.RaiseLevelComplete(LevelCompleteType.ChestNotOpen);
            }
        }
        
        else if (interactable.CompareTag(ChestEnter))
        {
            CameraManager.LadderFocus.parent = transform.parent;
            var position = interactable.transform.position;
            CameraManager.SetLadderFocus(position);
            var groupOffset = GroupUtils.IndexToLocalPosition(PlayerSpawner.players.IndexOf(this));
            var localPosition = transform.parent.InverseTransformPoint(position + groupOffset);
            
            moveTween?.Kill();
            moveTween = transform.DOLocalMove(localPosition, formatDuration);
            moveTween
                .SetEase(Ease.OutSine);
        }
        
        Vibrator.Vibrate();
    }
    
    public void Kill()
    {
        moveTween?.Kill();
        PlayerSpawner.Remove(this);
        Destroy(gameObject);
        GameEvents.RaisePlayerDied(this);
        AudioSource.PlayClipAtPoint(dieSound, Vector3.zero);
        Vibrator.Vibrate();
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

    public void LocalMove(Vector3 localPosition)
    {
        moveTween?.Kill();

        moveTween = transform.DOLocalMove(localPosition, moveSpeed);

        moveTween
            .SetSpeedBased();
    }
}