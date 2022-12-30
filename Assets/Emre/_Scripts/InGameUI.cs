using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Emre
{
    public class InGameUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text levelField;
        
        
        public UnityEvent<int> onCoinAmountLoaded;
        public UnityEvent<int> onCoinAmountChanged;
        public UnityEvent<int> onGemAmountLoaded;
        public UnityEvent<int> onGemAmountChanged;


        private void OnEnable()
        {
            onCoinAmountLoaded?.Invoke(Balance.CoinAmount);
            GameEvents.OnCoinAmountChanged += OnCoinAmountChanged;
            
            onGemAmountLoaded?.Invoke(Balance.GemAmount);
            GameEvents.OnGemAmountChanged += OnGemAmountChanged;

            GameEvents.OnLevelLoaded += OnLevelLoaded;
        }

        private void OnDestroy()
        {
            GameEvents.OnCoinAmountChanged -= OnCoinAmountChanged;
            GameEvents.OnGemAmountChanged -= OnGemAmountChanged;
            
            GameEvents.OnLevelLoaded -= OnLevelLoaded;
        }
        

        private void OnCoinAmountChanged(GameEventResponse response)
        {
            onCoinAmountChanged?.Invoke(response.coinAmount);
        }
        
        private void OnGemAmountChanged(GameEventResponse response)
        {
            onGemAmountChanged?.Invoke(response.gemAmount);
        }

        private void OnLevelLoaded(GameEventResponse response)
        {
            levelField.text = $"Level {response.currentLevel}";
        }
    }
}
