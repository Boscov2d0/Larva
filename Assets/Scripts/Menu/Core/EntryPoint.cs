using Larva.Data;
using Larva.Menu.Data;
using Larva.Menu.UI.Controller;
using UnityEngine;

namespace Larva.Menu.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private SaveLoadManager _saveLoadManager;
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private LarvaProfile _larvaProfile;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private VideoManager _videoManager;

        private LoadController _loadController;
        private LocalizationController _localizationController;
        private GameController _gameController;
        private HUDController _HUDController;
        
        private void Start()
        {
            _loadController = new LoadController(_localizationManager, _saveLoadManager, _audioManager, _videoManager);
            _localizationController = new LocalizationController(_localizationManager, _larvaProfile);
            _gameController = new GameController(_gameManager, _videoManager);
            _HUDController = new HUDController(_saveLoadManager, _localizationManager, _gameManager, _uiManager, _audioManager, _videoManager);
        }
        private void OnDestroy()
        {
            _loadController?.Dispose();
            _localizationController?.Dispose();
            _gameController?.Dispose();
            _HUDController?.Dispose();
        }
    }
}