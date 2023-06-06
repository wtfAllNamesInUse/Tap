using System;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class ClickDetector : ITickable
    {
        public event Action OnTap;

        public void Tick()
        {
            if (Input.GetMouseButtonUp(0)) {
                OnTap?.Invoke();
            }
        }
    }
}