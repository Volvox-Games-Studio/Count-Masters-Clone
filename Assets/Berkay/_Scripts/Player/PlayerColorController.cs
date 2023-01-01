using Emre;
using UnityEngine;

public class PlayerColorController : MonoBehaviour
{
    [SerializeField] private ColorContainer colors;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    
    
    

    private void Awake()
    {
        GameEvents.OnColorWheelSpin += OnColorWheelSpin;
    }

    private void Start()
    {
        SetColor(ColorWheelButton.SkinColorIndex);
    }

    private void OnDestroy()
    {
        GameEvents.OnColorWheelSpin -= OnColorWheelSpin;
    }


    private void OnColorWheelSpin(GameEventResponse response)
    {
        SetColor(response.colorWheelIndex);
    }
    

    private void SetColor(int index)
    {
        var color = colors[index];
        skinnedMeshRenderer.sharedMaterial.color = color;
        GetComponentInParent<PlayerGroupController>().SetCounterColor(color);
    }
}
