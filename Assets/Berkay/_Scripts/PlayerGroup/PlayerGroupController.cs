using Emre;
using UnityEngine;


public class PlayerGroupController : MonoBehaviour
{
    private PlayerGroupState PlayerGroupState
    {
        get => m_PlayerGroupState;
        set
        {
            m_PlayerGroupState = value;
            GameEvents.RaisePlayerGroupStateChanged(value);
        }
    }


    private PlayerGroupState m_PlayerGroupState;
    

    private void Awake()
    {
        GameEvents.OnGameStarted += OnGameStarted;
        GameEvents.OnPlayerGroupExpanded += OnPlayerGroupExpanded;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= OnGameStarted;
        GameEvents.OnPlayerGroupExpanded -= OnPlayerGroupExpanded;
    }


    private void Start()
    {
        PlayerGroupState = PlayerGroupState.Waiting;
    }


    private void OnGameStarted(GameEventResponse response)
    {
        PlayerGroupState = PlayerGroupState.Walking;
    }

    private void OnPlayerGroupExpanded(GameEventResponse response)
    {
        PlayerGroupState = PlayerGroupState;
    }
}
