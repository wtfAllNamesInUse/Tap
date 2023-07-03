using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class DistanceEvaluator : ITickable
    {
        public float Distance => distance;

        private readonly GameStateData gameStateData;

        private float distance;

        public DistanceEvaluator(
            GameStateData gameStateData)
        {
            this.gameStateData = gameStateData;
        }

        public void Tick()
        {
            if (gameStateData.Player == null) {
                return;
            }

            distance = Mathf.Abs(gameStateData.OriginalPlayerPosition.x - gameStateData.Player.transform.position.x);
        }
    }
}