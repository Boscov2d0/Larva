using Larva.Data;
using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Menu.UI.View;
using Larva.Tools;

using static Larva.Tools.AudioKeys;

namespace Larva.Menu.UI.Controller
{
    public class MenuUIController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;
        private readonly AudioManager _audioManager;

        public MenuUIController(GameManager gameManager, UIManager uiManager, AudioManager audioManager)
        {
            _gameManager = gameManager;
            _audioManager = audioManager;

            MenuCanvasView menuCanvasView = ResourcesLoader.InstantiateAndGetObject<MenuCanvasView>(uiManager.PathForUIObjects + uiManager.MainMenuCanvasPath);
            menuCanvasView.Initialize(StartGame, OpenLarvaHouse, OpenSettingsPanel, ExitGame);
            AddGameObject(menuCanvasView.gameObject);
        }
        private void StartGame()
        {
            _gameManager.GameState.Value = GameState.Play;
            _audioManager.State.Value = AudioStates.ButtonApply;
        }
        private void OpenLarvaHouse()
        {
            _gameManager.GameState.Value = GameState.LarvaHouse;
            _audioManager.State.Value = AudioStates.Button;
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