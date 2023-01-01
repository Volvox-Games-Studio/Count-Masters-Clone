using System.Collections;
using TMPro;
using UnityEngine;

namespace Emre
{
    public class LevelComplete : MonoBehaviour
    {
        [SerializeField] private GameObject main;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text earnedCoinField;
        [SerializeField] private TMP_Text earnedGemField;


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
                var earnedCoin = LevelManager.EarnedCoin;
                var earnedGem = LevelManager.EarnedGem;
                
                Balance.AddCoin(earnedCoin);
                Balance.AddGem(earnedGem);

                earnedCoinField.text = $"+{earnedCoin}";
                earnedGemField.text = $"+{earnedGem}";
                
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