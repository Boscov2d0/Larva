using Larva.Data;
using Larva.Menu.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using static Larva.Tools.LocalizationTextKeys.LocalizationSettingsTextKeys;

namespace Larva.Menu.UI.View
{
    public class PhoneSettingsCanvasView : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private House.Data.HouseManager _houseManager;

        [SerializeField] private Button _ruLanguageButton;
        [SerializeField] private Button _enLanguageButton;
        [SerializeField] private Button _zhLanguageButton;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Button _simpleModeButton;
        [SerializeField] private Button _realModeButton;
        [SerializeField] private Dropdown _dayOfFeedingDropdown;
        [SerializeField] private Dropdown _timeOfDayDropdown;
        [SerializeField] private Button _backButton;

        [SerializeField] private Text _languageText;
        [SerializeField] private Text _soundText;
        [SerializeField] private Text _musicText;
        [SerializeField] private Text _gameModeText;
        [SerializeField] private Text _simpleModeText;
        [SerializeField] private Text _realModeText;
        [SerializeField] private Text _simpleModeInformationText;
        [SerializeField] private Text _realModeInformationText;
        [SerializeField] private Text _chooseFeedingDayText;
        [SerializeField] private Text _timeOfDayText;
        [SerializeField] private Text _backText;

        private UnityAction _setRuLanguage;
        private UnityAction _setEnLanguage;
        private UnityAction _setZhLanguage;
        private UnityAction<float> _setSoundVolume;
        private UnityAction<float> _setMusicVolume;
        private UnityAction _simpleMode;
        private UnityAction _realMode;
        private UnityAction<int> _setDayOfFeed;
        private UnityAction<int> _setTimeOfDay;
        private UnityAction _back;

        public void Initialize(UnityAction setRuLanguage, UnityAction setEnLanguage, UnityAction setZhLanguage,
                               UnityAction<float> setSoundVolume, UnityAction<float> setMusicVolume,
                               UnityAction simpleMode, UnityAction realMode, UnityAction<int> setDayOfFeed,
                               UnityAction<int> setTimeOfDay, UnityAction back)
        {
            _setRuLanguage = setRuLanguage;
            _setEnLanguage = setEnLanguage;
            _setZhLanguage = setZhLanguage;
            _setSoundVolume = setSoundVolume;
            _setMusicVolume = setMusicVolume;
            _simpleMode = simpleMode;
            _realMode = realMode;
            _setDayOfFeed = setDayOfFeed;
            _setTimeOfDay = setTimeOfDay;
            _back = back;

            _ruLanguageButton.onClick.AddListener(_setRuLanguage);
            _enLanguageButton.onClick.AddListener(_setEnLanguage);
            _zhLanguageButton.onClick.AddListener(_setZhLanguage);
            _soundSlider.onValueChanged.AddListener(_setSoundVolume);
            _musicSlider.onValueChanged.AddListener(_setMusicVolume);
            _simpleModeButton.onClick.AddListener(_simpleMode);
            _realModeButton.onClick.AddListener(_realMode);
            _dayOfFeedingDropdown.onValueChanged.AddListener(_setDayOfFeed);
            _timeOfDayDropdown.onValueChanged.AddListener(_setTimeOfDay);
            _backButton.onClick.AddListener(_back);

            SetUI();
            _localizationManager.SettingsTable.SubscribeOnChange(TranslateText);
            TranslateText();
        }
        private void OnDestroy()
        {
            _localizationManager.SettingsTable.UnSubscribeOnChange(TranslateText);

            _ruLanguageButton.onClick.RemoveListener(_setRuLanguage);
            _enLanguageButton.onClick.RemoveListener(_setEnLanguage);
            _zhLanguageButton.onClick.RemoveListener(_setZhLanguage);
            _soundSlider.onValueChanged.RemoveListener(_setSoundVolume);
            _musicSlider.onValueChanged.RemoveListener(_setMusicVolume);
            _simpleModeButton.onClick.RemoveListener(_simpleMode);
            _realModeButton.onClick.RemoveListener(_realMode);
            _dayOfFeedingDropdown.onValueChanged.RemoveListener(_setDayOfFeed);
            _timeOfDayDropdown.onValueChanged.RemoveListener(_setTimeOfDay);
            _backButton.onClick.RemoveListener(_back);
        }
        private void SetUI()
        {
            _musicSlider.value = _audioManager.MusicVolume;
            _soundSlider.value = _audioManager.SoundsVolume;
            _dayOfFeedingDropdown.value = (int)_houseManager.DayForGiveFood;
            _timeOfDayDropdown.value = (int)_gameManager.DayTime.Value;
        }
        private void TranslateText()
        {
            _languageText.text = _localizationManager.SettingsTable.Value.GetEntry(Language.ToString())?.GetLocalizedString();
            _soundText.text = _localizationManager.SettingsTable.Value.GetEntry(Sounds.ToString())?.GetLocalizedString();
            _musicText.text = _localizationManager.SettingsTable.Value.GetEntry(Music.ToString())?.GetLocalizedString();
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

            _timeOfDayText.text = _localizationManager.SettingsTable.Value.GetEntry(DayTime.ToString())?.GetLocalizedString();
            _timeOfDayDropdown.options[0].text = _localizationManager.SettingsTable.Value.GetEntry(Auto.ToString())?.GetLocalizedString();
            _timeOfDayDropdown.options[1].text = _localizationManager.SettingsTable.Value.GetEntry(Day.ToString())?.GetLocalizedString();
            _timeOfDayDropdown.options[2].text = _localizationManager.SettingsTable.Value.GetEntry(Evening.ToString())?.GetLocalizedString();
            _timeOfDayDropdown.options[3].text = _localizationManager.SettingsTable.Value.GetEntry(Night.ToString())?.GetLocalizedString();
            _timeOfDayDropdown.captionText.text = _timeOfDayDropdown.options[_timeOfDayDropdown.value].text;

            _backText.text = _localizationManager.SettingsTable.Value.GetEntry(Back.ToString())?.GetLocalizedString();
        }
    }
}