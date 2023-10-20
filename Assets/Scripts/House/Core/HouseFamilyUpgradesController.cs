using Larva.Tools;
using Larva.Data;
using Larva.House.Data;
using Larva.House.UI.Controller;

using static Larva.House.Tools.HouseState;

namespace Larva.House.Core
{
    public class HouseFamilyUpgradesController : ObjectsDisposer
    {
        private readonly LocalizationManager _localizationManager;
        private readonly HouseManager _houseManager;
        private readonly UIManager _uiManager;
        private readonly AudioManager _audioManager;

        private PillowController _pillowController;
        private AddFamilyUIController _addFamilyUIController;

        public HouseFamilyUpgradesController(LocalizationManager localizationManager, HouseManager houseManager, UIManager uiManager, AudioManager audioManager)
        {
            _localizationManager = localizationManager;
            _houseManager = houseManager;
            _uiManager = uiManager;
            _audioManager = audioManager;

            _pillowController = new PillowController(_houseManager);

            _houseManager.RoomState.SubscribeOnChange(OnStateChange);
        }
        protected override void OnDispose()
        {
            _pillowController?.Dispose();

            _houseManager.RoomState.UnSubscribeOnChange(OnStateChange);

            base.OnDispose();
        }
        private void OnStateChange()
        {
            _addFamilyUIController?.Dispose();

            if (_houseManager.RoomState.Value != RoomState.Bedroom)
                _houseManager.AddPartner = false;

            if (_houseManager.RoomState.Value != RoomState.ChildrenRoom)
                _houseManager.AddChild = false;

            switch (_houseManager.RoomState.Value)
            {
                case RoomState.ChildrenRoom:
                    if (_houseManager.AddChild)
                        _addFamilyUIController = new AddFamilyUIController(_localizationManager, _houseManager, _uiManager, _audioManager);
                    break;
                case RoomState.Bedroom:
                    if (_houseManager.Bedroom.IsActive && _houseManager.AddPartner)
                        _addFamilyUIController = new AddFamilyUIController(_localizationManager, _houseManager, _uiManager, _audioManager);
                    break;
            }
        }
        public void Execute() 
        {
            _pillowController?.Execute();
        }
    }
}