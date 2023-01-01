using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Emre
{
    public class Counter : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private AudioClip sound;
        [SerializeField] private TMP_Text countField;

        
        private int m_Count;
        
        
        public void UpdateCount(int count)
        {
            const float duration = 1f;
            
            DOTween.To(() => m_Count, x => m_Count = x, count, duration)
                .SetEase(Ease.OutSine)
                .OnUpdate(() =>
                {
                    countField.text = m_Count.ToString();
                });

            transform.DOScale(Vector3.one * 1.2f, 0.25f)
                .SetEase(Ease.OutSine);

            transform.DOScale(Vector3.one, 0.25f)
                .SetEase(Ease.OutSine)
                .SetDelay(duration);

            if (sound)
            {
                AudioSource.PlayClipAtPoint(sound, Vector3.zero);
            }
        }
    }
}