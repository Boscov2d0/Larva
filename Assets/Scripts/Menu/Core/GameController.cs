using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Larva.Menu.Core
{
    public class GameController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;

        public GameController(GameManager gameManager)
        {
            _gameManager = gameManager;

            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.ScenePath);
            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.DirectionalLight);
            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.MenuCameraPath);

            ResourcesLoader.InstantiateAndGetObject<GameObject>(_gameManager.PathForObjects + _gameManager.MenuLarvaPath);

            _gameManager.GameState.SubscribeOnChange(OnChangeGameState);
        }
        protected override void OnDispose()
        {
            _gameManager.GameState.UnSubscribeOnChange(OnChangeGameState);
        }
        private void OnChangeGameState()
        {
            switch (_gameManager.GameState.Value)
            {
                case GameState.Play:
                    Play();
                    break;
                case GameState.Exit:
                    Exit();
                    break;
            }
        }
        private void Play() => SceneManager.LoadScene(Keys.SceneNameKeys.Game.ToString());
        private void Exit() => Application.Quit();
    }
}