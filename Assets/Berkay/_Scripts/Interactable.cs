using UnityEngine;
using UnityEngine.Events;

namespace Berkay._Scripts
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private bool disableAfterInteract;
        
        
        public UnityEvent onInteract;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                if (disableAfterInteract) gameObject.SetActive(false);
                
                playerController.OnInteract(this);
                onInteract?.Invoke();
            }
        }
    }
}