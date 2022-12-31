using UnityEngine;

namespace Emre
{
    public static class GroupUtils
    {
        private const float DistanceFactor = 0.35f;
        private const float RadiusFactor = 1f;
        
        
        public static Vector3 IndexToLocalPosition(int index)
        {
            var x = DistanceFactor * Mathf.Sqrt(index) * Mathf.Cos(index * RadiusFactor);
            var z = DistanceFactor * Mathf.Sqrt(index) * Mathf.Sin(index * RadiusFactor);
            return new Vector3(x, 0f, z);
        }
    }
}