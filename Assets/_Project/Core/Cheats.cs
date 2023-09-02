using System;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class Cheats : ITickable
    {
        private readonly ShowScreenCheats showScreenCheats;

        public Cheats(
            ShowScreenCheats showScreenCheats)
        {
            this.showScreenCheats = showScreenCheats;
        }

        public void Tick()
        {
            CheckKey(KeyCode.B, () => showScreenCheats.ShowBackpackScreen());
        }

        private void CheckKey(KeyCode keyCode, Action action)
        {
            if (Input.GetKeyUp(keyCode)) {
                action?.Invoke();
            }
        }
    }
}