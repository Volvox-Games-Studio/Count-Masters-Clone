using System.Collections.Generic;
using DG.Tweening;
using EmreBeratKR.DummyAds;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Emre
{
    public class SkinStore : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform pages;
        [SerializeField] private AudioSource skinUnlockSound;
        [SerializeField] private AudioSource skinSelectSound;

        [Header("Values")] 
        [SerializeField, Min(0f)] private float pageSwitchDuration;
        [SerializeField, Min(0f)] private int skinCost = 500;


        public UnityEvent onEnoughMoneyForSkin;
        public UnityEvent onNotEnoughMoneyForSkin;
        

        private SkinButton[] SkinButtons => m_SkinButtons ??= pages.GetComponentsInChildren<SkinButton>(true);
        private SkinButton[] LockedButtons
        {
            get
            {
                var lockedButtons = new List<SkinButton>();
                
                foreach (var skinButton in SkinButtons)
                {
                    if (skinButton.State != SkinButtonState.Locked) continue;
                    
                    lockedButtons.Add(skinButton);
                }

                return lockedButtons.ToArray();
            }
        }

        private SkinButton RandomLockedButtonFromCurrentPage
        {
            get
            {
                while (true)
                {
                    if (!HasLockedButtonCurrentPage) return null;
                    
                    var lockedButtons = LockedButtons;
                    var randomIndex = Random.Range(0, lockedButtons.Length);
                    var randomButton = lockedButtons[randomIndex];

                    if (randomButton.Index >= 9)
                    {
                        if (m_PageIndex != 1) continue;

                        return randomButton;
                    }
                    
                    if (m_PageIndex != 0) continue;

                    return randomButton;
                }
            }
        }

        private SkinButton SelectedButton
        {
            get
            {
                foreach (var button in SkinButtons)
                {
                    if (button.State == SkinButtonState.Selected) return button;
                }

                return null;
            }
        }

        private bool HasLockedButtonCurrentPage
        {
            get
            {
                var buttons = SkinButtons;
                var startIndex = m_PageIndex * 9;
                var endIndex = startIndex + 9;

                for (int i = startIndex; i < endIndex; i++)
                {
                    if (buttons[i].State == SkinButtonState.Locked) return true;
                }

                return false;
            }
        }


        private SkinButton[] m_SkinButtons;
        private Tween m_AnchorPosTween;
        private int m_PageIndex;


        private void Awake()
        {
            GameEvents.OnSkinChanged += OnSkinChanged;
        }

        private void OnDestroy()
        {
            GameEvents.OnSkinChanged -= OnSkinChanged;
        }


        private void OnSkinChanged(GameEventResponse response)
        {
            var skinButtons = SkinButtons;

            for (int i = 0; i < skinButtons.Length; i++)
            {
                var skinButton = skinButtons[i];
                var isOldSelectedSkin = skinButton.State == SkinButtonState.Selected && skinButton.Index != response.skinIndex;
                
                if (isOldSelectedSkin) skinButton.Deselect();
            }
            
            skinSelectSound.Play();
        }

        public void OnCoinAmountLoaded(int amount)
        {
            HandleCoinAmount();
        }

        public void OnCoinAmountChanged(int amount)
        {
            HandleCoinAmount();
        }


        public void GiveFreeCoin(int amount)
        {
            var rewardedAd = DummyAdsManager
                .BuildRewardedDummyAd(DummyAdOrientation.Portrait);

            rewardedAd.OnRewardGranted += () =>
            {
                Balance.AddCoin(amount);
            };
        }
        
        public void OpenFirstPage()
        {
            m_PageIndex = 0;
            DoAnchorPos(Vector2.zero);
        }

        public void OpenSecondPage()
        {
            m_PageIndex = 1;
            DoAnchorPos(Vector2.left * 975f);
        }

        public void TryUnlockRandomSkin()
        {
            if (!Balance.TryRemoveCoin(skinCost)) return;
            
            if (!RandomLockedButtonFromCurrentPage) return;
            
            SelectedButton.Deselect();
            RandomLockedButtonFromCurrentPage.Unlock();
            
            skinUnlockSound.Play();
        }


        private void HandleCoinAmount()
        {
            if (Balance.HasEnoughCoin(skinCost))
            {
                onEnoughMoneyForSkin?.Invoke();
                return;
            }
            
            onNotEnoughMoneyForSkin?.Invoke();
        }
        
        private void DoAnchorPos(Vector2 anchorPos)
        {
            m_AnchorPosTween?.Kill();

            m_AnchorPosTween = pages.DOAnchorPos(anchorPos, pageSwitchDuration);

            m_AnchorPosTween
                .SetEase(Ease.OutSine);
        }
    }
}