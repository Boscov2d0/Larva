using Larva.Data;
using Larva.House.Data;
using UnityEngine;
using UnityEngine.UI;

using LHTK = Larva.Tools.LocalizationTextKeys.LocalizationHouseTextKeys;

namespace Larva.House.UI.View
{
    public class HouseUIView : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private HouseManager _houseManager;
        [SerializeField] private Text _outside;

        private void Start()
        {
            SetRoomsNameText();
        }
        private void SetRoomsNameText()
        {
            _outside.text = _localizationManager.MenuTable.Value.GetEntry(LHTK.Outside.ToString())?.GetLocalizedString();
        }
    }
}