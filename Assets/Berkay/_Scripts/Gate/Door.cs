using System;
using System.Collections;
using System.Collections.Generic;
using Emre;
using TMPro;
using UnityEngine;

public enum GateOperator
{
    Add,
    Mult
}

public class Door : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private GateOperator gateOperator;
    [SerializeField] private int value;

    [Header("References")] 
    [SerializeField] private TMP_Text doorText;
    
    public bool isEntered = false;
    private BoxCollider boxCollider;

    
    private void Start()
    {
        boxCollider = GetComponentInChildren<BoxCollider>();
        UpdateText();
    }

    private void Update()
    {
        if (isEntered)
        {
            BeUnInteractable();
        }
    }

    private void UpdateText()
    {
        if (gateOperator == GateOperator.Add)
        {
            doorText.text = "+" + value;
        }
        else
        {
            doorText.text = "x" + value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isEntered)
        {
            isEntered = true;
            GameEvents.RaiseDoorDashed(gateOperator, value);
        }
    }
    

    public void BeUnInteractable()
    {
        boxCollider.enabled = false;
    }
}
