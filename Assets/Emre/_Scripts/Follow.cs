using UnityEngine;

namespace Emre
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private Transform target;


        private Vector3 m_Offset;


        private void Start()
        {
            m_Offset = transform.position - target.position;
        }


        private void LateUpdate()
        {
            transform.position = target.position + m_Offset;
        }
    }
}