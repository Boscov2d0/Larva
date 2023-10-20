using Larva.Core;
using Larva.Data;
using Larva.House.Core;
using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Tools;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Larva.Tools.Keys;

namespace Larva.Menu.Core
{
    public class GameController : ObjectsDisposer
    {
        private readonly LarvaProfile _larvaProfile;
        private readonly GameManager _gameManager;
        private readonly VideoManager _videoManager;

        private HouseController _house;
        private Light _directionLight;

        public GameController(LarvaProfile larvaProfile, GameManager gameManager, VideoManager videoManager)
        {
            _larvaProfile = larvaProfile;
            _gameManager = gameManager;
            _videoManager = videoManager;

            _gameManager.DayTime.SubscribeOnChange(SetDirectionLight);

            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.ScenePath);

            _gameManager.MenuLarva = ResourcesLoader.InstantiateAndGetObject<LarvaView>(_gameManager.PathForObjects + _gameManager.MenuLarvaPath);
            AddGameObject(_gameManager.MenuLarva.gameObject);

            ResourcesLoader.InstantiateObject<Camera>(_gameManager.PathForObjects + _gameManager.MenuCameraPath);
            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.AudioControllerPath);

            _house = ResourcesLoader.InstantiateAndGetObject<HouseController>(_gameManager.LarvaHousePath);
            AddGameObject(_house.gameObject);
            SetDirectionLight();
            SetVideoSettings();

            _gameManager.GameState.SubscribeOnChange(OnChangeGameState);
        }
        protected override void OnDispose()
        {
            _gameManager.DayTime.UnSubscribeOnChange(SetDirectionLight);
            _gameManager.GameState.UnSubscribeOnChange(OnChangeGameState);
        }
        private void SetDirectionLight()
        {
            GameObject.Destroy(_directionLight?.gameObject);

            switch (_gameManager.DayTime.Value)
            {
                case DayTime.Auto:
                    DateTime time = DateTime.Now;
                    if ((time.Hour >= 7 && time.Minute >= 0) && (time.Hour <= 16 && time.Minute >= 0))
                        _directionLight = ResourcesLoader.InstantiateAndGetObject<Light>(_gameManager.PathForObjects + _gameManager.DayDirectionalLightPath);
                    else if ((time.Hour >= 17 && time.Minute >= 0) && (time.Hour <= 22 && time.Minute >= 0))
                        _directionLight = ResourcesLoader.InstantiateAndGetObject<Light>(_gameManager.PathForObjects + _gameManager.EveningDirectionalLightPath);
                    else
                        _directionLight = ResourcesLoader.InstantiateAndGetObject<Light>(_gameManager.PathForObjects + _gameManager.NightDirectionalLightPath);
                    break;
                case DayTime.Day:
                    _directionLight = ResourcesLoader.InstantiateAndGetObject<Light>(_gameManager.PathForObjects + _gameManager.DayDirectionalLightPath);
                    break;
                case DayTime.Evening:
                    _directionLight = ResourcesLoader.InstantiateAndGetObject<Light>(_gameManager.PathForObjects + _gameManager.EveningDirectionalLightPath);
                    break;
                case DayTime.Night:
                    _directionLight = ResourcesLoader.InstantiateAndGetObject<Light>(_gameManager.PathForObjects + _gameManager.NightDirectionalLightPath);
                    break;
            }
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
        private void Play()
        {
            SceneManager.LoadScene(Keys.ScneneNameKeys.Game.ToString());
            _larvaProfile.DayTime = _gameManager.DayTime.Value;
        }
        private void Exit() => Application.Quit();
    }
}