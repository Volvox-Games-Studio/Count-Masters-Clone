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


    private void OnEnable()
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
        transform.Translate( Vector3.forward * verticalSpeed * Time.deltaTime);
    }

    private void MoveHorizontal()
    {
        horizontalInput = joystick.Horizontal;
        
        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime);
    }
}
