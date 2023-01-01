using UnityEngine;
using UnityEngine.SceneManagement;

namespace Emre
{
    public class LevelManager : MonoBehaviour
    {
        private const string LevelKey = "Level";
        private const string LevelCycleKey = "Level_Cycle";
        private const int CoinPerLevel = 25;
        private const int GemPerLevel = 1;


        [SerializeField] private GameObject[] levelPrefabs;
        

        public static int Level
        {
            get => PlayerPrefs.GetInt(LevelKey, 1);
            private set => PlayerPrefs.SetInt(LevelKey, value);
        }

        public static int AbsLevel => Level + LevelCycle * LevelCount;
        
        public static int LevelCycle
        {
            get => PlayerPrefs.GetInt(LevelCycleKey, 0);
            private set => PlayerPrefs.SetInt(LevelCycleKey, value);
        }

        public static int EarnedCoin => (int) (AbsLevel * CoinPerLevel * LadderBlock.Multiplier);
        public static int EarnedGem => (int) (AbsLevel * GemPerLevel * LadderBlock.Multiplier);
        
        
        private static LevelManager Instance => ms_Instance ? ms_Instance : FindObjectOfType<LevelManager>();
        private static GameObject LevelPrefab => Instance.levelPrefabs[Level - 1];
        private static int LevelCount => Instance.levelPrefabs.Length;
        
        
        private static LevelManager ms_Instance;


        private void Start()
        {
            SpawnLevel();
        }


        private static void SpawnLevel()
        {
            Instantiate(LevelPrefab, Instance.transform);
            GameEvents.RaiseLevelLoaded(AbsLevel);
        }


        public static void RestartLevel()
        {
            ReloadScene();
        }
        
        public static void NextLevel()
        {
            var nextLevel = Level + 1;

            if (nextLevel > LevelCount)
            {
                Level = 1;
                LevelCycle += 1;
            }

            else
            {
                Level = nextLevel;
            }

            ReloadScene();
        }


        private static void ReloadScene()
        {
            PlayerSpawner.players.Clear();
            SceneManager.LoadScene(0);
        }
    }
}