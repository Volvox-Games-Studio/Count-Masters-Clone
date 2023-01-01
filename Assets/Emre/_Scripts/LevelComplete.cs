using System.Collections;
using TMPro;
using UnityEngine;

namespace Emre
{
    public class LevelComplete : MonoBehaviour
    {
        [SerializeField] private GameObject main;
        [SerializeField] private TMP_Text title;


        private void Awake()
        {
            GameEvents.OnLevelComplete += OnLevelComplete;
        }

        private void OnDestroy()
        {
            GameEvents.OnLevelComplete -= OnLevelComplete;
        }

        
        private void OnLevelComplete(GameEventResponse response)
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                Balance.AddCoin(LevelManager.EarnedCoin);
                Balance.AddGem(LevelManager.EarnedGem);
                
                yield return new WaitForSeconds(2f);
                
                main.SetActive(true);
                title.text = response.levelCompleteType switch
                {
                    LevelCompleteType.ChestOpen => "Chest Opened",
                    _ => "Level Completed"
                };
            }
        }


        public void OnClickedNextLevel()
        {
            LevelManager.NextLevel();
        }
    }
}