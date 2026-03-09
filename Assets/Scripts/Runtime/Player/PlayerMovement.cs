using UnityEngine;

namespace Woks.DeadlyServ.Scripts.Runtime.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")]
        public float speed = 5f;
        
        private Rigidbody2D _player;
        private Vector2 _moveVelocity;
        private GameInput _gameInput;

        private float _minimalMovingSpeed = 0.1f;

        public event System.Action<Vector2, float> OnMovementChanged;

        void Start()
        {
            _player = GetComponent<Rigidbody2D>();
            
            _player.gravityScale = 0f;
            _player.freezeRotation = true;
            _player.linearDamping = 0f;

            _gameInput = FindFirstObjectByType<GameInput>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (_gameInput == null) return;

            Vector2 moveInput = _gameInput.GetMovementVector();
            _moveVelocity = moveInput * speed;
            _player.linearVelocity = _moveVelocity;
            
            OnMovementChanged?.Invoke(_moveVelocity, _moveVelocity.magnitude);
        }
    }
}