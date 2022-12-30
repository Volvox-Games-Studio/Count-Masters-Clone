using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Emre
{
    public class ColorWheelButton : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform ferrisWheel;

        [Header("Values")]
        [SerializeField] private float spinningSpeed;


        private float RandomAngle => Angle + Random.Range(16f, 31f);
        private float Angle => ferrisWheel.localEulerAngles.z;
        
        
        private bool m_IsSpinning;
        

        private Tween m_RotationTween;
        
        
        public void OnClicked()
        {
            TryStartSpinning();
        }


        private bool TryStartSpinning()
        {
            if (m_IsSpinning) return false;

            m_IsSpinning = true;

            m_RotationTween = ferrisWheel.DORotate(Vector3.forward * RandomAngle, spinningSpeed);

            m_RotationTween
                .SetSpeedBased()
                .SetLoops(40, LoopType.Incremental)
                .OnComplete(() =>
                {
                    m_IsSpinning = false;
                    var result = Angle switch
                    {
                        > 342 => 0,
                        _ => (int) (Angle + 18) / 36
                    };
                    
                    GameEvents.RaiseColorWheelSpin(result);
                });

            return true;
        }
    }
}
