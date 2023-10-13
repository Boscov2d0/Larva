using Larva.Data;
using Larva.House.Data;
using Larva.House.Tools;
using Larva.House.UI.Controller;
using Larva.Tools;

namespace Larva.House.Core
{
    public class HouseRoomsUpgradesController : ObjectsDisposer
    {
        private readonly SaveLoadManager _saveLoadManager;
        private readonly LocalizationManager _localizationManager;
        private readonly HouseManager _houseManager;
        private readonly UIManager _uiManager;
        private readonly AudioManager _audioManager;

        private AddRoomUIController _addRoomUIController;

        public HouseRoomsUpgradesController(SaveLoadManager saveLoadManager, LocalizationManager localizationManager, 
                                            HouseManager houseManager, UIManager uiManager, 
                                            AudioManager audioManager)
        {
            _saveLoadManager = saveLoadManager;
            _localizationManager = localizationManager;
            _houseManager = houseManager;
            _uiManager = uiManager;
            _audioManager = audioManager;

            _houseManager.HouseState.SubscribeOnChange(OnStateChange);
        }
        protected override void OnDispose()
        {
            _houseManager.HouseState.UnSubscribeOnChange(OnStateChange);

            base.OnDispose();
        }
        private void OnStateChange()
        {
            _addRoomUIController?.Dispose();

            switch (_houseManager.HouseState.Value)
            {
                case HouseState.Bedroom:
                    GetUpgrate(_houseManager.Bedroom);
                    break;
                case HouseState.ChildrenRoom:
                    GetUpgrate(_houseManager.ChildrenRoom);
                    break;
                case HouseState.Kitchen:
                    GetUpgrate(_houseManager.Kitchen);
                    break;
            }
        }
        private void GetUpgrate(RoomManager room)
        {
            if (room.IsActive == false)
                _addRoomUIController = new AddRoomUIController(_saveLoadManager, _localizationManager, 
                                                               _houseManager, _uiManager, 
                                                               _audioManager, room);
        }
    }
}