using TMPro;
using UnityEngine;

namespace Emre
{
    public class Counter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text countField;
        
        
        public void UpdateCount(int count)
        {
            countField.text = count.ToString();
        }
    }
}