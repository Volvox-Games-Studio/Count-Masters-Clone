using UnityEngine;

namespace Emre
{
    public class CustomToggle : MonoBehaviour
    {
        [SerializeField] private GameObject offGameObject;
        [SerializeField] private AudioSource toggleSound;
    
    
        public void OnValueChanged(bool isOn)
        {
            offGameObject.SetActive(!isOn);
            toggleSound.Play();
        }
    }
}
