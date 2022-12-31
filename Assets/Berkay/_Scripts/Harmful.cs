using UnityEngine;
using UnityEngine.Events;

namespace Berkay._Scripts
{
    public class Harmful : MonoBehaviour
    {
        [SerializeField] private bool selfKill;


        [Header("Events")]
        public UnityEvent onBeginInteract;
        public UnityEvent onEndInteract;


        private bool m_Enabled = true;
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                if (!m_Enabled) return;

                onBeginInteract?.Invoke();
                
                playerController.Kill();

                if (selfKill)
                {
                    m_Enabled = false;
                    Destroy(gameObject);
                }

                onEndInteract?.Invoke();
            }
        }
    }
}