using Larva.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using static Larva.Tools.LocalizationTextKeys.LocalizationMenuTextKeys;

namespace Larva.Menu.UI.View
{
    public class MenuCanvasView : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _houseButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitGameButton;


        [SerializeField] private Text _startGameText;
        [SerializeField] private Text _houseText;
        [SerializeField] private Text _settingsText;
        [SerializeField] private Text _exitGameText;

        private UnityAction _startGame;
        private UnityAction _house;
        private UnityAction _openSettings;
        private UnityAction _exitGame;

        public void Initialize(UnityAction startGame, UnityAction house, UnityAction openSettings, UnityAction exitGame)
        {
            _startGame = startGame;
            _house = house;
            _openSettings = openSettings;
            _exitGame = exitGame;

            _startGameButton.onClick.AddListener(_startGame);
            _houseButton.onClick.AddListener(_house);
            _settingsButton.onClick.AddListener(_openSettings);
            _exitGameButton.onClick.AddListener(_exitGame);

            _localizationManager.MenuTable.SubscribeOnChange(TranslateText);
            TranslateText();
        }
        private void OnDestroy()
        {
            _localizationManager.MenuTable.UnSubscribeOnChange(TranslateText);

            _startGameButton.onClick.RemoveListener(_startGame);
            _houseButton.onClick.RemoveListener(_house);
            _settingsButton.onClick.RemoveListener(_openSettings);
            _exitGameButton.onClick.RemoveListener(_exitGame);
        }
        private void TranslateText()
        {
            _startGameText.text = _localizationManager.MenuTable.Value.GetEntry(StartGame.ToString())?.GetLocalizedString();
            _houseText.text = _localizationManager.MenuTable.Value.GetEntry(LarvaHouse.ToString())?.GetLocalizedString();
            _settingsText.text = _localizationManager.MenuTable.Value.GetEntry(Settings.ToString())?.GetLocalizedString();
            _exitGameText.text = _localizationManager.MenuTable.Value.GetEntry(Exit.ToString())?.GetLocalizedString();
        }
    }
}