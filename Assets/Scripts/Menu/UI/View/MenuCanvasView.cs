using Larva.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using LMTK = Larva.Tools.LocalizationTextKeys.LocalizationMenuTextKeys;

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
            _startGameText.text = _localizationManager.MenuTable.Value.GetEntry(LMTK.StartGame.ToString())?.GetLocalizedString();
            _settingsText.text = _localizationManager.MenuTable.Value.GetEntry(LMTK.Settings.ToString())?.GetLocalizedString();
            _exitGameText.text = _localizationManager.MenuTable.Value.GetEntry(LMTK.Exit.ToString())?.GetLocalizedString();
        }
    }
}