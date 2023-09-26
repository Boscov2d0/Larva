using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Tools;

namespace Larva.Menu.UI.Controller
{
    public class HUDController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;
        private readonly UIManager _uiManager;
        private readonly AudioManager _audioManager;

        private MainMenuUIController _mainMenuUIController;
        private PCSettingsUIController _settingsPanelController;

        public HUDController(GameManager gameManager, UIManager uiManager, AudioManager audioManager)
        {
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
                    _mainMenuUIController = new MainMenuUIController(_gameManager, _uiManager, _audioManager);
                    break;
                case GameState.Settings:
#if UNITY_ANDROID && !UNITY_EDITOR
                    _settingsPanelController = new PhoneSettingsUIController(_gameManager, _uiManager, _audioManager);
#else
                    _settingsPanelController = new PCSettingsUIController(_gameManager, _uiManager, _audioManager);
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