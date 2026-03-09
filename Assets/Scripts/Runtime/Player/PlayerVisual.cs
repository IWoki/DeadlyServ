using UnityEngine;

namespace Woks.DeadlyServ.Scripts.Runtime.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        [Header("References")]
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private PlayerMovement _playerMovement;

        [Header("Settings")]
        [SerializeField] private float _minimalMovingSpeed = 0.1f;

        private static readonly int IsWalkingHash = Animator.StringToHash("IsWalking");

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _playerMovement = GetComponent<PlayerMovement>();

            if (_playerMovement != null)
            {
                _playerMovement.OnMovementChanged += HandleMovementVisual;
            }
        }

        private void OnDestroy()
        {
            if (_playerMovement != null)
            {
                _playerMovement.OnMovementChanged -= HandleMovementVisual;
            }
        }

        private void HandleMovementVisual(Vector2 moveVelocity, float magnitude)
        {
            bool isWalking = magnitude > _minimalMovingSpeed;
            
            _animator.SetBool(IsWalkingHash, isWalking);

            if (magnitude > 0.01f) 
            {
                bool flipX = moveVelocity.x < 0;
                _spriteRenderer.flipX = flipX;
            }
        }
    }
}