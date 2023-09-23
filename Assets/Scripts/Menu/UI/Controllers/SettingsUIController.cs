using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Menu.UI.View;
using Larva.Tools;

namespace Larva.Menu.UI.Controller
{
    public class SettingsUIController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;

        public SettingsUIController(GameManager gameManager, UIManager uiManager)
        {
            _gameManager = gameManager;

            SettingsCanvasView settingsCanvasView = ResourcesLoader.InstantiateAndGetObject<SettingsCanvasView>(uiManager.PathForUIObjects + uiManager.SettingsCanvasPath);
            AddGameObject(settingsCanvasView.gameObject);
            settingsCanvasView.Init(Back);
        }
        private void Back() => _gameManager.GameState.Value = GameState.MainMenu;
    }
}