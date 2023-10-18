using Larva.Data;
using Larva.Menu.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using static Larva.Tools.LocalizationTextKeys.LocalizationSettingsTextKeys;

namespace Larva.Menu.UI.View
{
    public class PCSettingsCanvasView : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private VideoManager _videoManager;
        [SerializeField] private House.Data.HouseManager _houseManager;

        [SerializeField] private Button _gamePanelButton;
        [SerializeField] private Button _audioPanelButton;
        [SerializeField] private Button _videoPanelButton;

        [SerializeField] private Button _ruLanguageButton;
        [SerializeField] private Button _enLanguageButton;
        [SerializeField] private Button _zhLanguageButton;
        [SerializeField] private Button _simpleModeButton;
        [SerializeField] private Button _realModeButton;
        [SerializeField] private Dropdown _dayOfFeedingDropdown;

        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;

        [SerializeField] private Dropdown _screenResolutionDropdown;
        [SerializeField] private Toggle _fulscreenToggle;
        [SerializeField] private Button _backButton;

        [SerializeField] private List<GameObject> _panels;

        [SerializeField] private Text _gamePanelText;
        [SerializeField] private Text _audioPanelText;
        [SerializeField] private Text _videoPanelText;

        [SerializeField] private Text _languageText;
        [SerializeField] private Text _gameModeText;
        [SerializeField] private Text _simpleModeText;
        [SerializeField] private Text _realModeText;
        [SerializeField] private Text _simpleModeInformationText;
        [SerializeField] private Text _realModeInformationText;
        [SerializeField] private Text _chooseFeedingDayText;

        [SerializeField] private Text _soundText;
        [SerializeField] private Text _musicText;
        [SerializeField] private Text _screenResolutionText;
        [SerializeField] private Text _fullscreenText;
        [SerializeField] private Text _backText;

        private UnityAction _openGamePanel;
        private UnityAction _openAudioPanel;
        private UnityAction _openVideoPanel;

        private UnityAction _setRuLanguage;
        private UnityAction _setEnLanguage;
        private UnityAction _setZhLanguage;
        private UnityAction _simpleMode;
        private UnityAction _realMode;
        private UnityAction<int> _setDayOfFeed;
        private UnityAction<float> _setSoundVolume;
        private UnityAction<float> _setMusicVolume;
        private UnityAction<int> _setScreenResolution;
        private UnityAction<bool> _setFullscreen;
        private UnityAction _back;

        public Dropdown ScreenResolutionDropdown { get { return _screenResolutionDropdown; } private set { } }
        public List<GameObject> Panels { get => _panels; }

