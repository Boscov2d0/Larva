using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Larva.Menu.UI.View
{
    public class PCSettingsCanvasView : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private Button _ruLanguageButton;
        [SerializeField] private Button _enLanguageButton;
        [SerializeField] private Button _zhLanguageButton;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Dropdown _screenResolutionDropdown;
        [SerializeField] private Toggle _fulscreenToggle;
        [SerializeField] private Button _backButton;

        [SerializeField] private Text _languageText;
        [SerializeField] private Text _soundText;
        [SerializeField] private Text _musicText;
        [SerializeField] private Text _screenResolutionText;
        [SerializeField] private Text _fullscreenText;
        [SerializeField] private Text _backText;

        private UnityAction _setRuLanguage;
        private UnityAction _setEnLanguage;
        private UnityAction _setZhLanguage;
        private UnityAction<float> _setSoundVolume;
        private UnityAction<float> _setMusicVolume;
        private UnityAction<int> _setScreenResolution;
        private UnityAction<bool> _setFullscreen;
        private UnityAction _back;

        public Dropdown ScreenResolutionDropdown { get { return _screenResolutionDropdown; } private set { } }

        public void Init(UnityAction setRuLanguage, UnityAction setEnLanguage, UnityAction setZhLanguage,
                         UnityAction<float> setSoundVolume, UnityAction<float> setMusicVolume,
                         UnityAction<int> setScreenResolution, UnityAction<bool> setFullscreen,
                         UnityAction back)
        {
            _setRuLanguage = setRuLanguage;
            _setEnLanguage = setEnLanguage;
            _setZhLanguage = setZhLanguage;
            _setSoundVolume = setSoundVolume;
            _setMusicVolume = setMusicVolume;
            _setScreenResolution = setScreenResolution;
            _setFullscreen = setFullscreen;
            _back = back;

            _localizationManager.Language.SubscribeOnChange(TranslateText);

            _ruLanguageButton.onClick.AddListener(_setRuLanguage);
            _enLanguageButton.onClick.AddListener(_setEnLanguage);
            _zhLanguageButton.onClick.AddListener(_setZhLanguage);
            _soundSlider.onValueChanged.AddListener(_setSoundVolume);
            _musicSlider.onValueChanged.AddListener(_setMusicVolume);
            _screenResolutionDropdown.onValueChanged.AddListener(_setScreenResolution);
            _fulscreenToggle.onValueChanged.AddListener(_setFullscreen);
            _backButton.onClick.AddListener(_back);

            SetUI();
            //TranslateText();
        }
        private void OnDestroy()
        {
            _localizationManager.Language.UnSubscribeOnChange(TranslateText);

            _ruLanguageButton.onClick.RemoveListener(_setRuLanguage);
            _enLanguageButton.onClick.RemoveListener(_setEnLanguage);
            _zhLanguageButton.onClick.RemoveListener(_setZhLanguage);
            _soundSlider.onValueChanged.RemoveListener(_setSoundVolume);
            _musicSlider.onValueChanged.RemoveListener(_setMusicVolume);
            _screenResolutionDropdown.onValueChanged.RemoveListener(_setScreenResolution);
            _fulscreenToggle.onValueChanged.RemoveListener(_setFullscreen);
            _backButton.onClick.RemoveListener(_back);
        }
        private void SetUI()
        {
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
        }
        private void TranslateText()
        {
            _languageText.text = Localizator.GetLocalizedValue(_localizationManager.LocalizedSettingsText, LocalizationTextKeys.LocalizationSettingsTextKeys.Language);
            _soundText.text = Localizator.GetLocalizedValue(_localizationManager.LocalizedSettingsText, LocalizationTextKeys.LocalizationSettingsTextKeys.Sounds);
            _musicText.text = Localizator.GetLocalizedValue(_localizationManager.LocalizedSettingsText, LocalizationTextKeys.LocalizationSettingsTextKeys.Music);
            _screenResolutionText.text = Localizator.GetLocalizedValue(_localizationManager.LocalizedSettingsText, LocalizationTextKeys.LocalizationSettingsTextKeys.ScreenResolution);
            _fullscreenText.text = Localizator.GetLocalizedValue(_localizationManager.LocalizedSettingsText, LocalizationTextKeys.LocalizationSettingsTextKeys.Fullscreen);
            _backText.text = Localizator.GetLocalizedValue(_localizationManager.LocalizedSettingsText, LocalizationTextKeys.LocalizationSettingsTextKeys.Back);
        }
    }
}