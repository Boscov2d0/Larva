using Larva.Data;
using Larva.Menu.Data;
using Larva.Menu.UI.Controller;
using UnityEngine;

namespace Larva.Menu.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private LarvaProfile _larvaProfile;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private AudioManager _audioManager;

        private LocalizationController _localizationController;
        private GameController _gameController;
        private HUDController _HUDController;
        
        private void Start()
        {
            _localizationController = new LocalizationController(_localizationManager, _larvaProfile);
            _gameController = new GameController(_gameManager);
            _HUDController = new HUDController(_localizationManager, _gameManager, _uiManager, _audioManager);
        }
        private void OnDestroy()
        {
            _localizationController?.Dispose();
            _gameController?.Dispose();
            _HUDController?.Dispose();
        }
    }
}