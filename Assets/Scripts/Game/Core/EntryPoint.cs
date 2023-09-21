using Larva.Game.Data;
using Larva.Game.Tools;
using UnityEngine;

namespace Larva.Game.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        private GameController _gameController;

        private void Start()
        {
            _gameManager.GameState.Value = GameState.Null;
            _gameController = new GameController(_gameManager);
        }

        private void OnDestroy()
        {
            _gameController?.Dispose();
        }
    }
}