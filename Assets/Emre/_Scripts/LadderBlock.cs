using TMPro;
using UnityEngine;

namespace Emre
{
    public class LadderBlock : MonoBehaviour
    {
        private const string MultiplierKey = "Multiplier";


        [SerializeField] private AudioSource stepSound;
        [SerializeField] private TMP_Text multiplierField;
        [SerializeField] private Transform cube;
        [SerializeField] private new Renderer renderer;


        public Vector3 Size => cube.lossyScale;


        public static float Multiplier
        {
            get => PlayerPrefs.GetFloat(MultiplierKey, 1f);
            private set => PlayerPrefs.SetFloat(MultiplierKey, value);
        }


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

        public void SetStepSound(AudioClip clip)
        {
            stepSound.clip = clip;
        }

        public void Touch()
        {
            Multiplier = m_Multiplier;
            stepSound.Play();
        }
    }
}