using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Woks.DeadlyServ.Scripts.Runtime.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Menus")]
        [SerializeField] private GameObject _computerMenu;
        [SerializeField] private GameObject _kitchenMenu;
        [SerializeField] private GameObject _garbageMenu;
        [SerializeField] private GameObject _fridgeMenu;

        private Dictionary<GameObject, bool> _menuStates = new Dictionary<GameObject, bool>();
        private GameObject _currentOpenMenu;

        private void Start()
        {
            InitializeMenu(_computerMenu);
            InitializeMenu(_kitchenMenu);
            InitializeMenu(_garbageMenu);
            InitializeMenu(_fridgeMenu);
        }

        private void InitializeMenu(GameObject menu)
        {
            if (menu != null)
            {
                menu.SetActive(false);
                _menuStates[menu] = false;
            }
        }

        public void OpenMenu(GameObject menuToOpen)
        {

            if (_menuStates.TryGetValue(menuToOpen, out bool isOpen) && isOpen)
            {
                CloseMenu(menuToOpen);
                return;
            }

            if (_currentOpenMenu != null)
            {
                CloseMenu(_currentOpenMenu);
            }

            menuToOpen.SetActive(true);
            _menuStates[menuToOpen] = true;
            _currentOpenMenu = menuToOpen;
            Time.timeScale = 0f;
        }

        public void CloseMenu(GameObject menuToClose)
        {
            menuToClose.SetActive(false);
            _menuStates[menuToClose] = false;
            
            if (_currentOpenMenu == menuToClose)
            {
                _currentOpenMenu = null;
            }

            if (!IsAnyMenuOpen())
            {
                Time.timeScale = 1f;
            }
        }

        public void CloseAllMenus()
        {
            var menusToClose = _menuStates.Keys.ToList();
            
            foreach (var menu in menusToClose)
            {
                if (menu != null)
                {
                    menu.SetActive(false);
                    _menuStates[menu] = false;
                }
            }
            
            _currentOpenMenu = null;
            Time.timeScale = 1f;
        }

        private bool IsAnyMenuOpen()
        {
            foreach (var state in _menuStates.Values)
            {
                if (state) return true;
            }
            return false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseAllMenus();
            }
        }
    }
}