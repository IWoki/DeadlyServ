using UnityEngine;
using Woks.DeadlyServ.Scripts.Runtime.UI;

namespace Woks.DeadlyServ.Scripts.Runtime.Interaction
{
    public class InteractableObject : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _objectName = "Object";
        
        [Header("UI References")]
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private GameObject _menuToOpen;

        private void Start()
        {            
            if (_uiManager == null)
            {
                _uiManager = FindFirstObjectByType<UIManager>();
            }
        }

        public void Interact()
        {
            if (_uiManager == null)
            {
                _uiManager = FindFirstObjectByType<UIManager>();
            }

            
            _uiManager.OpenMenu(_menuToOpen);
        }

        public string GetObjectName()
        {
            return _objectName;
        }
    }
}