using UnityEngine;
using UnityEngine.EventSystems;

namespace Emre
{
    public class GameStarter : MonoBehaviour, IBeginDragHandler
    {
        private void Awake()
        {
            GameEvents.OnBuildToggle += OnBuildToggle;
        }

        private void OnDestroy()
        {
            GameEvents.OnBuildToggle -= OnBuildToggle;
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            enabled = false;
            GameEvents.RaiseGameStarted();
        }


        private void OnBuildToggle(GameEventResponse response)
        {
            enabled = !response.openBuild;
        }
    }
}
