using UnityEngine;
using UnityEngine.Events;

namespace Emre
{
    public class InGameUI : MonoBehaviour
    {
        public UnityEvent<int> onCoinAmountLoaded;
        public UnityEvent<int> onCoinAmountChanged;


        private void OnEnable()
        {
            onCoinAmountLoaded?.Invoke(Balance.CoinAmount);
            GameEvents.OnCoinAmountChanged += OnCoinAmountChanged;
        }

        private void OnDestroy()
        {
            GameEvents.OnCoinAmountChanged -= OnCoinAmountChanged;
        }


        private void OnCoinAmountChanged(GameEventResponse response)
        {
            onCoinAmountChanged?.Invoke(response.coinAmount);
        }
    }
}
