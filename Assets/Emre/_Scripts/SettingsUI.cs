using UnityEngine;
using UnityEngine.Events;

namespace Emre
{
    public class SettingsUI : MonoBehaviour
    {
        private const string SoundToggleKey = "Sound_Toggle";
        private const string VibrationToggleKey = "Vibration_Toggle";


        public UnityEvent<bool> onLoadedSoundToggle;
        public UnityEvent<bool> onLoadedVibrationToggle;


        public static bool IsSoundOn
        {
            get => PlayerPrefs.GetInt(SoundToggleKey, 1) == 1;
            private set
            {
                PlayerPrefs.SetInt(SoundToggleKey, value ? 1 : 0);
                GameEvents.RaiseSongToggle(value);
            }
        }

        public static bool IsVibrationOn
        {
            get => PlayerPrefs.GetInt(VibrationToggleKey, 1) == 1;
            private set
            {
                PlayerPrefs.SetInt(VibrationToggleKey, value ? 1 : 0);
                GameEvents.RaiseVibrationToggle(value);
            }
        }


        private AudioSource[] Audios => m_Audios ??= FindObjectsOfType<AudioSource>(true);
        
        
        private AudioSource[] m_Audios;
        

        private void Start()
        {
            GameEvents.RaiseLoadedSongToggle(IsSoundOn);
            onLoadedSoundToggle?.Invoke(IsSoundOn);

            GameEvents.RaiseLoadedVibrationToggle(IsVibrationOn);
            onLoadedVibrationToggle?.Invoke(IsVibrationOn);
        }


        public static void OpenVolvoxGamesLink()
        {
            Application.OpenURL("https://www.linkedin.com/company/volvox-games/");
        }
        

        public void OnSoundToggle(bool isOn)
        {
            IsSoundOn = isOn;
            var volume = isOn ? 1f : 0f;

            foreach (var audio in Audios)
            {
                audio.volume = volume;
            }
        }

        public void OnVibrationToggle(bool isOn)
        {
            IsVibrationOn = isOn;
            
            if (!isOn) return;
            
            Vibrator.Vibrate(100);
        }
    }
}