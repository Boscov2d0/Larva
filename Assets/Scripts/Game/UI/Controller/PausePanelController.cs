using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Game.UI.View;
using Larva.Tools;

namespace Larva.Game.UI.Controller
{
    public class PausePanelController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;
        private readonly PauseCanvasView _pausePanelView;

        public PausePanelController(GameManager gameManager, UIManager uiManager)
        {
            _gameManager = gameManager;

            _pausePanelView = ResourcesLoader.InstantiateAndGetObject<PauseCanvasView>(uiManager.PathForUIObjects + uiManager.PauseCanvasPath);
            AddGameObject(_pausePanelView.gameObject);

            _pausePanelView.Initialized(ContinueGame, ExitToMainMenu);
        }
        private void ContinueGame()
        {
            _gameManager.GameState.Value = GameState.Game;
        }
        private void ExitToMainMenu()
        {
            _gameManager.GameState.Value = GameState.Exit;
        }
    }
}