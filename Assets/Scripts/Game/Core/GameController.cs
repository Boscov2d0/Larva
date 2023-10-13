using Larva.Data;
using Larva.Game.Core.Player;
using Larva.Game.Core.SpawnObjects;
using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Larva.Game.Core
{
    public class GameController : ObjectsDisposer
    {
        private readonly LarvaProfile _larvaProfile;
        private readonly GameManager _gameManager;
        private readonly LarvaManager _larvaManager;

        private LarvaView _larva;
        private Camera _camera;

        private PreStartController _preStartController;
        private MoveController _moveController;
        private SpawnObjectsController _spawnObjectsController;

        public GameController(LarvaProfile larvaProfile, GameManager gameManager, LarvaManager larvaManager, PreStartManager preStartManager)
        {
            _larvaProfile = larvaProfile;
            _gameManager = gameManager;
            _larvaManager = larvaManager;

            _gameManager.GameState.Value = GameState.Null;

            CreateLarva();

            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.DirectionalLightPath);
            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.GameAreaPath);
            ResourcesLoader.InstantiateObject<GameObject>(_gameManager.PathForObjects + _gameManager.AudioControllerPath);

            _gameManager.GameState.SubscribeOnChange(OnGameStateChange);

            if (_gameManager.PlayHellowAnimation)
                _preStartController = new PreStartController(_gameManager, preStartManager, _larva);
            else
                _gameManager.GameState.Value = GameState.PreGame;
        }
        private void CreateLarva()
        {
            _larva = ResourcesLoader.InstantiateAndGetObject<LarvaView>(_larvaManager.ObjectsPath + _larvaManager.LarvaPath);
            _larva.gameObject.transform.position = _gameManager.StartPosition;
            _larvaManager.State.SubscribeOnChange(OnLarvaStateChange);

#if UNITY_ANDROID && !UNITY_EDITOR
            _moveController = new InputTouchScreenController(_larvaManager);
#else
            _moveController = new InputKeyBoardController(_gameManager, _larvaManager);
#endif
            AddController(_moveController);
        }
        private void Initialize()
        {
            _preStartController?.Dispose();

            _camera = ResourcesLoader.InstantiateAndGetObject<Camera>(_gameManager.PathForObjects + _gameManager.CameraPath);

            _gameManager.GameState.Value = GameState.Game;
            _gameManager.CurrentCountOfGoodFood.Value = _gameManager.CountOfGoodFood;
            _gameManager.Score.Value = 0;

            _spawnObjectsController = new SpawnObjectsController(_gameManager);
            AddController(_spawnObjectsController);

            _larva.Animator.enabled = false;
            _larvaManager.State.Value = LarvaState.Play;
        }

        protected override void OnDispose()
        {
            _larvaManager.State.UnSubscribeOnChange(OnLarvaStateChange);
            _gameManager.GameState.UnSubscribeOnChange(OnGameStateChange);

            _moveController?.Dispose();
            _spawnObjectsController?.Dispose();

            base.OnDispose();
        }
        public void Execute()
        {
            _moveController?.Execute();
        }
        public void FixedExecute()
        {
            _preStartController?.FixedExecute();
        }
        private void OnGameStateChange()
        {
            switch (_gameManager.GameState.Value)
            {
                case GameState.PreGame:
                    Initialize();
                    break;
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
            switch (_larvaManager.State.Value)
            {
                case LarvaState.EatGoodFood:
                    _gameManager.CurrentCountOfGoodFood.Value--;
                    _gameManager.Score.Value += _gameManager.IncreasePointsValue;
                    break;
                case LarvaState.EatBadFood:
                    _gameManager.Score.Value -= _gameManager.IncreasePointsValue;
                    break;
                case LarvaState.Death:
                    _gameManager.GameState.Value = GameState.Lose;
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
            _camera.GetComponentInChildren<Animator>().enabled = true;
            RecalculateFood();
        }
        private void RecalculateFood() => _larvaProfile.Food.Value = _gameManager.Score.Value / 10;

        private void Restart() => SceneManager.LoadScene(Keys.ScneneNameKeys.Game.ToString());
        private void Exit()
        {
            GamePause(false);
            SceneManager.LoadScene(Keys.ScneneNameKeys.Menu.ToString());
        }
    }
}