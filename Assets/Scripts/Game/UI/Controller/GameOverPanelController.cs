using Larva.Data;
using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Game.UI.View;
using Larva.Tools;

namespace Larva.Game.UI.Controller
{
    public class GameOverPanelController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;
        private readonly AudioManager _audioManager;

        public GameOverPanelController(GameManager gameManager, UIManager uiManager, AudioManager audioManager)
        {
            _gameManager = gameManager;
            _audioManager = audioManager;

            if (_gameManager.Score.Value < 0)
                _gameManager.Score.Value = 0;

            GameOverCanvasView gameOverPanelview = ResourcesLoader.InstantiateAndGetObject<GameOverCanvasView>(uiManager.PathForUIObjects + uiManager.GameOverCanvasPath);
            AddGameObject(gameOverPanelview.gameObject);
            gameOverPanelview.Initialize(RestarGame, ExitToMainMenu, _gameManager.Score.Value);
        }
        private void RestarGame()
        {
            _gameManager.GameState.Value = GameState.Restart;
            _audioManager.State.Value = AudioKeys.AudioStates.ButtonApply;
        }
        private void ExitToMainMenu()
        {
            _gameManager.GameState.Value = GameState.Exit;
            _audioManager.State.Value = AudioKeys.AudioStates.ButtonApply;
        }
    }
}