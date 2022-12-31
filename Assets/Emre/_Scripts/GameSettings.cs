using UnityEngine;

namespace Emre
{
    public class GameSettings : MonoBehaviour
    {
        [SerializeField, Min(0)] private int mobileFps = 60;
        [SerializeField] private bool dontAffectEditorFps = true;
        

        private void Awake()
        {
            SetMobileFps();
        }


        private void SetMobileFps()
        {
            if (dontAffectEditorFps && Application.isEditor) return;

            Application.targetFrameRate = mobileFps;
        }
    }
}