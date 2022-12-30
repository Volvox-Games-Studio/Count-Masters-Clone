using UnityEngine;

public class ArcherEnemyGroup : Enemy
{
    public EnemyState state;
    
    
    private void Start()
    {
        state = EnemyState.Wait;
    }

    private void OnTriggerEnter(Collider other)     //ATAK STATE TRİGGERLIYOR
    {
        if (other.CompareTag("Player"))
        {
            state = EnemyState.Attack;
        }
    }
}