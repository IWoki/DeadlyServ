using UnityEngine;
using Woks.DeadlyServ.Scripts.Runtime.Interaction;

namespace Woks.DeadlyServ.Scripts.Runtime.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Vector2 _interactionBoxSize = new Vector2(1f, 1f);
        [SerializeField] private Vector2 _interactionOffset = Vector2.zero;

        private GameInput _gameInput;
        private InteractableObject _currentInteractable;
        private BoxCollider2D _playerCollider;

        private void Start()
        {
            Debug.Log("[PlayerInteraction] Start вызван");
            
            _gameInput = FindFirstObjectByType<GameInput>();
            _playerCollider = GetComponent<BoxCollider2D>();

            if (_gameInput != null)
            {
                Debug.Log("[PlayerInteraction] GameInput найден!");
                _gameInput.OnInteractPressed += HandleInteract;
            }
        }

        private void OnDestroy()
        {
            if (_gameInput != null)
            {
                _gameInput.OnInteractPressed -= HandleInteract;
            }
        }

        private void Update()
        {
            CheckNearbyInteractable();
        }

        private void CheckNearbyInteractable()
        {

            Vector2 worldCenter = (Vector2)transform.position + _interactionOffset;
            
            Collider2D[] colliders = Physics2D.OverlapBoxAll(
                worldCenter, 
                _interactionBoxSize, 
                0f
            );

            _currentInteractable = null;

            foreach (var collider in colliders)
            {
                if (collider.gameObject == gameObject)
                {
                    continue;
                }
                
                InteractableObject interactable = collider.GetComponent<InteractableObject>();
                if (interactable != null)
                {
                    _currentInteractable = interactable;
                    break;
                }
            }
        }

        private void HandleInteract()
        {
            Debug.Log("[PlayerInteraction] HandleInteract вызван!");
            
            if (_currentInteractable != null)
            {
                Debug.Log($"[PlayerInteraction] Взаимодействие с: {_currentInteractable.GetObjectName()}");
                _currentInteractable.Interact();
            }
            else
            {
                Debug.LogWarning("[PlayerInteraction] Нет объекта для взаимодействия рядом. Подойдите ближе!");
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            
            Vector2 worldCenter = (Vector2)transform.position + _interactionOffset;
            
            Gizmos.DrawWireCube(worldCenter, _interactionBoxSize);
            
            Gizmos.color = Color.white;
            Gizmos.DrawLine(worldCenter, worldCenter + (Vector2.up * _interactionBoxSize.y / 2 + Vector2.right * 0.5f));
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            
            Gizmos.color = _currentInteractable != null ? Color.green : Color.yellow;
            
            Vector2 worldCenter = (Vector2)transform.position + _interactionOffset;
            Gizmos.DrawWireCube(worldCenter, _interactionBoxSize);
        }
    }
}