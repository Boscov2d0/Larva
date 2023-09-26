using Larva.Data;
using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Game.UI.Controller;
using UnityEngine;

namespace Larva.Game.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private LarvaProfile _larvaProfile;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private LarvaManager _larvaManager;
        [SerializeField] private UIManager _uiManager;

        private LocalizationController _localizationController;
        private GameController _gameController;
        private HUDController _hUdController;

        private void Start()
        {
            _localizationController = new LocalizationController(_localizationManager, _larvaProfile);

            _gameManager.GameState.Value = GameState.Null;
            _gameController = new GameController(_gameManager, _larvaManager);
            _hUdController = new HUDController(_gameManager, _uiManager);
            _gameManager.GameState.Value = GameState.Game;
        }
        private void Update()
        {
            _gameController?.Execute();
        }
        private void OnDestroy()
        {
            _gameController?.Dispose();
            _hUdController?.Dispose();
            _localizationController?.Dispose();
        }
    }
}