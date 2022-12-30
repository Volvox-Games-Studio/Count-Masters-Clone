using UnityEngine;
using UnityEngine.Events;

namespace Emre
{
    public class InGameUI : MonoBehaviour
    {
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
        }

        private void OnDestroy()
        {
            GameEvents.OnCoinAmountChanged -= OnCoinAmountChanged;
            GameEvents.OnGemAmountChanged -= OnGemAmountChanged;
        }


        private void OnCoinAmountChanged(GameEventResponse response)
        {
            onCoinAmountChanged?.Invoke(response.coinAmount);
        }
        
        private void OnGemAmountChanged(GameEventResponse response)
        {
            onGemAmountChanged?.Invoke(response.gemAmount);
        }
    }
}
