using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Emre
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera mainFollow;
        [SerializeField] private CinemachineVirtualCamera ladderEntering;
        [SerializeField] private CinemachineVirtualCamera ladderCamera;
        [SerializeField] private Transform ladderFocus;


        public static Transform LadderFocus => Instance.ladderFocus;
        

        private static CameraManager Instance => ms_Instance ??= FindObjectOfType<CameraManager>();
        
        
        private static CameraManager ms_Instance;
        

        private void Awake()
        {
            GameEvents.OnReachedFinishLine += OnReachedFinishLine;
        }

        private void OnDestroy()
        {
            GameEvents.OnReachedFinishLine -= OnReachedFinishLine;
        }


        public static void SetLadderFocus(Vector3 position)
        {
            LadderFocus.position = position;
        }
        

        private void OnReachedFinishLine(GameEventResponse response)
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                mainFollow.Priority = 0;
                ladderEntering.Priority = 10;
                ladderCamera.Priority = 0;
                
                
                yield return new WaitForSeconds(3f);
                
                if (response.levelEndingType == LevelEndingType.Ladders)
                {
                    mainFollow.Priority = 0;
                    ladderEntering.Priority = 0;
                    ladderCamera.Priority = 10;
                }
            }
        }
    }
}