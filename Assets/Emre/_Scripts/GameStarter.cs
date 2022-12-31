using UnityEngine;
using UnityEngine.EventSystems;

namespace Emre
{
    public class GameStarter : MonoBehaviour, IBeginDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            enabled = false;
            GameEvents.RaiseGameStarted();
        }
    }
}
