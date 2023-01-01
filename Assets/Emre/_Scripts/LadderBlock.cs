using TMPro;
using UnityEngine;

namespace Emre
{
    public class LadderBlock : MonoBehaviour
    {
        [SerializeField] private TMP_Text multiplierField;
        [SerializeField] private Transform cube;
        [SerializeField] private new Renderer renderer;


        public Vector3 Size => cube.lossyScale;
        

        private float m_Multiplier;


        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        public void SetMultiplier(float multiplier)
        {
            m_Multiplier = multiplier;
            multiplierField.text = $"x{multiplier:0.0}";
        }

        public void SetWidth(float width)
        {
            var oldScale = cube.localScale;
            oldScale.x = width;
            cube.localScale = oldScale;
        }

        public void SetColor(Color color)
        {
            renderer.material.color = color;
        }
    }
}