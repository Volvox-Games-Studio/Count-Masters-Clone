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
        [SerializeField] private RectTransform comingSoon;
        [SerializeField] private GameObject holdAndMove;
        [SerializeField] private TMP_Text levelField;

        [Header("Values")]
        [SerializeField, Min(0f)] private float hideDuration;
        
        
        public UnityEvent<int> onCoinAmountLoaded;
        public UnityEvent<int> onCoinAmountChanged;
        public UnityEvent<int> onGemAmountLoaded;
        public UnityEvent<int> onGemAmountChanged;

        
        private Vector2 m_LeftButtonsAnchorPos;
        private Vector2 m_RightButtonsAnchorPos;
        private Vector2 m_UpgradeButtonsAnchorPos;
        

        private void Awake()
        {
            onCoinAmountLoaded?.Invoke(Balance.CoinAmount);
            GameEvents.OnCoinAmountChanged += OnCoinAmountChanged;
            
            onGemAmountLoaded?.Invoke(Balance.GemAmount);
            GameEvents.OnGemAmountChanged += OnGemAmountChanged;

            GameEvents.OnGameStarted += OnGameStarted;
            GameEvents.OnLevelLoaded += OnLevelLoaded;

            GameEvents.OnBuildToggle += OnBuildToggle;
        }

        private void OnDestroy()
        {
            GameEvents.OnCoinAmountChanged -= OnCoinAmountChanged;
            GameEvents.OnGemAmountChanged -= OnGemAmountChanged;
            
            GameEvents.OnGameStarted -= OnGameStarted;
            GameEvents.OnLevelLoaded -= OnLevelLoaded;
            
            GameEvents.OnBuildToggle -= OnBuildToggle;
        }


        public void OnBuildToggle(GameEventResponse response)
        {
            if (response.openBuild)
            {
                HideNonGameplayUIs();
                
                comingSoon.DOScale(Vector3.one, 0.25f)
                    .From(Vector3.zero)
                    .SetDelay(0.5f)
                    .SetEase(Ease.OutBack)
                    .OnStart(() =>
                    {
                        comingSoon.gameObject.SetActive(true);
                    });

                comingSoon.DORotate(Vector3.zero, 0.25f)
                    .From(Vector3.forward * 60)
                    .SetDelay(0.5f)
                    .SetEase(Ease.OutBack);

                return;
            }
            
            ShowNonGameplayUIs();
            
            comingSoon.DOScale(Vector3.zero, 0.25f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    comingSoon.gameObject.SetActive(false);
                });
                
            comingSoon.DORotate(Vector3.forward * 60, 0.25f)
                .SetEase(Ease.InBack);
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


        private void ShowNonGameplayUIs()
        {
            leftButtons.DOAnchorPos(m_LeftButtonsAnchorPos, hideDuration)
                .SetEase(Ease.OutBack);

            rightButtons.DOAnchorPos(m_RightButtonsAnchorPos, hideDuration)
                .SetEase(Ease.OutBack);

            upgradeButtons.DOAnchorPos(m_UpgradeButtonsAnchorPos, hideDuration)
                .SetEase(Ease.OutBack);
            
            holdAndMove.SetActive(true);
        }

        private void HideNonGameplayUIs()
        {
            const float smallButtonsHideRange = 230f;

            m_LeftButtonsAnchorPos = leftButtons.anchoredPosition;
            leftButtons.DOAnchorPos(Vector2.left * smallButtonsHideRange, hideDuration)
                .SetEase(Ease.InBack);

            m_RightButtonsAnchorPos = rightButtons.anchoredPosition;
            rightButtons.DOAnchorPos(Vector2.right * smallButtonsHideRange, hideDuration)
                .SetEase(Ease.InBack);

            m_UpgradeButtonsAnchorPos = upgradeButtons.anchoredPosition;
            upgradeButtons.DOAnchorPos(Vector2.down * 500f, hideDuration)
                .SetEase(Ease.InBack);
            
            holdAndMove.SetActive(false);
        }
    }
}
