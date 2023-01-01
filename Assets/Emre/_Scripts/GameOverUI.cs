using UnityEngine;

namespace Emre
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;


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
            gameOverPanel.SetActive(true);
        }
        
        
        public void OnClickedTryAgain()
        {
            LevelManager.RestartLevel();
        }
    }
}