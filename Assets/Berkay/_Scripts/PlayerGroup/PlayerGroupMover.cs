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

    private float horizontalInput;
    private bool isMoving;


    private void Awake()
    {
        GameEvents.OnPlayerGroupStateChanged += OnPlayerGroupStateChanged;
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerGroupStateChanged -= OnPlayerGroupStateChanged;
    }


    private void Update()
    {
        Move();
    }


    private void OnPlayerGroupStateChanged(GameEventResponse response)
    {
        isMoving = response.playerGroupState == PlayerGroupState.Walking;
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
        horizontalInput = joystick.Horizontal;
        var delta = Vector3.right * (horizontalInput * horizontalSpeed * Time.deltaTime);
        var newPosition = transform.position + delta;
        newPosition.x = Mathf.Clamp(newPosition.x, PlayerSpawner.LeftBorder, PlayerSpawner.RightBorder);
        transform.position = newPosition;
    }
}
