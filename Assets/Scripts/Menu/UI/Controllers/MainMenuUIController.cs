using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Menu.UI.View;
using Larva.Tools;

namespace Larva.Menu.UI.Controller
{
    public class MainMenuUIController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;
        private readonly AudioManager _audioManager;

        public MainMenuUIController(GameManager gameManager, UIManager uiManager, AudioManager audioManager)
        {
            _gameManager = gameManager;
            _audioManager = audioManager;

            MainMenuCanvasView mainMenuCanvasView = ResourcesLoader.InstantiateAndGetObject<MainMenuCanvasView>(uiManager.PathForUIObjects + uiManager.MainMenuCanvasPath);
            mainMenuCanvasView.Init(StartGame, OpenSettingsPanel, ExitGame);
            AddGameObject(mainMenuCanvasView.gameObject);
        }
        private void StartGame()
        {
            _gameManager.GameState.Value = GameState.Play;
            _audioManager.State.Value = AudioStates.ButtonApply;
        }
        private void OpenSettingsPanel()
        {
            _gameManager.GameState.Value = GameState.Settings;
            _audioManager.State.Value = AudioStates.Button;
        }
        private void ExitGame()
        {
            _gameManager.GameState.Value = GameState.Exit;
            _audioManager.State.Value = AudioStates.ButtonCancel;
        }
    }
}