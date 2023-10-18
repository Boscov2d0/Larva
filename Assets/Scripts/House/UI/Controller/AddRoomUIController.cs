using Larva.Data;
using Larva.House.Data;
using Larva.House.Tools;
using Larva.House.UI.View;
using Larva.Tools;

using static Larva.Tools.AudioKeys;
using static Larva.Tools.Keys;
using static Larva.Tools.LocalizationTextKeys.LocalizationHouseTextKeys;

namespace Larva.House.UI.Controller
{
    public class AddRoomUIController : ObjectsDisposer
    {
        private readonly SaveLoadManager _saveLoadManager;
        private readonly LocalizationManager _localizationManager;
        private readonly HouseManager _houseManager;
        private readonly AudioManager _audioManager;
        private readonly AddRoomCanvasView _addRoomCanvasView;
        private RoomManager _room;

        public AddRoomUIController(SaveLoadManager saveLoadManager, LocalizationManager localizationManager, HouseManager houseManager, UIManager uiManager, AudioManager audioManager, RoomManager room)
        {
            _saveLoadManager = saveLoadManager;
            _localizationManager = localizationManager;
            _houseManager = houseManager;
            _audioManager = audioManager;
            _room = room;

            _addRoomCanvasView = ResourcesLoader.InstantiateAndGetObject<AddRoomCanvasView>(uiManager.PathForUIObjects + uiManager.AddRoomCanvasPath);
            AddGameObject(_addRoomCanvasView.gameObject);
            _addRoomCanvasView.Initialize(AddRoom, _room.Cost, _room.RoomName);
        }
        private void AddRoom()
        {
            if (_room.RoomName == HouseState.RoomState.ChildrenRoom && !_houseManager.Bedroom.IsActive)
            {
                _addRoomCanvasView.ErrorMessage(_localizationManager.HouseTable.Value.GetEntry(HavetBedroom.ToString())?.GetLocalizedString());
                _audioManager.State.Value = AudioStates.ButtonCancel;
                return;
            }

            if (_houseManager.CountOfFood.Value < _room.Cost)
            {
                _addRoomCanvasView.ErrorMessage(_localizationManager.HouseTable.Value.GetEntry(NotEnought.ToString())?.GetLocalizedString());
                _audioManager.State.Value = AudioStates.ButtonCancel;
                return;
            }

            _houseManager.CountOfFood.Value -= _room.Cost;
            _room.IsActive = true;
            _houseManager.RoomState.Value = _room.RoomName;

            _audioManager.State.Value = AudioStates.ButtonApply;

            _houseManager.SaveLoadState.Value = SaveState.SaveHouseData;
        }
    }
}