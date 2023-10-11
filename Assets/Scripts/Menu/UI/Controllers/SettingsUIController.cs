using Larva.Data;
using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Tools;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Larva.Menu.UI.Controller
{
    public class SettingsUIController : ObjectsDisposer
    {
        protected readonly LocalizationManager _localizationManager;
        protected readonly GameManager _gameManager;
        protected readonly AudioManager _audioManager;

        private const int ru = 0;
        private const int en = 1;
        private const int zh = 2;

        public SettingsUIController() { }
        public SettingsUIController(LocalizationManager localizationManager, GameManager gameManager, AudioManager audioManager) 
        {
            _localizationManager = localizationManager;
            _gameManager = gameManager;
            _audioManager = audioManager;
        }

        protected void SetRuLanguage()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[ru];
            _localizationManager.Language = LocalizationSettings.AvailableLocales.Locales[ru];
            _audioManager.State.Value = AudioKeys.AudioStates.Button;
        }
        protected void SetEnLanguage()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[en];
            _localizationManager.Language = LocalizationSettings.AvailableLocales.Locales[en];
            _audioManager.State.Value = AudioKeys.AudioStates.Button;
        }
        protected void SetZhLanguage()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[zh];
            _localizationManager.Language = LocalizationSettings.AvailableLocales.Locales[zh];
            _audioManager.State.Value = AudioKeys.AudioStates.Button;
        }
        protected void SetSoundVolume(float value)
        {
            _audioManager.SoundsVolume = value;
            _audioManager.AudioMixer.SetFloat(AudioKeys.MixerGroups.Sound.ToString(), Mathf.Log10(value) * 20);

            if (value == 0)
                _audioManager.AudioMixer.SetFloat(AudioKeys.MixerGroups.Sound.ToString(), -80);
        }
        protected void SetMusicVolume(float value)
        {
            _audioManager.MusicVolume = value;
            _audioManager.AudioMixer.SetFloat(AudioKeys.MixerGroups.Music.ToString(), Mathf.Log10(value) * 20);

            if (value == 0)
                _audioManager.AudioMixer.SetFloat(AudioKeys.MixerGroups.Music.ToString(), -80);
        }
        protected virtual void Back()
        {
            _gameManager.GameState.Value = GameState.Menu;
            _audioManager.State.Value = AudioKeys.AudioStates.ButtonApply;
        }
    }
}