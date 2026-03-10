using UnityEngine;

namespace Woks.DeadlyServ.Scripts.Runtime.Interaction
{
    public class InteractableObject : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _objectName = "Object";
        public void Interact()
        {
            Debug.Log($"Interact with {_objectName}");
        }

        public string GetObjectName()
        {
            return _objectName;
        }
    }
}