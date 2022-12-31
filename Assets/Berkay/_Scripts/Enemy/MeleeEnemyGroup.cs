using System.Collections.Generic;
using Emre;
using UnityEngine;

namespace Berkay._Scripts.Enemy
{
    public class MeleeEnemyGroup : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Interactable battleTrigger;
        [SerializeField] private Transform unitsParent;
        [SerializeField] private WorldSpaceCounter counter;
        [SerializeField] private MeleeEnemy prefab;
        [SerializeField] private Decal decal;
        
        [Header("Values")]
        [SerializeField] private int initialSize;


        private float Radius => GroupUtils.IndexToLocalPosition(Size - 1).magnitude;
        private int Size => m_Units.Count;
        

        private readonly List<MeleeEnemy> m_Units = new();


        private void Start()
        {
            SpawnGroup();
            decal.SetRadius(Radius + 1f);
        }


        public void BeginBattle()
        {
            var delay = BattleUtils.BattleInvadeInterval;
            var position = battleTrigger.transform.position;

            foreach (var unit in m_Units)
            {
                var offset = BattleUtils.GetRandomOffset();
                unit.Move(position + offset, delay);
                delay += BattleUtils.BattleInvadeInterval;
            }
        }
        
        public void Add(MeleeEnemy enemy)
        {
            m_Units.Add(enemy);
            UpdateCounter();
        }
        
        public void Remove(MeleeEnemy enemy)
        {
            m_Units.Remove(enemy);
            UpdateCounter();

            if (Size <= 0)
            {
                GameEvents.RaiseBattleEnd(true);
            }
        }
        

        private void SpawnGroup()
        {
            for (int i = 0; i < initialSize; i++)
            {
                var newUnit = Instantiate(prefab, unitsParent);
                newUnit.Inject(this);
                Add(newUnit);
            }
            
            FormatGroup();
        }
        
        private void FormatGroup()
        {
            for (int i = 0; i < m_Units.Count; i++)
            {
                var unit = m_Units[i];
                var localPosition = GroupUtils.IndexToLocalPosition(i);
                unit.Format(localPosition);
            }
        }

        private void UpdateCounter()
        {
            counter.SetCount(Size);

            if (Size <= 0)
            {
                counter.Hide();
                decal.Hide();
            }
        }
    }
}