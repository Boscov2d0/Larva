using Larva.Tools;
using Larva.Data;
using Larva.House.Tools;
using Larva.House.Data;

namespace Larva.House.UI.Controller
{
    public class HUDController : ObjectsDisposer
    {
        private readonly HouseManager _houseManager;
        private readonly UIManager _uiManager;
        private readonly AudioManager _audioManager;

        private MainHallUIController _mainHallUIController;

        public HUDController(HouseManager houseManager, UIManager uiManager, AudioManager audioManager)
        {
            _houseManager = houseManager;
            _uiManager = uiManager;
            _audioManager = audioManager;

            _houseManager.HouseState.SubscribeOnChange(OnChangeState);
        }
        protected override void OnDispose()
        {
            _houseManager.HouseState.UnSubscribeOnChange(OnChangeState);

            DisposeControllers();

            base.OnDispose();
        }
        private void OnChangeState()
        {
            DisposeControllers();

            switch (_houseManager.HouseState.Value)
            {
                case HouseState.MainHall:
                    _mainHallUIController = new MainHallUIController(_houseManager, _uiManager, _audioManager);
                    AddController(_mainHallUIController);
                    break;
            }
        }
        private void DisposeControllers()
        {
            _mainHallUIController?.Dispose();
        }
    }
}