using UnityEngine;

namespace Woks.DeadlyServ.Scripts.Runtime.Player
{
    public class GameInput : MonoBehaviour
    {
        public event System.Action<Vector2> OnMoveVectorChanged;
        public event System.Action OnInteractPressed;

        private Vector2 _moveInput;

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            _moveInput = new Vector2(horizontal, vertical).normalized;

            OnMoveVectorChanged?.Invoke(_moveInput);

            if (Input.GetKeyDown(KeyCode.E))
            {
                OnInteractPressed?.Invoke();
            }
        }

        public Vector2 GetMovementVector()
        {
            return _moveInput;
        }

        public bool IsMoving()
        {
            return _moveInput != Vector2.zero;
        }
    }
}