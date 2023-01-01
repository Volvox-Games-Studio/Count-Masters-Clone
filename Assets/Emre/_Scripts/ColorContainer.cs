using UnityEngine;

namespace Emre
{
    [CreateAssetMenu]
    public class ColorContainer : ScriptableObject
    {
        [SerializeField] private Color[] colors;


        public Color this[int index] => colors[index];
    }
}