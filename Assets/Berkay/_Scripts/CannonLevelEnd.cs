using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Emre;
using UnityEngine;

public class CannonLevelEnd : MonoBehaviour
{
    [SerializeField] private Transform cannonTransform;
    [SerializeField] private GameObject ragdollPlayerPrefab;
    private int playerBullet;
    
    public void GetPlayersInCannon()
    {
        GameEvents.RaiseStartLevelEnding(LevelEndingType.Cannon);
        foreach (var player in PlayerSpawner.players)
        {
            player.transform.DOMove(cannonTransform.position, 1f).OnComplete(()=>
            {
                player.transform.DOScale(Vector3.zero, .25f)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                     {
                        player.Kill(true);
                     });
            });
        }

        playerBullet = PlayerSpawner.players.Count;
        Debug.Log(playerBullet);
    }
    
    
    
}
