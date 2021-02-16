using GameController;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Menu
{
    public class MenuTapScrip : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Type _type = default;
        [SerializeField] private Canvas _scoreCanvas = default;
        [SerializeField] private Canvas _menuCanvas = default;
        [SerializeField] private Canvas _winCanvas = default;
        
        private enum Type
        {
            MENU,
            WIN
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            switch (_type)
            {
                case Type.MENU:
                    StartGame();
                    break;
                case Type.WIN:
                    OpenMenu();
                    break;
            }
        }

        private void StartGame()
        {
            _scoreCanvas.gameObject.SetActive(true);
            _menuCanvas.gameObject.SetActive(false);
            GameManager.Instance.StartLevel();
        }

        private void OpenMenu()
        {
            _menuCanvas.gameObject.SetActive(true);
            _winCanvas.gameObject.SetActive(false);
        }
    }
}
