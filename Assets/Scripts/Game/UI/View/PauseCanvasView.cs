using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Larva.Game.UI.View
{
    public class PauseCanvasView : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private Text _continueText;
        [SerializeField] private Text _exitText;

        private UnityAction _continueVoid;
        private UnityAction _exitVoid;

        public void Initialized(UnityAction continueVoid, UnityAction exit)
        {
            _continueVoid = continueVoid;
            _exitVoid = exit;

            _continueButton.onClick.AddListener(_continueVoid);
            _exitButton.onClick.AddListener(_exitVoid);

            TranslateText();
        }
        private void OnDestroy()
        {
            _continueButton.onClick.RemoveListener(_continueVoid);
            _exitButton.onClick.RemoveListener(_exitVoid);
        }
        private void TranslateText()
        {
            _continueText.text = Localizator.GetLocalizedValue(_localizationManager.LocalizedGameText, LocalizationGameTextKeys.Continue);
            _exitText.text = Localizator.GetLocalizedValue(_localizationManager.LocalizedGameText, LocalizationGameTextKeys.MainMenu);
        }
    }
}