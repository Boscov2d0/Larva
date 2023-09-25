using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Game.UI.View;
using Larva.Tools;

namespace Larva.Game.UI.Controller
{
    public class PausePanelController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;

        public PausePanelController(GameManager gameManager, UIManager uiManager)
        {
            _gameManager = gameManager;

            PauseCanvasView pausePanelView = ResourcesLoader.InstantiateAndGetObject<PauseCanvasView>(uiManager.PathForUIObjects + uiManager.PauseCanvasPath);
            AddGameObject(pausePanelView.gameObject);
            pausePanelView.Initialized(ContinueGame, ExitToMainMenu);
        }
        private void ContinueGame() => _gameManager.GameState.Value = GameState.Game;
        private void ExitToMainMenu() => _gameManager.GameState.Value = GameState.Exit;
    }
}