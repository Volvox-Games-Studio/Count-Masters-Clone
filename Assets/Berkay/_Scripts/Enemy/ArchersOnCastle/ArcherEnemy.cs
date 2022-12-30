using System;
using System.Collections;
using UnityEngine;

public class ArcherEnemy : MonoBehaviour
{
    [SerializeField] private ArcherEnemyGroup group;
    [SerializeField] private Transform arrowExitTransform;
    [SerializeField] private Arrow arrow;
    private bool isAttacking = false;
    
    private void Update()
    {
        if (group.state == EnemyState.Attack && !isAttacking)  //ATAK STATE TRİGGERLANINCA OK FIRLAT
        {
            StartCoroutine(AttackRoutine());
            isAttacking = true;
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(arrow, arrowExitTransform.position, Quaternion.identity);
        }
    }
}