using UnityEngine;
using UnityEngine.Events;

namespace Berkay._Scripts
{
    public class Interactable : MonoBehaviour
    {
        public UnityEvent onInteract;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                onInteract?.Invoke();
            }
        }
    }
}