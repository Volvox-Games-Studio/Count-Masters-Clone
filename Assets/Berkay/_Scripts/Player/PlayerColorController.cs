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

    public static PlayerColor PlayerColor = PlayerColor.DarkBlue;
    
    
    public void SetColor()
    {
        skinnedMeshRenderer.material = colors[(int)PlayerColor];
    }

    private void Update()
    {
        SetColor();
    }
}
