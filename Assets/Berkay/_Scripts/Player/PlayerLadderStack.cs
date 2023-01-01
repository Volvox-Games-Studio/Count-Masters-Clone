using System.Collections.Generic;
using Emre;
using UnityEngine;

namespace Berkay._Scripts.Player
{
    public class PlayerLadderStack : MonoBehaviour
    {
        private void Awake()
        {
            GameEvents.OnStartLevelEnding += OnStartLevelEnding;
        }

        private void OnDestroy()
        {
            GameEvents.OnStartLevelEnding -= OnStartLevelEnding;
        }


        private void OnStartLevelEnding(GameEventResponse response)
        {
            if (response.levelEndingType != LevelEndingType.Ladders) return;
            
            var players = PlayerSpawner.players;
            var stairs = CalculateStairs(players.Count);
            var counter = 0;

            for (var i = 0; i < stairs.Count; i++)
            {
                var playerCount = stairs[i];

                for (var j = 0; j < playerCount; j++)
                {
                    const float playerSizeX = 0.8f;
                    const float playerSizeY = 2f;
                    
                    var totalHalfWidth = playerCount * playerSizeX * 0.5f;
                    var t = playerCount == 1 ? 0.5f : j / (playerCount - 1f);
                    var x = Mathf.Lerp(-totalHalfWidth, totalHalfWidth, t);
                    players[counter].LocalMove(new Vector3(x, (stairs.Count - i - 1) * playerSizeY, 0f));
                    counter += 1;
                }
            }
        }
        
        
        private List<int> CalculateStairs(int count)
        {
            var output = new List<int>();
            
            if (count <= 0) return output;
            
            var countCopy = count;
            var counter = 0;

            while (true)
            {
                var width = (counter / 2) + 1;

                if (countCopy >= width)
                {
                    countCopy -= width;
                    output.Add(width);
                    counter += 1;
                    continue;
                }

                if (countCopy < output[^1])
                {
                    output[^1] += countCopy;
                    break;
                }
                
                output.Add(countCopy);
                break;
            }

            var lastHalf = output[^1] / 2;

            if (lastHalf >= output[^2])
            {
                var otherHalf = output[^1] - lastHalf;
                output[^1] -= otherHalf;
                output.Add(otherHalf);
            }

            return output;
        }
    }
}