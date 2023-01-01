using System;
using UnityEngine.Events;

namespace Emre
{
    public static class GameEvents
    {
        public static UnityAction<GameEventResponse> OnLoadedSoundToggle;
        public static UnityAction<GameEventResponse> OnSoundToggle;
        public static UnityAction<GameEventResponse> OnLoadedVibrationToggle;
        public static UnityAction<GameEventResponse> OnVibrationToggle;

        public static UnityAction<GameEventResponse> OnCoinAmountChanged;
        public static UnityAction<GameEventResponse> OnGemAmountChanged;

        public static UnityAction<GameEventResponse> OnColorWheelSpin;
        public static UnityAction<GameEventResponse> OnSkinChanged;

        public static UnityAction<GameEventResponse> OnStartUnitsUpgraded;
        public static UnityAction<GameEventResponse> OnIncomeUpgraded;

        public static UnityAction<GameEventResponse> OnLevelLoaded;
        public static UnityAction<GameEventResponse> OnDoorDashed;
        public static UnityAction<GameEventResponse> OnPlayerGroupSizeChanged;
        public static UnityAction<GameEventResponse> OnPlayerDied;
        public static UnityAction<GameEventResponse> OnPlayerGroupStateChanged;
        public static UnityAction<GameEventResponse> OnBattleEnd;

        public static UnityAction<GameEventResponse> OnGameStarted;
        public static UnityAction<GameEventResponse> OnGameOver;
        public static UnityAction<GameEventResponse> OnLevelComplete;
        public static UnityAction<GameEventResponse> OnReachedFinishLine;
        public static UnityAction<GameEventResponse> OnStartLevelEnding;


        public static void RaiseLoadedSongToggle(bool isOn)
        {
            OnLoadedSoundToggle?.Invoke(new GameEventResponse()
            {
                isSoundOn = isOn
            });
        }
        
        public static void RaiseSongToggle(bool isOn)
        {
            OnSoundToggle?.Invoke(new GameEventResponse()
            {
                isSoundOn = isOn
            });
        }
        
        public static void RaiseLoadedVibrationToggle(bool isOn)
        {
            OnLoadedVibrationToggle?.Invoke(new GameEventResponse()
            {
                isVibrationOn = isOn
            });
        }
        
        public static void RaiseVibrationToggle(bool isOn)
        {
            OnVibrationToggle?.Invoke(new GameEventResponse()
            {
                isVibrationOn = isOn
            });
        }

        public static void RaiseCoinAmountChanged(int amount)
        {
            OnCoinAmountChanged?.Invoke(new GameEventResponse()
            {
                coinAmount = amount
            });
        }
        
        public static void RaiseGemAmountChanged(int amount)
        {
            OnGemAmountChanged?.Invoke(new GameEventResponse()
            {
                gemAmount = amount
            });
        }

        public static void RaiseColorWheelSpin(int index)
        {
            OnColorWheelSpin?.Invoke(new GameEventResponse()
            {
                colorWheelIndex = index
            });
        }

        public static void RaiseSkinChanged(int index)
        {
            OnSkinChanged?.Invoke(new GameEventResponse()
            {
                skinIndex = index
            });
        }

        public static void RaiseStartUnitsUpgraded(int startUnits)
        {
            OnStartUnitsUpgraded?.Invoke(new GameEventResponse()
            {
                startUnits = startUnits
            });
        }

        public static void RaiseIncomeUpgraded(float incomeMultiplier)
        {
            OnIncomeUpgraded?.Invoke(new GameEventResponse()
            {
                incomeMultiplier = incomeMultiplier
            });
        }
        
        public static void RaiseLevelLoaded(int level)
        {
            OnLevelLoaded?.Invoke(new GameEventResponse()
            {
                currentLevel = level
            });
        }
        
        public static void RaiseDoorDashed(GateOperator gateOperator, int gateValue)
        {
            OnDoorDashed?.Invoke(new GameEventResponse()
            {
                gateOperator = gateOperator,
                gateValue = gateValue
            });
        }

        public static void RaisePlayerGroupSizeChanged(int size, float radius)
        {
            OnPlayerGroupSizeChanged?.Invoke(new GameEventResponse()
            {
                playerGroupSize = size,
                playerGroupRadius = radius
            });
        }
        
        public static void RaisePlayerDied(PlayerController player)
        {
            OnPlayerDied?.Invoke(new GameEventResponse()
            {
                diedPlayer = player
            });
        }

        public static void RaisePlayerGroupStateChanged(PlayerGroupState state)
        {
            OnPlayerGroupStateChanged?.Invoke(new GameEventResponse()
            {
                playerGroupState = state
            });
        }

        public static void RaiseBattleEnd(bool isVictory)
        {
            OnBattleEnd?.Invoke(new GameEventResponse()
            {
                isVictory = isVictory
            });
        }

        public static void RaiseGameStarted()
        {
            OnGameStarted?.Invoke(new GameEventResponse());
        }

        public static void RaiseGameOver(GameOverReason reason)
        {
            OnGameOver?.Invoke(new GameEventResponse()
            {
                gameOverReason = reason
            });
        }

        public static void RaiseLevelComplete(LevelCompleteType type)
        {
            OnLevelComplete?.Invoke(new GameEventResponse()
            {
                levelCompleteType = type
            });
        }

        public static void RaiseReachedFinishLine()
        {
            OnReachedFinishLine?.Invoke(new GameEventResponse());
        }

        public static void RaiseStartLevelEnding(LevelEndingType type)
        {
            OnStartLevelEnding?.Invoke(new GameEventResponse()
            {
                levelEndingType = type
            });
        }
    }


    [Serializable]
    public struct GameEventResponse
    {
        public bool isSoundOn;
        public bool isVibrationOn;
        public int coinAmount;
        public int gemAmount;
        public int colorWheelIndex;
        public int skinIndex;
        public int startUnits;
        public float incomeMultiplier;
        public int currentLevel;
        public GateOperator gateOperator;
        public int gateValue;
        public int playerGroupSize;
        public float playerGroupRadius;
        public PlayerController diedPlayer;
        public PlayerGroupState playerGroupState;
        public bool isVictory;
        public GameOverReason gameOverReason;
        public LevelCompleteType levelCompleteType;
        public LevelEndingType levelEndingType;
    }

    public enum GameOverReason
    {
        LosingBattle,
        NoPlayerLeft
    }

    public enum LevelCompleteType
    {
        ChestOpen
    }

    public enum LevelEndingType
    {
        Ladders
    }
}