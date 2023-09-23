using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Menu.UI.View;
using Larva.Tools;

namespace Larva.Menu.UI.Controller
{
    public class MainMenuUIController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;

        public MainMenuUIController(GameManager gameManager, UIManager uiManager)
        {
            _gameManager = gameManager;

            MainMenuCanvasView mainMenuCanvasView = ResourcesLoader.InstantiateAndGetObject<MainMenuCanvasView>(uiManager.PathForUIObjects + uiManager.MainMenuCanvasPath);
            mainMenuCanvasView.Init(StartGame, OpenSettingsPanel, ExitGame);
            AddGameObject(mainMenuCanvasView.gameObject);
        }
        private void StartGame() => _gameManager.GameState.Value = GameState.Play;
        private void OpenSettingsPanel() => _gameManager.GameState.Value = GameState.Settings;
        private void ExitGame() => _gameManager.GameState.Value = GameState.Exit;
    }
}