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

        private Camera _camera;

        public GameController(GameManager gameManager)
        {
            _gameManager = gameManager;

            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.GameAreaPath);
            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.DirectionalLight);
            ResourcesLoader.InstantiateObject<Camera>(_gameManager.PathForObjects + _gameManager.CameraPath);

            _gameManager.GameState.SubscribeOnChange(OnChangeGameState);
            _gameManager.GameState.Value = GameState.Game;
            _gameManager.Score.Value = 0;
        }

        protected override void OnDispose()
        {
            _gameManager.GameState.UnSubscribeOnChange(OnChangeGameState);

            base.OnDispose();
        }
        private void OnChangeGameState()
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
        }
        private void RecalculateFood() { }
        private void Restart() => SceneManager.LoadScene(1);
        private void Exit() => SceneManager.LoadScene(0);
    }
}