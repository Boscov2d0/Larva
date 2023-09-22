using Larva.Game.Core.Player;
using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Larva.Game.Core
{
    public class GameController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;
        private readonly LarvaManager _larvaManager;

        private Camera _camera;
        private MoveController _moveController;

        public GameController(GameManager gameManager, LarvaManager larvaManager)
        {
            _gameManager = gameManager;
            _larvaManager = larvaManager;

            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.GameAreaPath);
            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.DirectionalLight);
            _camera = ResourcesLoader.InstantiateAndGetObject<Camera>(_gameManager.PathForObjects + _gameManager.CameraPath);
            ResourcesLoader.InstantiateObject<LarvaView>(_larvaManager.ObjectsPath + _larvaManager.LarvaPath);

            _larvaManager.State.SubscribeOnChange(OnLarvaStateChange);

#if UNITY_ANDROID && !UNITY_EDITOR
            _moveController = new InputTouchScreenController()
#else
            _moveController = new InputKeyBoardController(_gameManager, larvaManager);
#endif

            _gameManager.GameState.SubscribeOnChange(OnGameStateChange);
            _gameManager.GameState.Value = GameState.Game;
            _gameManager.Score.Value = 0;
        }

        protected override void OnDispose()
        {
            _larvaManager.State.UnSubscribeOnChange(OnLarvaStateChange);
            _gameManager.GameState.UnSubscribeOnChange(OnGameStateChange);

            _moveController?.Dispose();

            base.OnDispose();
        }
        public void Execute()
        {
            _moveController?.Execute();
        }
        private void OnGameStateChange()
        {
            switch (_gameManager.GameState.Value)
            {
                case GameState.Game:
                    GamePause(false);
                    break;
                case GameState.Pause:
                    GamePause(true);
                    break;
                case GameState.Lose:
                    GameOver();
                    break;
                case GameState.Restart:
                    Restart();
                    break;
                case GameState.Exit:
                    Exit();
                    break;
            }
        }
        private void OnLarvaStateChange() 
        {
            if (_larvaManager.State.Value == PlayerState.Death)
                _gameManager.GameState.Value = GameState.Lose;
        }
        private void GamePause(bool isPause)
        {
            if (isPause)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
        private void GameOver()
        {
            RecalculateFood();
            _camera.GetComponentInChildren<Animator>().enabled = true;
        }
        private void RecalculateFood() { }
        private void Restart() => SceneManager.LoadScene(0);
        private void Exit() => SceneManager.LoadScene(0);
    }
}