using DG.Tweening;
using Emre;
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
    [SerializeField] private AudioSource walkSound;

    private float horizontalInput;
    private bool isMoving;
    private bool lockHorizontal;


    private void Awake()
    {
        GameEvents.OnPlayerGroupStateChanged += OnPlayerGroupStateChanged;
        GameEvents.OnStartLevelEnding += OnStartLevelEnding;
        GameEvents.OnReachedFinishLine += OnReachedFinishLine;
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerGroupStateChanged -= OnPlayerGroupStateChanged;
        GameEvents.OnStartLevelEnding -= OnStartLevelEnding;
        GameEvents.OnReachedFinishLine -= OnReachedFinishLine;
    }


    private void Update()
    {
        Move();
    }


    private void OnPlayerGroupStateChanged(GameEventResponse response)
    {
        isMoving = response.playerGroupState == PlayerGroupState.Walking;

        if (isMoving)
        {
            if (!walkSound.isPlaying)
            {
                walkSound.Play();
            }
        }

        else
        {
            walkSound.Stop();
        }
    }

    private void OnReachedFinishLine(GameEventResponse response)
    {
        walkSound.Stop();
    }
    
    private void OnStartLevelEnding(GameEventResponse response)
    {
        if (response.levelEndingType == LevelEndingType.Ladders)
        {
            lockHorizontal = true;
            transform.DOMoveX(0f, 0.25f)
                .SetEase(Ease.OutSine);
        }
        
        walkSound.Stop();
    }


    private void Move()
    {
        if (!isMoving) return;
        
        MoveForward();
        MoveHorizontal();
    }
    
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * (verticalSpeed * Time.deltaTime));
    }

    private void MoveHorizontal()
    {
        if (lockHorizontal) return;
        
        horizontalInput = joystick.Horizontal;
        var delta = Vector3.right * (horizontalInput * horizontalSpeed * Time.deltaTime);
        var newPosition = transform.position + delta;
        newPosition.x = Mathf.Clamp(newPosition.x, PlayerSpawner.LeftBorder, PlayerSpawner.RightBorder);
        transform.position = newPosition;
    }
}
