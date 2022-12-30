using System;
using UnityEngine;
public class PlayerAttackTrigger : MonoBehaviour
{
    [SerializeField] private ArcherEnemyGroup enemy;
    private int enemyCount;
    private bool isTriggered = false;
    
    
    private void Start()
    {
        enemyCount = enemy.groupCount;
    }
    
    private void OnTriggerEnter(Collider other)         //PLAYER KULENİN YANINA GELDİĞİ ZAMAN TETİKLENİYOR, KULEDEKİ ASKER SAYISI KADAR 
    {                                                   //PLAYER ÖLÜYOR, SONRA KULE YOK OLUYOR
        if (!isTriggered)
        {
            isTriggered = true;
            for (int i = 1; i <= enemyCount; i++)
            {
                PlayerSpawner.players.Remove(PlayerSpawner.players[^i]);
                Destroy(PlayerSpawner.players[^i].gameObject);
            }
            Destroy(enemy.gameObject);
        }
        
    }
}
