using Larva.Data;
using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Game.UI.View;
using Larva.Tools;

namespace Larva.Game.UI.Controller
{
    public class PausePanelController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;
        private readonly AudioManager _audioManager;

        public PausePanelController(GameManager gameManager, UIManager uiManager, AudioManager audioManager)
        {
            _gameManager = gameManager;
            _audioManager = audioManager;

            PauseCanvasView pausePanelView = ResourcesLoader.InstantiateAndGetObject<PauseCanvasView>(uiManager.PathForUIObjects + uiManager.PauseCanvasPath);
            AddGameObject(pausePanelView.gameObject);
            pausePanelView.Initialize(ContinueGame, ExitToMainMenu);
        }
        private void ContinueGame()
        {
            _gameManager.GameState.Value = GameState.Game;
            _audioManager.State.Value = AudioKeys.AudioStates.ButtonCancel;
        }
        private void ExitToMainMenu()
        {
            _gameManager.GameState.Value = GameState.Exit;
            _audioManager.State.Value = AudioKeys.AudioStates.ButtonApply;
        }
    }
}