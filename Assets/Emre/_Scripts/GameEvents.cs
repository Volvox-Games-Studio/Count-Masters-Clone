using System;
using UnityEngine.Events;

namespace Emre
{
    public static class GameEvents
    {
        public static UnityAction<GameEventResponse> OnLoadedSoundToggle;
        public static UnityAction<GameEventResponse> OnSoundToggle;
        public static UnityAction<GameEventResponse> OnLoadedVibrationToggle;
        public static UnityAction<GameEventResponse> OnVibrationToggle;


        public static void RaiseLoadedSongToggle(bool isOn)
        {
            OnLoadedSoundToggle?.Invoke(new GameEventResponse()
            {
                isSoundOn = isOn
            });
        }
        
        public static void RaiseSongToggle(bool isOn)
        {
            OnSoundToggle?.Invoke(new GameEventResponse()
            {
                isSoundOn = isOn
            });
        }
        
        public static void RaiseLoadedVibrationToggle(bool isOn)
        {
            OnLoadedVibrationToggle?.Invoke(new GameEventResponse()
            {
                isVibrationOn = isOn
            });
        }
        
        public static void RaiseVibrationToggle(bool isOn)
        {
            OnVibrationToggle?.Invoke(new GameEventResponse()
            {
                isVibrationOn = isOn
            });
        }
    }


    [Serializable]
    public struct GameEventResponse
    {
        public bool isSoundOn;
        public bool isVibrationOn;
    }
}