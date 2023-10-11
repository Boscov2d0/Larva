using Larva.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using LGTK = Larva.Tools.LocalizationTextKeys.LocalizationGameTextKeys;

namespace Larva.Game.UI.View
{
    public class PauseCanvasView : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _menuButton;

        [SerializeField] private Text _continueText;
        [SerializeField] private Text _menuText;

        private UnityAction _continueVoid;
        private UnityAction _exitVoid;

        public void Initialize(UnityAction continueVoid, UnityAction exit)
        {
            _continueVoid = continueVoid;
            _exitVoid = exit;

            _continueButton.onClick.AddListener(_continueVoid);
            _menuButton.onClick.AddListener(_exitVoid);

            TranslateText();
        }
        private void OnDestroy()
        {
            _continueButton.onClick.RemoveListener(_continueVoid);
            _menuButton.onClick.RemoveListener(_exitVoid);
        }
        private void TranslateText()
        {
            _continueText.text = _localizationManager.GameTable.Value.GetEntry(LGTK.Continue.ToString())?.GetLocalizedString();
            _menuText.text = _localizationManager.GameTable.Value.GetEntry(LGTK.Menu.ToString())?.GetLocalizedString();
        }
    }
}