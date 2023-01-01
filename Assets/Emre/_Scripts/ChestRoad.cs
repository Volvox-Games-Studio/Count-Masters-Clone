using DG.Tweening;
using UnityEngine;

namespace Emre
{
    public class ChestRoad : MonoBehaviour
    {
        [SerializeField] private AudioSource openChestSound;
        [SerializeField] private Transform chestLid;
        [SerializeField] private GameObject chestParticles;


        public void OnReachChest()
        {
            chestLid.DOLocalRotate(Vector3.forward * -112, 0.5f)
                .SetEase(Ease.OutBounce)
                .OnComplete(() =>
                {
                    chestParticles.SetActive(true);
                });
            
            GameEvents.RaiseLevelComplete(LevelCompleteType.ChestOpen);
            openChestSound.Play();
        }
    }
}