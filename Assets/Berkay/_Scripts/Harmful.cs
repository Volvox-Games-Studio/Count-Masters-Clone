using UnityEngine;

namespace Berkay._Scripts
{
    public class Harmful : MonoBehaviour
    {
        [SerializeField] private bool selfKill;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                playerController.Kill();
                
                if (selfKill) Destroy(gameObject);
            }
        }
    }
}