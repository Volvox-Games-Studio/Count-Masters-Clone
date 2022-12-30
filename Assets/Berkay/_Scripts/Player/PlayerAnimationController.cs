using System;
using UnityEngine;

namespace Berkay._Scripts.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator animator;


        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            ControlAnimations();
        }

        private void ControlAnimations()
        {
            if (PlayerGroupController.PlayerGroupState == PlayerGroupState.Walking)
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