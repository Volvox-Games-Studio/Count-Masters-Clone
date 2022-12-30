using DG.Tweening;
using UnityEngine;

namespace Emre
{
    public class HoldAndMove : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform hand;

        [SerializeField, Min(0f)] private float range;
        [SerializeField, Min(0f)] private float duration;


        private void Start()
        {
            StartTween();
        }


        private void StartTween()
        {
            hand.DOAnchorPos(Vector2.right * range, duration)
                .From(Vector2.left * range)
                .SetEase(Ease.InOutExpo)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}