        public void Initialize(UnityAction openGamePanel, UnityAction openAudioPanel, UnityAction openVideoPanel,
                               UnityAction setRuLanguage, UnityAction setEnLanguage, UnityAction setZhLanguage,
                               UnityAction simpleMode, UnityAction realMode, UnityAction<int> setDayOfFeed,
                               UnityAction<float> setSoundVolume, UnityAction<float> setMusicVolume,
                               UnityAction<int> setScreenResolution, UnityAction<bool> setFullscreen,
                               UnityAction back)
        {
            _openGamePanel = openGamePanel;
            _openAudioPanel = openAudioPanel;
            _openVideoPanel = openVideoPanel;
            _setRuLanguage = setRuLanguage;
            _setEnLanguage = setEnLanguage;
            _setZhLanguage = setZhLanguage;
            _simpleMode = simpleMode;
            _realMode = realMode;
            _setDayOfFeed = setDayOfFeed;
            _setSoundVolume = setSoundVolume;
            _setMusicVolume = setMusicVolume;
            _setScreenResolution = setScreenResolution;
            _setFullscreen = setFullscreen;
            _back = back;

            _gamePanelButton.onClick.AddListener(_openGamePanel);
            _audioPanelButton.onClick.AddListener(_openAudioPanel);
            _videoPanelButton.onClick.AddListener(_openVideoPanel);
            _ruLanguageButton.onClick.AddListener(_setRuLanguage);
            _enLanguageButton.onClick.AddListener(_setEnLanguage);
            _zhLanguageButton.onClick.AddListener(_setZhLanguage);
            _simpleModeButton.onClick.AddListener(_simpleMode);
            _realModeButton.onClick.AddListener(_realMode);
            _dayOfFeedingDropdown.onValueChanged.AddListener(_setDayOfFeed);
            _soundSlider.onValueChanged.AddListener(_setSoundVolume);
            _musicSlider.onValueChanged.AddListener(_setMusicVolume);
            _screenResolutionDropdown.onValueChanged.AddListener(_setScreenResolution);
            _fulscreenToggle.onValueChanged.AddListener(_setFullscreen);
            _backButton.onClick.AddListener(_back);

            SetUI();
            _localizationManager.SettingsTable.SubscribeOnChange(TranslateText);
            TranslateText();
        }
        private void OnDestroy()
        {
            _localizationManager.SettingsTable.UnSubscribeOnChange(TranslateText);

            _gamePanelButton.onClick.RemoveListener(_openGamePanel);
            _audioPanelButton.onClick.RemoveListener(_openAudioPanel);
            _videoPanelButton.onClick.RemoveListener(_openVideoPanel);
            _ruLanguageButton.onClick.RemoveListener(_setRuLanguage);
            _enLanguageButton.onClick.RemoveListener(_setEnLanguage);
            _zhLanguageButton.onClick.RemoveListener(_setZhLanguage);
            _simpleModeButton.onClick.RemoveListener(_simpleMode);
            _realModeButton.onClick.RemoveListener(_realMode);
            _screenResolutionDropdown.onValueChanged.RemoveListener(_setDayOfFeed);
            _soundSlider.onValueChanged.RemoveListener(_setSoundVolume);
            _musicSlider.onValueChanged.RemoveListener(_setMusicVolume);
            _screenResolutionDropdown.onValueChanged.RemoveListener(_setScreenResolution);
            _fulscreenToggle.onValueChanged.RemoveListener(_setFullscreen);
            _backButton.onClick.RemoveListener(_back);
        }
        private void SetUI()
        {
            _gamePanelButton.Select();
            if (_houseManager.GameMode == Larva.Tools.Keys.GameMode.Simple)
                _simpleModeButton.Select();
            else
                _realModeButton.Select();

            _dayOfFeedingDropdown.value = (int)_houseManager.DayForGiveFood;
            _musicSlider.value = _audioManager.MusicVolume;
            _soundSlider.value = _audioManager.SoundsVolume;

            Dropdown.OptionData data;

            for (int i = 0; i < Screen.resolutions.Length; i++)
            {
                data = new Dropdown.OptionData() { text = Screen.resolutions[i].ToString() };
                _screenResolutionDropdown.options.Add(data);
                if (data.text == Screen.currentResolution.ToString())
                    _screenResolutionDropdown.value = i;
            }
            _fulscreenToggle.isOn = _videoManager.Fullscreen;
        }
        private void TranslateText()
        {
            _languageText.text = _localizationManager.SettingsTable.Value.GetEntry(Language.ToString())?.GetLocalizedString();
            _gameModeText.text = _localizationManager.SettingsTable.Value.GetEntry(GameMode.ToString())?.GetLocalizedString();
            _simpleModeText.text = _localizationManager.SettingsTable.Value.GetEntry(SimpleMode.ToString())?.GetLocalizedString();
            _realModeText.text = _localizationManager.SettingsTable.Value.GetEntry(RealMode.ToString())?.GetLocalizedString();
            _simpleModeInformationText.text = _localizationManager.SettingsTable.Value.GetEntry(SimpleModeInformation.ToString())?.GetLocalizedString();
            _realModeInformationText.text = _localizationManager.SettingsTable.Value.GetEntry(RealModeInformation.ToString())?.GetLocalizedString();
            _chooseFeedingDayText.text = _localizationManager.SettingsTable.Value.GetEntry(ChooseFeedDay.ToString())?.GetLocalizedString();

            _dayOfFeedingDropdown.options[0].text = _localizationManager.SettingsTable.Value.GetEntry(Monday.ToString())?.GetLocalizedString();
            _dayOfFeedingDropdown.options[1].text = _localizationManager.SettingsTable.Value.GetEntry(Tuesday.ToString())?.GetLocalizedString();
            _dayOfFeedingDropdown.options[2].text = _localizationManager.SettingsTable.Value.GetEntry(Wednesday.ToString())?.GetLocalizedString();
            _dayOfFeedingDropdown.options[3].text = _localizationManager.SettingsTable.Value.GetEntry(Thursday.ToString())?.GetLocalizedString();
            _dayOfFeedingDropdown.options[4].text = _localizationManager.SettingsTable.Value.GetEntry(Friday.ToString())?.GetLocalizedString();
            _dayOfFeedingDropdown.options[5].text = _localizationManager.SettingsTable.Value.GetEntry(Saturday.ToString())?.GetLocalizedString();
            _dayOfFeedingDropdown.options[6].text = _localizationManager.SettingsTable.Value.GetEntry(Sunday.ToString())?.GetLocalizedString();

            _dayOfFeedingDropdown.captionText.text = _dayOfFeedingDropdown.options[_dayOfFeedingDropdown.value].text;

            _soundText.text = _localizationManager.SettingsTable.Value.GetEntry(Sounds.ToString())?.GetLocalizedString();
            _musicText.text = _localizationManager.SettingsTable.Value.GetEntry(Music.ToString())?.GetLocalizedString();
            _screenResolutionText.text = _localizationManager.SettingsTable.Value.GetEntry(ScreenResolution.ToString())?.GetLocalizedString();
            _fullscreenText.text = _localizationManager.SettingsTable.Value.GetEntry(Fullscreen.ToString())?.GetLocalizedString();
            _backText.text = _localizationManager.SettingsTable.Value.GetEntry(Back.ToString())?.GetLocalizedString();
        }
    }
}