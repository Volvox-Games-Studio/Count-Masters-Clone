using System.Collections;
using UnityEngine;

namespace Emre
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private AudioSource loseSound;


        private void Awake()
        {
            GameEvents.OnGameOver += OnGameOver;
        }

        private void OnDestroy()
        {
            GameEvents.OnGameOver -= OnGameOver;
        }


        private void OnGameOver(GameEventResponse response)
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                yield return new WaitForSeconds(1.5f);
                
                gameOverPanel.SetActive(true);
                loseSound.Play();
            }
        }
        
        
        public void OnClickedTryAgain()
        {
            LevelManager.RestartLevel();
        }
    }
}