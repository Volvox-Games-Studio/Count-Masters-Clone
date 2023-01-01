using UnityEngine;

namespace Emre
{
    public class LadderLevelEnding : MonoBehaviour
    {
        [SerializeField] private LadderBlock prefab;
        [SerializeField] private ChestRoad chestRoadPrefab;
        [SerializeField] private Color[] colors;
        [SerializeField] private int stepCount;
        [SerializeField] private float startWidth;
        [SerializeField] private float endWidth;
        [SerializeField] private float startMultiplier;
        [SerializeField] private float endMultiplier;


        private Vector3 SubOffset => new Vector3(0f, prefab.Size.y, prefab.Size.z);


        private void Awake()
        {
            GameEvents.OnReachedFinishLine += OnReachedFinishLine;
        }

        private void Start()
        {
            var position = Vector3.zero;
            
            for (int i = 0; i < stepCount; i++)
            {
                var t = i / (stepCount - 1f);
                var multiplier = Mathf.Lerp(startMultiplier, endMultiplier, t);
                var width = Mathf.Lerp(startWidth, endWidth, t);
                var offset = SubOffset * (i + 0.5f);
                var color = colors[i % colors.Length];
                var newLadderBlock = Instantiate(prefab, transform);
                position = transform.position + offset;
                newLadderBlock.SetPosition(position);
                newLadderBlock.SetMultiplier(multiplier);
                newLadderBlock.SetWidth(width);
                newLadderBlock.SetColor(color);
            }

            var chestRoad = Instantiate(chestRoadPrefab, transform);
            chestRoad.transform.position = new Vector3(0f, position.y, position.z + SubOffset.z);
        }

        private void OnDestroy()
        {
            GameEvents.OnReachedFinishLine -= OnReachedFinishLine;
        }


        private void OnReachedFinishLine(GameEventResponse response)
        {
            GameEvents.RaiseStartLevelEnding(LevelEndingType.Ladders);
        }
    }
}