using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Game.UI.View;
using Larva.Tools;

namespace Larva.Game.UI.Controller
{
    public class TopPanelController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;
        private readonly GameCanvaslView _topPanelView;

        public TopPanelController(GameManager gameManager, UIManager uiManager)
        {
            _gameManager = gameManager;
            _gameManager.Score.SubscribeOnChange(OnChangeScore);

            _topPanelView = ResourcesLoader.InstantiateAndGetObject<GameCanvaslView>(uiManager.PathForUIObjects + uiManager.GameCanvasPath);
            AddGameObject(_topPanelView.gameObject);
            _topPanelView.Initialized(PauseGame);
        }
        protected override void OnDispose()
        {
            _gameManager.Score.UnSubscribeOnChange(OnChangeScore);

            base.OnDispose();
        }
        private void OnChangeScore() => _topPanelView.OnChangeScoreText(_gameManager.Score.Value);
        private void PauseGame() => _gameManager.GameState.Value = GameState.Pause;
    }
}