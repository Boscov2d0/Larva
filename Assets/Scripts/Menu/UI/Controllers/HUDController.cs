using Larva.Data;
using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Tools;

namespace Larva.Menu.UI.Controller
{
    public class HUDController : ObjectsDisposer
    {
        private readonly LocalizationManager _localizationManager;
        private readonly SaveLoadManager _saveLoadManager;
        private readonly GameManager _gameManager;
        private readonly UIManager _uiManager;
        private readonly AudioManager _audioManager;
        private readonly VideoManager _videoManager;
        private readonly House.Data.HouseManager _houseManager;

        private OutSideUIController _outSideUIController;

        public HUDController(LocalizationManager localizationManager, SaveLoadManager saveLoadManager, 
                             GameManager gameManager, UIManager uiManager, 
                             AudioManager audioManager, VideoManager videoManager,
                             House.Data.HouseManager houseManager)
        {
            _localizationManager = localizationManager;
            _saveLoadManager = saveLoadManager;
            _gameManager = gameManager;
            _uiManager = uiManager;
            _audioManager = audioManager;
            _videoManager = videoManager;
            _houseManager = houseManager;

            _gameManager.GameState.SubscribeOnChange(OnChangeState);
            _gameManager.GameState.Value = GameState.OutSideMenu;
        }
        protected override void OnDispose()
        {
            _gameManager.GameState.UnSubscribeOnChange(OnChangeState);

            base.OnDispose();
        }
        private void OnChangeState()
        {
            switch (_gameManager.GameState.Value)
            {
                case GameState.OutSideMenu:
                    _outSideUIController = new OutSideUIController(_localizationManager, _saveLoadManager,
                                                                   _gameManager, _uiManager, 
                                                                   _audioManager, _videoManager,
                                                                   _houseManager);
                    AddController(_outSideUIController);
                    break;
                case GameState.LarvaHouse:
                    _outSideUIController?.Dispose();
                    break;
            }
        }
    }
}