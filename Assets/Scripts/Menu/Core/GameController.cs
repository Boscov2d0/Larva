using Larva.House.Core;
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
        private readonly VideoManager _videoManager;

        private HouseController _house;

        public GameController(GameManager gameManager, VideoManager videoManager)
        {
            _gameManager = gameManager;
            _videoManager = videoManager;

            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.ScenePath);
            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.MenuLarvaPath);
            ResourcesLoader.InstantiateObject<Camera>(_gameManager.PathForObjects + _gameManager.MenuCameraPath);
            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.AudioControllerPath);
            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.DirectionalLightPath);

            _house = ResourcesLoader.InstantiateAndGetObject<HouseController>(_gameManager.LarvaHousePath);
            AddGameObject(_house.gameObject);

            SetVideoSettings();

            _gameManager.GameState.SubscribeOnChange(OnChangeGameState);
        }
        protected override void OnDispose()
        {
            _gameManager.GameState.UnSubscribeOnChange(OnChangeGameState);
        }
        private void SetVideoSettings()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
                    return;
#else
            if (_videoManager.ScreenResolution.x == 0)
            {
                _videoManager.ScreenResolution.x = Screen.currentResolution.width;
                _videoManager.ScreenResolution.y = Screen.currentResolution.height;
                _videoManager.Fullscreen = true;
                _videoManager.ScreenResolution.z = Screen.currentResolution.refreshRate;
            }
            Screen.SetResolution((int)_videoManager.ScreenResolution.x, (int)_videoManager.ScreenResolution.y, _videoManager.Fullscreen, (int)_videoManager.ScreenResolution.z);
#endif
        }
        private void OnChangeGameState()
        {
            switch (_gameManager.GameState.Value)
            {
                case GameState.LarvaHouse:
                    _house.EnterToHouse();
                    break;
                case GameState.Play:
                    Play();
                    break;
                case GameState.Exit:
                    Exit();
                    break;
            }
        }
        private void Play() => SceneManager.LoadScene(Keys.ScneneNameKeys.Game.ToString());
        private void Exit() => Application.Quit();
    }
}