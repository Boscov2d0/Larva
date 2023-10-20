using Larva.Data;
using Larva.House.Data;
using Larva.Tools;

using static Larva.House.Tools.HouseState;

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

            _houseManager.RoomState.SubscribeOnChange(OnChangeRoomState);
            _houseManager.ActionState.SubscribeOnChange(OnChangeActionState);
        }
        protected override void OnDispose()
        {
            _houseManager.RoomState.UnSubscribeOnChange(OnChangeRoomState);
            _houseManager.ActionState.UnSubscribeOnChange(OnChangeActionState);

            DisposeControllers();

            base.OnDispose();
        }
        private void OnChangeRoomState()
        {
            DisposeControllers();

            switch (_houseManager.RoomState.Value)
            {
                case RoomState.MainHall:
                    _mainHallUIController = new MainHallUIController(_houseManager, _uiManager, _audioManager);
                    AddController(_mainHallUIController);
                    break;
                case RoomState.Bedroom:
                    _bedroomUIController = new BedroomUIController(_houseManager, _uiManager, _audioManager);
                    AddController(_bedroomUIController);
                    break;
                case RoomState.ChildrenRoom:
                    _childrenRoomUIController = new ChildrenRoomUIController(_houseManager, _uiManager, _audioManager);
                    AddController(_childrenRoomUIController);
                    break;
                case RoomState.Kitchen:
                    _kitchenUIController = new KitchenUIController(_houseManager, _uiManager, _audioManager);
                    AddController(_kitchenUIController);
                    break;
            }
        }
        private void OnChangeActionState()
        {
            switch (_houseManager.ActionState.Value)
            {
                case ActionState.OpenWardrobe:
                    _bedroomUIController?.Dispose();
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