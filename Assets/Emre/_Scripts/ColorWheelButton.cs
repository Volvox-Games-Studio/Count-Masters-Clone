using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Emre
{
    public class ColorWheelButton : MonoBehaviour
    {
        private const string SkinColorKey = "Skin_Color";
        
        
        [Header("References")]
        [SerializeField] private RectTransform ferrisWheel;
        [SerializeField] private AudioSource ferrisWheelSound;
        [SerializeField] private AudioSource colorChangeSound;

        [Header("Values")]
        [SerializeField] private float spinningSpeed;


        public static int SkinColorIndex
        {
            get => PlayerPrefs.GetInt(SkinColorKey, 0);
            private set
            {
                PlayerPrefs.SetInt(SkinColorKey, value);
                GameEvents.RaiseColorWheelSpin(value);
            }
        }
        

        private float RandomAngle => Angle + Random.Range(16f, 31f);

        private float Angle
        {
            get => ferrisWheel.localEulerAngles.z;
            set
            {
                var oldLocalEulerAngles = ferrisWheel.localEulerAngles;
                oldLocalEulerAngles.z = value;
                ferrisWheel.localEulerAngles = oldLocalEulerAngles;
            }
        }
        
        
        private bool m_IsSpinning;
        

        private Tween m_RotationTween;


        private void Start()
        {
            Angle = SkinColorIndex * 36;
            SkinColorIndex = SkinColorIndex;
        }


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
                .OnStart(() =>
                {
                    ferrisWheelSound.Play();
                })
                .OnComplete(() =>
                {
                    SnapToBar();
                });

            return true;
        }

        private void SnapToBar()
        {
            var factor = (int) Angle / 36 + 1;
            var snappedAngle = factor * 36;

            m_RotationTween = ferrisWheel.DORotate(Vector3.forward * snappedAngle, spinningSpeed);

            m_RotationTween
                .SetSpeedBased()
                .OnComplete(() =>
                {
                    m_IsSpinning = false;
                    var result = Angle switch
                    {
                        > 342 => 0,
                        _ => (int) (Angle + 18) / 36
                    };

                    SkinColorIndex = result;

                    ferrisWheelSound.Stop();
                    colorChangeSound.Play();
                    Vibrator.Vibrate();
                });
        }
    }
}
