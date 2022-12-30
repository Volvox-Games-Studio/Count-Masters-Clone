using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Emre
{
    public class UpgradeButton : MonoBehaviour
    {
        private const string Max = "Max";


        [Header("References")] 
        [SerializeField] private RectTransform main;
        [SerializeField] private TMP_Text levelField;
        [SerializeField] private TMP_Text costField;
        [SerializeField] private Image buttonImage;
        [SerializeField] private Image coinImage;
        [SerializeField] private GameObject upgradeIcon;
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite activeCoinSprite;
        [SerializeField] private Sprite inactiveSprite;
        [SerializeField] private Sprite inactiveCoinSprite;

        [Header("Values")]
        [SerializeField] private int[] costs;
        [SerializeField] private string saveKey;
        [SerializeField] private AnimationCurve punchCurve;
        [SerializeField, Min(0f)] private float shakeDuration;
        [SerializeField, Min(0f)] private float punchDuration;
        [SerializeField, Min(0f)] private float shrinkSize;


        private Vector2 m_StartAnchorPos;
        private Tween m_AnchorPosTween;
        private Tween m_SizeTween;
    

        private int Level
        {
            get => PlayerPrefs.GetInt(saveKey, 1);
            set => PlayerPrefs.SetInt(saveKey, value);
        }
    
        private bool Upgradable
        {
            set
            {
                buttonImage.sprite = value ? activeSprite : inactiveSprite;
                coinImage.sprite = value ? activeCoinSprite : inactiveCoinSprite;
                upgradeIcon.SetActive(value);
            }
        }
    
        private int LevelIndex => Level - 1;
        private int CurrentCost => costs[LevelIndex];
        private bool IsMaxed => LevelIndex >= costs.Length;

    
        private void Start()
        {
            m_StartAnchorPos = main.anchoredPosition;
            UpdateFields();
        }
    

        public void OnClicked()
        {
            TryUpgrade();
        }


        private void UpdateFields()
        {
            Upgradable = !IsMaxed && Balance.CoinAmount >= CurrentCost;
            UpdateLevelField();
            UpdateCostField();
        }
    
        private void UpdateLevelField()
        {
            var levelString = IsMaxed ? Max : Level.ToString();
            levelField.text = $"{levelString}\nLVL";
        }

        private void UpdateCostField()
        {
            costField.text = IsMaxed ? Max : CurrentCost.ToString();
        }
    
        private bool TryUpgrade()
        {
            var canUpgrade = !IsMaxed && Balance.TryRemoveCoin(CurrentCost);
        
            if (canUpgrade)
            {
                Level += 1;
                OnUpgradeSucceed();
                return true;
            }
        
            OnUpgradeFailed();
            return false;
        }

        private void OnUpgradeFailed()
        {
            m_AnchorPosTween?.Kill();
            
            main.anchoredPosition = m_StartAnchorPos;
            m_AnchorPosTween = main.DOShakeAnchorPos(shakeDuration, 100f, 50, 90f);
            m_AnchorPosTween
                .OnUpdate(() =>
                {
                    var anchorPos = main.anchoredPosition;
                    anchorPos.y = m_StartAnchorPos.y;
                    main.anchoredPosition = anchorPos;
                });
        }

        private void OnUpgradeSucceed()
        {
            UpdateFields();
        
            m_SizeTween?.Kill();
            m_SizeTween = main.DOScale(Vector3.one, punchDuration)
                .From(shrinkSize);

            m_SizeTween
                .SetEase(punchCurve);
        }
    }
}
