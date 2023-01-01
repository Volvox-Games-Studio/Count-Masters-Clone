using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Emre
{
    public class InGameUI : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private RectTransform leftButtons;
        [SerializeField] private RectTransform rightButtons;
        [SerializeField] private RectTransform upgradeButtons;
        [SerializeField] private GameObject holdAndMove;
        [SerializeField] private TMP_Text levelField;

        [Header("Values")]
        [SerializeField, Min(0f)] private float hideDuration;
        
        
        public UnityEvent<int> onCoinAmountLoaded;
        public UnityEvent<int> onCoinAmountChanged;
        public UnityEvent<int> onGemAmountLoaded;
        public UnityEvent<int> onGemAmountChanged;


        private void Awake()
        {
            onCoinAmountLoaded?.Invoke(Balance.CoinAmount);
            GameEvents.OnCoinAmountChanged += OnCoinAmountChanged;
            
            onGemAmountLoaded?.Invoke(Balance.GemAmount);
            GameEvents.OnGemAmountChanged += OnGemAmountChanged;

            GameEvents.OnGameStarted += OnGameStarted;
            GameEvents.OnLevelLoaded += OnLevelLoaded;
        }

        private void OnDestroy()
        {
            GameEvents.OnCoinAmountChanged -= OnCoinAmountChanged;
            GameEvents.OnGemAmountChanged -= OnGemAmountChanged;
            
            GameEvents.OnGameStarted -= OnGameStarted;
            GameEvents.OnLevelLoaded -= OnLevelLoaded;
        }


        private void OnGameStarted(GameEventResponse response)
        {
            HideNonGameplayUIs();
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


        private void HideNonGameplayUIs()
        {
            const float smallButtonsHideRange = 230f;

            leftButtons.DOAnchorPos(Vector2.left * smallButtonsHideRange, hideDuration)
                .SetEase(Ease.InBack);

            rightButtons.DOAnchorPos(Vector2.right * smallButtonsHideRange, hideDuration)
                .SetEase(Ease.InBack);

            upgradeButtons.DOAnchorPos(Vector2.down * 500f, hideDuration)
                .SetEase(Ease.InBack);
            
            holdAndMove.SetActive(false);
        }
    }
}
