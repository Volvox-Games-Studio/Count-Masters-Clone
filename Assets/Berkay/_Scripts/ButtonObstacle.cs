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
        transform.DOMove(transform.position + Vector3.down * 0.5f, 0.25f)
            .SetEase(Ease.OutSine);
    }


    public void OnInteract()
    {
        CloseKapaks();
        GoDown();
    }
}
