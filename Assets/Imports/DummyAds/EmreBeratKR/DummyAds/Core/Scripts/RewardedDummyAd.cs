using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EmreBeratKR.DummyAds
{
    public class RewardedDummyAd : BaseDummyAd
    {
        private const int DefaultDuration = 5;
        
        
        [SerializeField] private GameObject mainCounterObject;
        [SerializeField] private Text counterField;


        public UnityAction OnFinished;
        public UnityAction OnRewardGranted;
        

        private float m_StartTime;
        private int m_Duration = DefaultDuration;
        

        private void OnEnable()
        {
            OnLoaded += CountBack;
            OnClosed += GrantReward;
        }

        private void OnDisable()
        {
            OnLoaded -= CountBack;
            OnClosed -= GrantReward;
        }


        public override BaseDummyAd SetDuration(int duration)
        {
            m_Duration = duration;
            return this;
        }


        private void CountBack()
        {
            StartCoroutine(Counting());
            
            IEnumerator Counting()
            {
                var oldTimeScale = Time.timeScale;
                Time.timeScale = 0f;
                m_StartTime = Time.unscaledTime;

                while (true)
                {
                    var elapsedSeconds = Mathf.FloorToInt(Time.unscaledTime - m_StartTime);
                    var secondsLeft = m_Duration - elapsedSeconds;

                    if (secondsLeft < 0)
                    {
                        OnCountFinished();
                        break;
                    }
                    
                    UpdateCounterField(secondsLeft);

                    yield return null;
                }

                Time.timeScale = oldTimeScale;
            }
        }

        private void OnCountFinished()
        {
            HideCounter();
            ShowCloseButton();
            OnFinished?.Invoke();
        }
        
        private void GrantReward()
        {
            OnRewardGranted?.Invoke();
        }

        private void UpdateCounterField(int secondsLeft)
        {
            counterField.text = secondsLeft.ToString();
        }

        private void HideCounter()
        {
            mainCounterObject.SetActive(false);
        }
    }
}