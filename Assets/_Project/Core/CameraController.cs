using Cinemachine;
using UnityEngine;

namespace TapTapTap.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        public void Initialize(Entity entity)
        {
            virtualCamera.Follow = entity.transform;
        }
    }
}