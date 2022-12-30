using UnityEngine;

namespace Emre
{
    public class CustomToggle : MonoBehaviour
    {
        [SerializeField] private GameObject offGameObject;
    
    
        public void OnValueChanged(bool isOn)
        {
            offGameObject.SetActive(!isOn);
        }
    }
}
