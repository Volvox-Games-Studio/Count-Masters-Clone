using Emre;
using UnityEngine;

namespace Berkay._Scripts.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private static readonly int IsRun = Animator.StringToHash("isRun");
        
        
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
            if (groupState is PlayerGroupState.Walking or PlayerGroupState.Fighting)
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
            animator.SetBool(IsRun, true);
        }

        private void PlayIdle()
        {
            animator.SetBool(IsRun, false);
        }
    }
}