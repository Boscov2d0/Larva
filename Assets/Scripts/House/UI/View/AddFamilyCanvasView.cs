using Larva.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using static Larva.Tools.LocalizationTextKeys.LocalizationHouseTextKeys;

namespace Larva.House.UI.View
{
    public class AddFamilyCanvasView : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager; 
        [SerializeField] private Text _addText;
        [SerializeField] private Text _addButtonText;
        [SerializeField] private Text _cancelButtonText;
        [SerializeField] private Text _errorMassageText;
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private GameObject _errorMassagePanel;

        private UnityAction _add;
        private UnityAction _cancel;

        private string _objectName;
        private int _foodCount;

        public void Initialize(UnityAction add, UnityAction cancel, string objectName, int foodCount)
        {
            _add = add;
            _cancel = cancel;
            _objectName = objectName;
            _foodCount = foodCount;

            _addButton.onClick.AddListener(_add);
            _cancelButton.onClick.AddListener(_cancel);

            _localizationManager.HouseTable.SubscribeOnChange(UHaveTextChange);
            UHaveTextChange();
        }
        private void OnDestroy()
        {
            _localizationManager.MenuTable.UnSubscribeOnChange(UHaveTextChange);

            _addButton.onClick.RemoveListener(_add);
            _cancelButton.onClick.RemoveListener(_cancel);
        }
        private void UHaveTextChange()
        {
            _addText.text = $"{_localizationManager.HouseTable.Value.GetEntry(Add.ToString())?.GetLocalizedString()} " +
                              $"{_objectName} {_localizationManager.HouseTable.Value.GetEntry(For.ToString())?.GetLocalizedString()} " +
                              $"{_foodCount}";

            _addButtonText.text = _localizationManager.HouseTable.Value.GetEntry(Add.ToString())?.GetLocalizedString();
            _cancelButtonText.text = _localizationManager.HouseTable.Value.GetEntry(Cancel.ToString())?.GetLocalizedString();
        }
        public void ErrorMessage(string message)
        {
            _errorMassagePanel.SetActive(true);
            _errorMassageText.text = message;
        }
    }
}