using System;
using UnityEngine;
public class PlayerAttackTrigger : MonoBehaviour
{
    [SerializeField] private GameObject archerEnemyParent;
    
    private bool isTriggered = false;
    
    
    private void OnTriggerEnter(Collider other)         //TODO Player'ın enemy türlerine göre atak davranışları eklenecek 
    {                                                   
        if (!isTriggered && other.CompareTag("Player"))
        {
            Destroy(archerEnemyParent.gameObject);
        }
        
    }
}
