using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform target;

    private void OnEnable() //PLAYER LİSTESİNDEN RANDOM TARGET SEÇ
    {
        var randomTarget = Random.Range(0, PlayerSpawner.players.Count);
        target = PlayerSpawner.players[randomTarget].transform;
    }

    private void Update()
    {
        if (target == null) //SEÇTİĞİM TARGET BEN YOLDAYKEN YOK EDİLİRSE YENİ TARGET SEÇ
        {
            var randomTarget = Random.Range(0, PlayerSpawner.players.Count);
            target = PlayerSpawner.players[randomTarget].transform;
        }
        GoTarget();
    }

    private void GoTarget()
    {
        var direction = (target.transform.position - transform.position) + Vector3.up * 2;
        transform.position += direction.normalized * Time.deltaTime * speed;
    }
}