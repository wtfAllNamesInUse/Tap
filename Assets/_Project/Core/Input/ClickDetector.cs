using System;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class ClickDetector : ITickable
    {
        public event Action OnClick;

        public void Tick()
        {
            if (Input.GetMouseButtonUp(0)) {
                OnClick?.Invoke();
            }
        }
    }
}