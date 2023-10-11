using Larva.Data;
using Larva.Game.Data;
using Larva.Game.UI.Controller;
using UnityEngine;

namespace Larva.Game.Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private LarvaManager _larvaManager;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private PreStartManager _preStartManager;

        private LocalizationController _localizationController;
        private GameController _gameController;
        private HUDController _hUdController;

        private void Start()
        {
            _localizationController = new LocalizationController(_localizationManager);
            _gameController = new GameController(_gameManager, _larvaManager, _preStartManager);
            _hUdController = new HUDController(_gameManager, _uiManager, _audioManager);
        }
        private void Update()
        {
            _gameController?.Execute();
        }
        private void FixedUpdate()
        {
            _gameController?.FixedExecute();
        }
        private void OnDestroy()
        {
            _localizationController?.Dispose();
            _gameController?.Dispose();
            _hUdController?.Dispose();
        }
    }
}