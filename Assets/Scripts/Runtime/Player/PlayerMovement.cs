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

        void Start()
        {
            _player = GetComponent<Rigidbody2D>();
            
            _player.gravityScale = 0f;
            _player.freezeRotation = true;
            _player.linearDamping = 0f;

            _gameInput = FindFirstObjectByType<GameInput>();
        }

        void Update()
        {
            if (_gameInput == null) return;

            Vector2 moveInput = _gameInput.GetMovementVector();
            _moveVelocity = moveInput * speed;
        }

        private void FixedUpdate()
        {
            _player.linearVelocity = _moveVelocity;
        }
    }
}