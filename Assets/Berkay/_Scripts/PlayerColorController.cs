using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerColor
{
    DarkBlue,
    LightBlue,
    DarkGreen,
    LightGreen,
    Yellow,
    Orange,
    Pink,
    Grey,
    Metalic,
    Black
}


public class PlayerColorController : MonoBehaviour
{
    [SerializeField] private Material[] colors;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    public static PlayerColor PlayerColor = PlayerColor.DarkBlue; //PLAYERIN RENGİNİ DEĞİŞTİRMEK İSTENİLDİĞİ YERDE BU ARKADAŞ ÇAĞIRILACAK
    private static int ActiveColor = 0;

    
    private void SetColor()
    {
        skinnedMeshRenderer.material = colors[(int)PlayerColor];
    }


    private void Update()
    {
        SetColor();
    }
}
