using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class ButtonObstacle : MonoBehaviour
{
    [SerializeField] private Animator k1Anim;
    [SerializeField] private Animator k2Anim;
    
    
    private void CloseKapaks()
    {
       k1Anim.SetBool("up",true);
       k2Anim.SetBool("up",true);
    }

    private void GoDown()
    {
        transform.Translate(Vector3.down * 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseKapaks();
            GoDown();
        }
    }
}
