using Larva.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using static Larva.House.Tools.HouseState;
using static Larva.Tools.LocalizationTextKeys.LocalizationHouseTextKeys;

namespace Larva.House.UI.View
{
    public class AddRoomCanvasView : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private Text _roomNameText;
        [SerializeField] private Text _buildForText;
        [SerializeField] private Text _notBuildText;
        [SerializeField] private Text _addButtonText;
        [SerializeField] private Button _addButton;
        [SerializeField] private GameObject _errorMassagePanel;
        [SerializeField] private Text _errorMassageText;

        private UnityAction _add;

        public void Initialize(UnityAction add, int foodCost, RoomState room)
        {
            _add = add;

            _addButton.onClick.AddListener(_add);

            TranslateText(foodCost, room);

            _notBuildText.text = _localizationManager.HouseTable.Value.GetEntry(NotBuilt.ToString())?.GetLocalizedString();
        }
        private void OnDestroy()
        {
            _addButton.onClick.RemoveListener(_add);
        }
        private void TranslateText(int foodCost, RoomState room) 
        {
            string roomName = null;

            switch (room)
            {
                case RoomState.Kitchen:
                    roomName = _localizationManager.HouseTable.Value.GetEntry(Kitchen.ToString())?.GetLocalizedString();
                    break;
                case RoomState.Bedroom:
                    roomName = _localizationManager.HouseTable.Value.GetEntry(Bedroom.ToString())?.GetLocalizedString();
                    break;
                case RoomState.ChildrenRoom:
                    roomName = _localizationManager.HouseTable.Value.GetEntry(ChildrenRoom.ToString())?.GetLocalizedString();
                    break;
            }

            _roomNameText.text = roomName;

            _buildForText.text = $"{_localizationManager.HouseTable.Value.GetEntry(BuiltFor.ToString())?.GetLocalizedString()} " +
                                 $"{foodCost} ";

            _addButtonText.text = _localizationManager.HouseTable.Value.GetEntry(Built.ToString())?.GetLocalizedString();
        }
        public void ErrorMessage(string message)
        {
            _errorMassagePanel.SetActive(true);
            _errorMassageText.text = message;
        }
    }
}