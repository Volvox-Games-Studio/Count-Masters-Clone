using DG.Tweening;
using UnityEngine;

namespace Berkay._Scripts.Enemy
{
    public class MeleeEnemy : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float moveSpeed;
        [SerializeField, Min(0f)] private float formatDuration;


        private MeleeEnemyGroup m_EnemyGroup;
        private Tween m_MoveTween;


        public void Inject(MeleeEnemyGroup enemyGroup)
        {
            m_EnemyGroup = enemyGroup;
        }
        
        public void Format(Vector3 localPosition)
        {
            m_MoveTween?.Kill();
            m_MoveTween = transform.DOLocalMove(localPosition, formatDuration);
                
            m_MoveTween
                .SetEase(Ease.OutBack);
        }

        public void Move(Vector3 position, float delay = 0f)
        {
            m_MoveTween?.Kill();

            m_MoveTween = transform.DOMove(position, moveSpeed);
            
            m_MoveTween
                .SetSpeedBased()
                .SetDelay(delay);
        }
        
        public void OnKilled()
        {
            m_MoveTween?.Kill();
            m_EnemyGroup.Remove(this);
        }
    }
}