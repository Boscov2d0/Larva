using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Game.UI.View;
using Larva.Tools;

namespace Larva.Game.UI.Controller
{
    public class GameOverPanelController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;

        public GameOverPanelController(GameManager gameManager, UIManager uiManager)
        {
            _gameManager = gameManager;

            GameOverCanvasView gameOverPanelview = ResourcesLoader.InstantiateAndGetObject<GameOverCanvasView>(uiManager.PathForUIObjects + uiManager.GameOverCanvasPath);
            AddGameObject(gameOverPanelview.gameObject);
            gameOverPanelview.Initialized(RestarGame, ExitToMainMenu, _gameManager.Score.Value);
        }
        private void RestarGame() => _gameManager.GameState.Value = GameState.Restart;
        private void ExitToMainMenu() => _gameManager.GameState.Value = GameState.Exit;
    }
}