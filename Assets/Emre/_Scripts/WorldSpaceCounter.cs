using TMPro;
using UnityEngine;

namespace Emre
{
    public class WorldSpaceCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text countField;


        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void SetCount(int count)
        {
            countField.text = count.ToString();
        }
    }
}