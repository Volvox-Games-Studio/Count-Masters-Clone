using UnityEngine;

namespace Berkay._Scripts.Enemy
{
    public static class BattleUtils
    {
        public const float BattleInvadeInterval = 0.01f;
        
        
        public static Vector3 GetRandomOffset()
        {
            return new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.1f, 0.1f));
        }
    }
}