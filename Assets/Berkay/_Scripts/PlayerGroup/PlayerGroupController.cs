using Berkay._Scripts.Enemy;
using Emre;
using UnityEngine;


public class PlayerGroupController : MonoBehaviour
{
    public static PlayerGroupState PlayerGroupState
    {
        get => ms_PlayerGroupState;
        private set
        {
            ms_PlayerGroupState = value;
            GameEvents.RaisePlayerGroupStateChanged(value);
        }
    }


    private static PlayerGroupState ms_PlayerGroupState;
    

    private void Awake()
    {
        GameEvents.OnGameStarted += OnGameStarted;
        GameEvents.OnPlayerGroupSizeChanged += OnPlayerGroupSizeChanged;
        GameEvents.OnBattleEnd += OnBattleEnd;
        GameEvents.OnLevelComplete += OnLevelComplete;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= OnGameStarted;
        GameEvents.OnPlayerGroupSizeChanged -= OnPlayerGroupSizeChanged;
        GameEvents.OnBattleEnd -= OnBattleEnd;
    }


    private void Start()
    {
        PlayerGroupState = PlayerGroupState.Waiting;
    }


    public void BeginBattle(Vector3 position)
    {
        PlayerGroupState = PlayerGroupState.Fighting;
        
        var delay = BattleUtils.BattleInvadeInterval;

        foreach (var player in PlayerSpawner.players)
        {
            var offset = BattleUtils.GetRandomOffset();
            player.Move(position + offset, delay);
            delay += BattleUtils.BattleInvadeInterval;
        }
    }
    
    private void OnGameStarted(GameEventResponse response)
    {
        PlayerGroupState = PlayerGroupState.Walking;
    }

    private void OnPlayerGroupSizeChanged(GameEventResponse response)
    {
        PlayerGroupState = PlayerGroupState;
    }

    private void OnBattleEnd(GameEventResponse response)
    {
        if (response.isVictory)
        {
            PlayerGroupState = PlayerGroupState.Walking;
        }
    }
    
    private void OnLevelComplete(GameEventResponse response)
    {
        PlayerGroupState = PlayerGroupState.Waiting;
    }
}
