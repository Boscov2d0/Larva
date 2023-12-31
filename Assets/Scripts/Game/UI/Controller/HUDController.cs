﻿using Larva.Data;
using Larva.Game.Data;
using Larva.Tools;

using static Larva.Game.Tools.States;

namespace Larva.Game.UI.Controller
{
    public class HUDController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;
        private readonly UIManager _uiManager;
        private readonly AudioManager _audioManager;

        private TopPanelController _topPanelController;
        private PausePanelController _pausePanelController;
        private GameOverPanelController _gameOverPanelController;

        public HUDController(GameManager gameManager, UIManager uiManager, AudioManager audioManager)
        {
            _gameManager = gameManager;
            _uiManager = uiManager;
            _audioManager = audioManager;

            _gameManager.GameState.SubscribeOnChange(OnChangeState);
        }
        protected override void OnDispose()
        {
            _gameManager.GameState.UnSubscribeOnChange(OnChangeState);
            _topPanelController?.Dispose();

            base.OnDispose();
        }
        private void OnChangeState()
        {
            DisposeControllers();

            switch (_gameManager.GameState.Value)
            {
                case GameState.Game:
                    _topPanelController = new TopPanelController(_gameManager, _uiManager, _audioManager);
                    AddController(_topPanelController);
                    break;
                case GameState.Pause:
                    _pausePanelController = new PausePanelController(_gameManager, _uiManager, _audioManager);
                    AddController(_pausePanelController);
                    break;
                case GameState.Lose:
                    _gameOverPanelController = new GameOverPanelController(_gameManager, _uiManager, _audioManager);
                    AddController(_gameOverPanelController);
                    break;
            }
        }
        private void DisposeControllers()
        {
            _topPanelController?.Dispose();
            _pausePanelController?.Dispose();
            _gameOverPanelController?.Dispose();
        }
    }
}