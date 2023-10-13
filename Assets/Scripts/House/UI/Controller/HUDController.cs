using Larva.Data;
using Larva.House.Data;
using Larva.House.Tools;
using Larva.Tools;
using UnityEngine;

namespace Larva.House.UI.Controller
{
    public class HUDController : ObjectsDisposer
    {
        private readonly SaveLoadManager _saveLoadManager;
        private readonly HouseManager _houseManager;
        private readonly UIManager _uiManager;
        private readonly AudioManager _audioManager;
        private readonly LarvaProfile _larvaProfile;

        private MainHallUIController _mainHallUIController;
        private BedroomUIController _bedroomUIController;
        private ChildrenRoomUIController _childrenRoomUIController;
        private KitchenUIController _kitchenUIController;
        private WardrobeUIController _wardrobeUIController;

        public HUDController(SaveLoadManager saveLoadManager, HouseManager houseManager, UIManager uiManager, AudioManager audioManager, LarvaProfile larvaProfile)
        {
            _saveLoadManager = saveLoadManager;
            _houseManager = houseManager;
            _uiManager = uiManager;
            _audioManager = audioManager;
            _larvaProfile = larvaProfile;

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
                case HouseState.Bedroom:
                    _bedroomUIController = new BedroomUIController(_houseManager, _uiManager, _audioManager);
                    AddController(_bedroomUIController);
                    break;
                case HouseState.ChildrenRoom:
                    _childrenRoomUIController = new ChildrenRoomUIController(_houseManager, _uiManager, _audioManager);
                    AddController(_childrenRoomUIController);
                    break;
                case HouseState.Kitchen:
                    _kitchenUIController = new KitchenUIController(_houseManager, _uiManager, _audioManager);
                    AddController(_kitchenUIController);
                    break;
                case HouseState.Wardrobe:
                    _wardrobeUIController = new WardrobeUIController(_saveLoadManager, _larvaProfile, 
                                                                     _houseManager, _uiManager, _audioManager);
                    AddController(_wardrobeUIController);
                    break;
            }
        }
        private void DisposeControllers()
        {
            _mainHallUIController?.Dispose();
            _bedroomUIController?.Dispose();
            _childrenRoomUIController?.Dispose();
            _kitchenUIController?.Dispose();
        }
    }
}