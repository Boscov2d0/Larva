using Larva.Data;
using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Game.UI.View;
using Larva.Tools;

using static Larva.Tools.AudioKeys;

namespace Larva.Game.UI.Controller
{
    public class TopPanelController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;
        private readonly AudioManager _audioManager;
        private readonly GameCanvaslView _topPanelView;

        public TopPanelController(GameManager gameManager, UIManager uiManager, AudioManager audioManager)
        {
            _gameManager = gameManager;
            _audioManager = audioManager;

            _gameManager.Score.SubscribeOnChange(OnChangeScore);

            _topPanelView = ResourcesLoader.InstantiateAndGetObject<GameCanvaslView>(uiManager.PathForUIObjects + uiManager.GameCanvasPath);
            AddGameObject(_topPanelView.gameObject);
            _topPanelView.Initialize(PauseGame);

            OnChangeScore();
        }
        protected override void OnDispose()
        {
            _gameManager.Score.UnSubscribeOnChange(OnChangeScore);

            base.OnDispose();
        }
        private void OnChangeScore() => _topPanelView.OnChangeScoreText(_gameManager.Score.Value);
        private void PauseGame()
        {
            _gameManager.GameState.Value = GameState.Pause;
            _audioManager.State.Value = AudioStates.Button;
        }
    }
}