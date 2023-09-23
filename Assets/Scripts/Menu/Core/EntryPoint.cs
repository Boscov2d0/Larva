using Larva.Menu.Data;
using Larva.Menu.UI.Controller;
using UnityEngine;

namespace Larva.Menu.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private UIManager _uiManager;

        private GameController _gameController;
        private HUDController _HUDController;
        
        private void Start()
        {
            _gameController = new GameController(_gameManager);
            _HUDController = new HUDController(_gameManager, _uiManager);
        }
        private void OnDestroy()
        {
            _gameController?.Dispose();
            _HUDController?.Dispose();
        }
    }
}