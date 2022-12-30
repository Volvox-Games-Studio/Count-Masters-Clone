using UnityEngine;

public class ArcherAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ArcherEnemyGroup group;

    private void Update()
    {
        PlayAttack();
    }

    private void PlayAttack()
    {
        if (group.state == EnemyState.Attack)
        {
            animator.SetBool("isAttack", true);
        }
    }
}