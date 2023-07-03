using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class ParallaxNode : MonoBehaviour
    {
        private float startpos;
        private float length;
        
        [SerializeField]
        private float parallaxFactor;

        private Transform cameraTransform;
        private Transform nodeTransform;
        
        [Inject]
        public void Inject(Camera camera)
        {
            cameraTransform = camera.transform;
            nodeTransform = transform;
        }
        
        private void Start()
        {
            startpos = nodeTransform.position.x;
            var sprite = GetComponent<SpriteRenderer>();
            length = sprite.bounds.size.x;
        }

        private void Update()
        {
            var cameraPosition = cameraTransform.position;
            var nodePosition = nodeTransform.position;
            
            var temp = cameraPosition.x * (1 - parallaxFactor);
            var distance = cameraPosition.x * parallaxFactor;

            var newPosition = new Vector3(startpos + distance, nodePosition.y, nodePosition.z);
            nodeTransform.position = newPosition;

            if (temp > startpos + (length / 2)) {
                startpos += length;
            }
            else if (temp < startpos - (length / 2)) {
                startpos -= length;
            }
        }
    }
}