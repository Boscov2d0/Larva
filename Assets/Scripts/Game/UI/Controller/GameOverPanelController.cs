using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Game.UI.View;
using Larva.Tools;
using UnityEngine;

namespace Larva.Game.UI.Controller
{
    public class GameOverPanelController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;
        private readonly GameOverCanvasView _gameOverPanelview;

        public GameOverPanelController(GameManager gameManager, UIManager uiManager)
        {
            _gameManager = gameManager;

            _gameOverPanelview = ResourcesLoader.InstantiateAndGetObject<GameOverCanvasView>(uiManager.PathForUIObjects + uiManager.GameOverCanvasPath);
            AddGameObject(_gameOverPanelview.gameObject);

            _gameOverPanelview.Initialized(RestarGame, ExitToMainMenu, _gameManager.Score.Value);
        }
        private void RestarGame() 
        {
            _gameManager.GameState.Value = GameState.Restart;
        }
        private void ExitToMainMenu() 
        {
            _gameManager.GameState.Value = GameState.Exit;
        }
    }
}