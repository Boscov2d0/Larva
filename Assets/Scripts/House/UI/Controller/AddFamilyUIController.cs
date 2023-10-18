using Larva.Tools;
using Larva.Data;
using Larva.House.Data;
using Larva.House.UI.View;

using static Larva.House.Tools.HouseState;
using static Larva.Tools.LocalizationTextKeys.LocalizationHouseTextKeys;
using static Larva.Tools.AudioKeys;
using static Larva.Tools.Keys;

namespace Larva.House.UI.Controller
{
    public class AddFamilyUIController : ObjectsDisposer
    {
        private readonly LocalizationManager _localizationManager;
        private readonly HouseManager _houseManager;
        private readonly AudioManager _audioManager;
        private readonly AddFamilyCanvasView _addFamilyCanvasView;

        public AddFamilyUIController(LocalizationManager localizationManager, HouseManager houseManager, UIManager uiManager, AudioManager audioManager)
        {
            _localizationManager = localizationManager;
            _houseManager = houseManager;
            _audioManager = audioManager;

            _addFamilyCanvasView = ResourcesLoader.InstantiateAndGetObject<AddFamilyCanvasView>(uiManager.PathForUIObjects + uiManager.AddFamilyCanvasPath);
            if (_houseManager.RoomState.Value == RoomState.ChildrenRoom)
                _addFamilyCanvasView.Initialize(AddChildren, Cancel, 
                                                _localizationManager.HouseTable.Value.GetEntry(Child.ToString())?.GetLocalizedString(), 
                                                _houseManager.ChildrensProfile[_houseManager.ChildrenID].Cost);
            if (_houseManager.RoomState.Value == RoomState.Bedroom)
                _addFamilyCanvasView.Initialize(AddPartner, Cancel, 
                                                _localizationManager.HouseTable.Value.GetEntry(Partner.ToString())?.GetLocalizedString(),
                                                _houseManager.PartnerProfile.Cost);

            AddGameObject(_addFamilyCanvasView.gameObject);
        }
        private void AddPartner()
        {
            if (_houseManager.CountOfFood.Value < _houseManager.PartnerProfile.Cost)
            {
                _addFamilyCanvasView.ErrorMessage(_localizationManager.HouseTable.Value.GetEntry(NotEnought.ToString())?.GetLocalizedString());
                _audioManager.State.Value = AudioStates.ButtonCancel;
                return;
            }
            if (!_houseManager.Kitchen.IsActive)
            {
                _addFamilyCanvasView.ErrorMessage(_localizationManager.HouseTable.Value.GetEntry(HavetKitchen.ToString())?.GetLocalizedString());
                _audioManager.State.Value = AudioStates.ButtonCancel;
                return;
            }

            _houseManager.CountOfFood.Value -= _houseManager.PartnerProfile.Cost;
            _houseManager.HavePartner = true;
            _houseManager.AddPartner = false;
            _houseManager.RoomState.Value = RoomState.Kitchen;

            _audioManager.State.Value = AudioStates.ButtonApply;

            _houseManager.SaveLoadState.Value = SaveState.SaveHouseData;
        }
        private void AddChildren()
        {
            if (_houseManager.CountOfFood.Value < _houseManager.ChildrensProfile[_houseManager.ChildrenID].Cost)
            {
                _addFamilyCanvasView.ErrorMessage(_localizationManager.HouseTable.Value.GetEntry(NotEnought.ToString())?.GetLocalizedString());
                _audioManager.State.Value = AudioStates.ButtonCancel;
                return;
            }
            if (!_houseManager.HavePartner)
            {
                _addFamilyCanvasView.ErrorMessage(_localizationManager.HouseTable.Value.GetEntry(HavetPartner.ToString())?.GetLocalizedString());
                _audioManager.State.Value = AudioStates.ButtonCancel;
                return;
            }

            _houseManager.CountOfFood.Value -= _houseManager.ChildrensProfile[_houseManager.ChildrenID].Cost;
            _houseManager.CountOfChildren++;
            _houseManager.HaveChild = true;
            _houseManager.AddChild = false;
            _houseManager.ActionState.Value = ActionState.InstantiateChild;
            _houseManager.RoomState.Value = _houseManager.RoomState.Value;

            _audioManager.State.Value = AudioStates.ButtonApply;

            _houseManager.SaveLoadState.Value = SaveState.SaveHouseData;

            if (_houseManager.CountOfChildren == 4)
            {
                Dispose();
            }
        }
        private void Cancel()
        {
            _houseManager.AddPartner = false;
            _houseManager.AddChild = false;
            _houseManager.RoomState.Value = _houseManager.RoomState.Value;

            _audioManager.State.Value = AudioStates.ButtonCancel;
        }
    }
}