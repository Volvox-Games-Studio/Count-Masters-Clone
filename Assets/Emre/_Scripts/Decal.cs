using UnityEngine;

namespace Emre
{
    public class Decal : MonoBehaviour
    {
        [SerializeField] private new SpriteRenderer renderer;
        [SerializeField] private Transform main;
        [SerializeField] private Color color;
        [SerializeField, Min(0f)] private float radius;


        private void OnValidate()
        {
            renderer.color = color;
            main.localScale = Vector3.one * (radius * 2f);
        }


        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetRadius(float newRadius)
        {
            radius = newRadius;
            main.localScale = Vector3.one * (radius * 2f);
        }
    }
}