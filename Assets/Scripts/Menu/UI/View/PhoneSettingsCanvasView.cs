using Larva.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using static Larva.Tools.LocalizationTextKeys.LocalizationSettingsTextKeys;

namespace Larva.Menu.UI.View
{
    public class PhoneSettingsCanvasView : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private Button _ruLanguageButton;
        [SerializeField] private Button _enLanguageButton;
        [SerializeField] private Button _zhLanguageButton;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Button _backButton;

        [SerializeField] private Text _languageText;
        [SerializeField] private Text _soundText;
        [SerializeField] private Text _musicText;
        [SerializeField] private Text _backText;

        private UnityAction _setRuLanguage;
        private UnityAction _setEnLanguage;
        private UnityAction _setZhLanguage;
        private UnityAction<float> _setSoundVolume;
        private UnityAction<float> _setMusicVolume;
        private UnityAction _back;

        public void Initialize(UnityAction setRuLanguage, UnityAction setEnLanguage, UnityAction setZhLanguage,
                 UnityAction<float> setSoundVolume, UnityAction<float> setMusicVolume,
                 UnityAction back)
        {
            _setRuLanguage = setRuLanguage;
            _setEnLanguage = setEnLanguage;
            _setZhLanguage = setZhLanguage;
            _setSoundVolume = setSoundVolume;
            _setMusicVolume = setMusicVolume;
            _back = back;

            _ruLanguageButton.onClick.AddListener(_setRuLanguage);
            _enLanguageButton.onClick.AddListener(_setEnLanguage);
            _zhLanguageButton.onClick.AddListener(_setZhLanguage);
            _soundSlider.onValueChanged.AddListener(_setSoundVolume);
            _musicSlider.onValueChanged.AddListener(_setMusicVolume);
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
            _backButton.onClick.RemoveListener(_back);
        }
        private void SetUI()
        {
            _musicSlider.value = _audioManager.MusicVolume;
            _soundSlider.value = _audioManager.SoundsVolume;
        }
        private void TranslateText()
        {
            _languageText.text = _localizationManager.SettingsTable.Value.GetEntry(Language.ToString())?.GetLocalizedString();
            _soundText.text = _localizationManager.SettingsTable.Value.GetEntry(Sounds.ToString())?.GetLocalizedString();
            _musicText.text = _localizationManager.SettingsTable.Value.GetEntry(Music.ToString())?.GetLocalizedString();
            _backText.text = _localizationManager.SettingsTable.Value.GetEntry(Back.ToString())?.GetLocalizedString();
        }
    }
}