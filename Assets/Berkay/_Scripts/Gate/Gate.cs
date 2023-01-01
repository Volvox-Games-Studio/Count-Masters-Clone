using Emre;
using UnityEngine;

namespace Berkay._Scripts.Gate
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private AudioSource dashSound;
        [SerializeField] private Door left;
        [SerializeField] private Door right;
        
        
        private void OnDoorDashed(GameEventResponse data)
        {
            ControlPairs();
            dashSound.Play();
        }

        
        private void ControlPairs()
        {
            if (left.isEntered)
            {
                right.BeUnInteractable();
            }
            else if (right.isEntered)
            {
                left.BeUnInteractable();
            }
        }
        
        private void OnEnable()
        {
            GameEvents.OnDoorDashed += OnDoorDashed;
        }

        private void OnDisable()
        {
            GameEvents.OnDoorDashed -= OnDoorDashed;
        }
        
    }
}