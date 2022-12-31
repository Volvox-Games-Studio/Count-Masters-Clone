using System;
using UnityEditor;
using UnityEngine;

namespace Emre
{
    [CreateAssetMenu(menuName = nameof(UpgradeDataProvider))]
    public class UpgradeDataProvider : ScriptableObject
    {
        [SerializeField] private UpgradePair<float, float>[] values;
        [SerializeField] private float lastLevelValue;


        public int LevelCount => values.Length + 1;
        

        private void OnValidate()
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i].name = $"Level {i + 1} -> Level {i + 2} | Cost: {GetCost(i + 1)} -> {GetCost(i + 2)} | Value: {GetValue(i + 1)} -> {GetValue(i + 2)}";
            }
        }


        public float GetCost(int level)
        {
            return level == LevelCount 
                ? float.NaN 
                : values[level - 1].cost;
        }
        
        public float GetValue(int level)
        {
            return level == LevelCount 
                ? lastLevelValue 
                : values[level - 1].value;
        }



        [Serializable]
        private struct UpgradePair<TCost, TValue>
        {
            public string name;
            public TCost cost;
            public TValue value;
        }
    }
}