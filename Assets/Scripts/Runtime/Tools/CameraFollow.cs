using UnityEngine;

namespace Woks.DeadlyServ.Scripts.Runtime.Tools
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Target")]
        public Transform target;

        [Header("Settings")]
        public float smoothSpeed = 5f;
        public Vector3 offset = new Vector3(0, 0, -10);

        [Header("Bounds")]
        public float minX, maxX, minY, maxY;

        private Vector3 _velocity;

        private void FixedUpdate()
        {
            if (target == null) return;

            Vector3 desiredPosition = target.position + offset;
            
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, 1f / smoothSpeed);
            
            float clampedX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(smoothedPosition.y, minY, maxY);
            
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Vector3 center = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, 0);
            Vector3 size = new Vector3(maxX - minX, maxY - minY, 0);
            Gizmos.DrawWireCube(center, size);
        }
    }
}