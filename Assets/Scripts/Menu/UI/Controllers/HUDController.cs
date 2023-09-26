using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Tools;

namespace Larva.Menu.UI.Controller
{
    public class HUDController : ObjectsDisposer
    {
        private readonly LocalizationManager _localizationManager;
        private readonly GameManager _gameManager;
        private readonly UIManager _uiManager;
        private readonly AudioManager _audioManager;

        private MenuUIController _mainMenuUIController;
        private PCSettingsUIController _settingsPanelController;

        public HUDController(LocalizationManager localizationManager, GameManager gameManager, UIManager uiManager, AudioManager audioManager)
        {
            _localizationManager = localizationManager;
            _gameManager = gameManager;
            _uiManager = uiManager;
            _audioManager = audioManager;

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
                case GameState.Settings:
#if UNITY_ANDROID && !UNITY_EDITOR
                    _settingsPanelController = new PhoneSettingsUIController(_localizationManager, _gameManager, _uiManager, _audioManager);
#else
                    _settingsPanelController = new PCSettingsUIController(_localizationManager, _gameManager, _uiManager, _audioManager);
#endif
                    AddController(_settingsPanelController);
                    break;
            }
        }
        private void DisposeControllers()
        {
            _mainMenuUIController?.Dispose();
            _settingsPanelController?.Dispose();
        }
    }
}