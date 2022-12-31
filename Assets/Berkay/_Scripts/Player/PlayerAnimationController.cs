using Emre;
using UnityEngine;

namespace Berkay._Scripts.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;


        private void Awake()
        {
            GameEvents.OnPlayerGroupStateChanged += OnPlayerGroupStateChanged;
        }

        private void OnDestroy()
        {
            GameEvents.OnPlayerGroupStateChanged -= OnPlayerGroupStateChanged;
        }


        private void OnPlayerGroupStateChanged(GameEventResponse response)
        {
            ControlAnimations(response.playerGroupState);
        }
        

        private void ControlAnimations(PlayerGroupState groupState)
        {
            if (groupState == PlayerGroupState.Walking)
            {
                PlayRun();
            }
            else
            {
                PlayIdle();
            }
        }
        
        private void PlayRun()
        {
            animator.SetBool("isRun", true);
        }

        private void PlayIdle()
        {
            animator.SetBool("isRun", false);
        }
    }
}