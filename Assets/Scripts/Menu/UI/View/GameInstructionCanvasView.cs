using Larva.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using static Larva.Tools.LocalizationTextKeys.LocalizationInstructionsTextKeys;

namespace Larva.Menu.UI.View
{
    public class GameInstructionCanvasView : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private Button _backButton;
        [SerializeField] private Text _ruleText;
        [SerializeField] private Text _goodLeafsText;
        [SerializeField] private Text _badLeafsText;
        [SerializeField] private Text _obstaclesText;
        [SerializeField] private Text _backButtonText;

        private UnityAction _back;

        public void Initialize(UnityAction back)
        {
            _back = back;

            _backButton.onClick.AddListener(_back);

            _localizationManager.MenuTable.SubscribeOnChange(TranslateText);
            TranslateText();
        }
        private void OnDestroy()
        {
            _backButton.onClick.RemoveListener(_back);
            _localizationManager.MenuTable.UnSubscribeOnChange(TranslateText);
        }
        private void TranslateText() 
        {
            _ruleText.text = _localizationManager.MenuTable.Value.GetEntry(Instructions.ToString())?.GetLocalizedString();
            _goodLeafsText.text = _localizationManager.MenuTable.Value.GetEntry(GoodLeafs.ToString())?.GetLocalizedString();
            _badLeafsText.text = _localizationManager.MenuTable.Value.GetEntry(BadLeafs.ToString())?.GetLocalizedString();
            _obstaclesText.text = _localizationManager.MenuTable.Value.GetEntry(Obstacles.ToString())?.GetLocalizedString();
            _backButtonText.text = _localizationManager.MenuTable.Value.GetEntry(Back.ToString())?.GetLocalizedString();
        }
    }
}