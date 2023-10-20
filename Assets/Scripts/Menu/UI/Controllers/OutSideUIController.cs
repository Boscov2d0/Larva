using Larva.Data;
using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Tools;

namespace Larva.Menu.UI.Controller
{
    public class OutSideUIController : ObjectsDisposer
    {
        private readonly LocalizationManager _localizationManager;
        private readonly SaveLoadManager _saveLoadManager;
        private readonly GameManager _gameManager;
        private readonly UIManager _uiManager;
        private readonly AudioManager _audioManager;
        private readonly VideoManager _videoManager;
        private readonly House.Data.HouseManager _houseManager;

        private MenuUIController _mainMenuUIController;
        private SettingsUIController _settingsUIController;
        private GameInstructionUIController _gameInstructionUIController;

        public OutSideUIController(LocalizationManager localizationManager, SaveLoadManager saveLoadManager,
                             GameManager gameManager, UIManager uiManager,
                             AudioManager audioManager, VideoManager videoManager,
                             House.Data.HouseManager houseManager) 
        {
            _localizationManager = localizationManager;
            _saveLoadManager = saveLoadManager;
            _gameManager = gameManager;
            _uiManager = uiManager;
            _audioManager = audioManager;
            _videoManager = videoManager;
            _houseManager = houseManager;

            _gameManager.GameState.SubscribeOnChange(OnChangeState);
            _gameManager.GameState.Value = GameState.Menu;
        }
        protected override void OnDispose()
        {
            _gameManager.GameState.UnSubscribeOnChange(OnChangeState);

            DisposeControllers();

            base.OnDispose();
        }
        private void OnChangeState()
        {
            DisposeControllers();

            switch (_gameManager.GameState.Value)
            {
                case GameState.Menu:
                    _mainMenuUIController = new MenuUIController(_gameManager, _uiManager, _audioManager);
                    break;
                case GameState.Instructions:
                    _gameInstructionUIController = new GameInstructionUIController(_gameManager, _uiManager, _audioManager);
                    break;
                case GameState.Settings:
#if UNITY_ANDROID || UNITY_WEBGL && !UNITY_EDITOR
                    _settingsUIController = new PhoneSettingsUIController(_localizationManager, _saveLoadManager, 
                                                                          _gameManager, _uiManager,
                                                                          _audioManager, _houseManager);
#else
                    _settingsUIController = new PCSettingsUIController(_localizationManager, _saveLoadManager,
                                                                       _gameManager, _uiManager,
                                                                       _audioManager, _videoManager,
                                                                       _houseManager);
#endif
                    AddController(_settingsUIController);
                    break;
            }
        }
        private void DisposeControllers()
        {
            _mainMenuUIController?.Dispose();
            _settingsUIController?.Dispose();
            _gameInstructionUIController?.Dispose();
        }
    }
}