using TMPro;
using UnityEngine;

namespace Emre
{
    public class WorldSpaceCounter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer background;
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

        public void SetColor(Color color)
        {
            background.color = color;
        }
    }
}