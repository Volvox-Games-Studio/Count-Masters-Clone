using UnityEngine;

namespace Berkay._Scripts.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int IsRun = Animator.StringToHash("isRun");


        [SerializeField] private Animator animator;
        
        
        private void PlayRun()
        {
            animator.SetBool(IsRun, true);
        }

        private void PlayIdle()
        {
            animator.SetBool(IsRun, false);
        }
    }
}