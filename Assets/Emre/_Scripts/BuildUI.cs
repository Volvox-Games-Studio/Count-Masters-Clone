using UnityEngine;

namespace Emre
{
    public class BuildUI : MonoBehaviour
    {
        public void OnClicked(bool open)
        {
            GameEvents.RaiseBuildToggle(open);
        }
    }
}