using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Larva.Menu.UI.View
{
    public class MenuCanvasView : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitGameButton;


        [SerializeField] private Text _startGameText;
        [SerializeField] private Text _settingsText;
        [SerializeField] private Text _exitGameText;

        private UnityAction _startGame;
        private UnityAction _openSettings;
        private UnityAction _exitGame;

        public void Init(UnityAction startGame, UnityAction openSettings, UnityAction exitGame)
        {
            _startGame = startGame;
            _openSettings = openSettings;
            _exitGame = exitGame;

            _startGameButton.onClick.AddListener(_startGame);
            _settingsButton.onClick.AddListener(_openSettings);
            _exitGameButton.onClick.AddListener(_exitGame);
            
            TranslateText();
        }
        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(_startGame);
            _settingsButton.onClick.RemoveListener(_openSettings);
            _exitGameButton.onClick.RemoveListener(_exitGame);
        }
        private void TranslateText()
        {
            _startGameText.text = Localizator.GetLocalizedValue(_localizationManager.LocalizedMenuText, LocalizationTextKeys.LocalizationMainMenuTextKeys.StartGame);
            _settingsText.text = Localizator.GetLocalizedValue(_localizationManager.LocalizedMenuText, LocalizationTextKeys.LocalizationMainMenuTextKeys.Settings);
            _exitGameText.text = Localizator.GetLocalizedValue(_localizationManager.LocalizedMenuText, LocalizationTextKeys.LocalizationMainMenuTextKeys.Exit);
        }
    }
}