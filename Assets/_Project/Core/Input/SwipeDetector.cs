using System;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class SwipeDetector : ITickable
    {
        public event Action<SwipeDirection> OnSwipe;

        private Vector3 startPosition;
        private Vector3 endPosition;

        private DateTime startTime;
        private DateTime endTime;

        public const float SwipeThreshold = 20.0f;

        public void Tick()
        {
            var isButtonDown = Input.GetMouseButtonDown(0);
            if (isButtonDown) {
                startPosition = Input.mousePosition;
                startTime = DateTime.Now;
            }

            var isButtonUp = Input.GetMouseButtonUp(0);
            if (!isButtonUp) {
                return;
            }

            endPosition = Input.mousePosition;
            endTime = DateTime.Now;
            CheckSwipe();
        }

        private void CheckSwipe()
        {
            var duration = endTime - startTime;
            if (duration.TotalSeconds > 0.3d) {
                return;
            }

            var xChange = endPosition.x - startPosition.x;
            var yChange = endPosition.y - startPosition.y;
            var absXChange = Mathf.Abs(xChange);
            var absYChange = Mathf.Abs(yChange);

            var swipeDirection = SwipeDirection.None;
            if (absXChange > SwipeThreshold) {
                if (xChange > 0) {
                    swipeDirection = SwipeDirection.LeftToRight;
                }
                else if (xChange < 0) {
                    swipeDirection = SwipeDirection.RightToLeft;
                }
            }
            else if (absYChange > SwipeThreshold) {
                if (yChange > 0) {
                    swipeDirection = SwipeDirection.DownToUp;
                }
                else {
                    swipeDirection = SwipeDirection.UpToDown;
                }
            }

            if (swipeDirection != SwipeDirection.None) {
                OnSwipe?.Invoke(swipeDirection);
            }
        }
    }
}