using Larva.Menu.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Larva.Menu.UI.View
{
    public class PCSettingsCanvasView : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private Button _ruLanguageButton;
        [SerializeField] private Button _enLanguageButton;
        [SerializeField] private Button _zhLanguageButton;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Dropdown _screenResolutionDropdown;
        [SerializeField] private Toggle _fulscreenToggle;
        [SerializeField] private Button _backButton;

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

            _ruLanguageButton.onClick.AddListener(_setRuLanguage);
            _enLanguageButton.onClick.AddListener(_setEnLanguage);
            _zhLanguageButton.onClick.AddListener(_setZhLanguage);
            _soundSlider.onValueChanged.AddListener(_setSoundVolume);
            _musicSlider.onValueChanged.AddListener(_setMusicVolume);
            _screenResolutionDropdown.onValueChanged.AddListener(_setScreenResolution);
            _fulscreenToggle.onValueChanged.AddListener(_setFullscreen);
            _backButton.onClick.AddListener(_back);

            SetUI();
        }
        private void OnDestroy()
        {
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
    }
}