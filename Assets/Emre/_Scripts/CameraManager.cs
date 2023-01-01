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
        

        private static CameraManager Instance => ms_Instance ? ms_Instance : FindObjectOfType<CameraManager>();
        
        
        private static CameraManager ms_Instance;
        

        private void Awake()
        {
            GameEvents.OnStartLevelEnding += OnStartLevelEnding;
        }

        private void OnDestroy()
        {
            GameEvents.OnStartLevelEnding -= OnStartLevelEnding;
        }


        public static void SetLadderFocus(Vector3 position)
        {
            LadderFocus.position = position;
        }
        
        

        private void OnStartLevelEnding(GameEventResponse response)
        {
            if (response.levelEndingType == LevelEndingType.Cannon)
            {
                CannonFinish();
            }
            else if(response.levelEndingType == LevelEndingType.Ladders)
            {
                LadderFinish();
            }
        }

        private void CannonFinish()
        {
            var cam = GameObject.FindGameObjectWithTag("CannonCamera")
                .GetComponent<CinemachineVirtualCamera>();

            cam.Priority = 10;
            mainFollow.Priority = 0;
            ladderEntering.Priority = 0;
            ladderCamera.Priority = 0;
        }
        
        private void LadderFinish()
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                mainFollow.Priority = 0;
                ladderEntering.Priority = 10;
                ladderCamera.Priority = 0;
                
                
                yield return new WaitForSeconds(3f);

                mainFollow.Priority = 0;
                ladderEntering.Priority = 0;
                ladderCamera.Priority = 10;
                
            }
        }
    }
}