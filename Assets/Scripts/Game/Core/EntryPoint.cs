using Larva.Game.Data;
using Larva.Game.Tools;
using UnityEngine;

namespace Larva.Game.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private LarvaManager _playerManager;

        private GameController _gameController;
        private MoveController _moveController;

        private void Start()
        {
            _gameManager.GameState.Value = GameState.Null;
            _gameController = new GameController(_gameManager);
            
            #if UNITY_ANDROID && !UNITY_EDITOR
            _moveController = new InputTouchScreenController()
            #else
            _moveController = new InputKeyBoardController(_gameManager, _playerManager);
            #endif
        }
        private void Update()
        {
            _gameController?.Dispose();
            _moveController?.Execute();
        }
        private void OnDestroy()
        {
            _moveController?.Dispose();
        }
    }
}