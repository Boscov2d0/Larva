using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Menu.UI.View;
using Larva.Tools;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Larva.Menu.UI.Controller
{
    public class PhoneSettingsUIController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;
        private readonly AudioManager _audioManager;

        private const int ru = 0;
        private const int en = 1;
        private const int zh = 2;

        public PhoneSettingsUIController(SaveLoadManager saveLoadManager, LocalizationManager localizationManager, 
                                         GameManager gameManager, UIManager uiManager, AudioManager audioManager)
        {
            _saveLoadManager = saveLoadManager;
            _gameManager = gameManager;
            _audioManager = audioManager;

            PhoneSettingsCanvasView settingsCanvasView = ResourcesLoader.InstantiateAndGetObject<PhoneSettingsCanvasView>(uiManager.PathForUIObjects + uiManager.PhoneSettingsCanvasPath);
            AddGameObject(settingsCanvasView.gameObject);
            settingsCanvasView.Init(SetRuLanguage, SetEnLanguage, SetZhLanguage, SetSoundVolume, SetMusicVolume, Back);
        }
        private void SetRuLanguage()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[ru];
            _audioManager.State.Value = AudioStates.Button;
        }
        private void SetEnLanguage()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[en];
            _audioManager.State.Value = AudioStates.Button;
        }
        private void SetZhLanguage()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[zh];
            _audioManager.State.Value = AudioStates.Button;
        }
        private void SetSoundVolume(float value)
        {
            _audioManager.SoundsVolume = value;
            _audioManager.AudioMixer.SetFloat(AudioKeys.Sound.ToString(), Mathf.Log10(value) * 20);

            if (value == 0)
                _audioManager.AudioMixer.SetFloat(AudioKeys.Sound.ToString(), -80);
        }
        private void SetMusicVolume(float value)
        {
            _audioManager.MusicVolume = value;
            _audioManager.AudioMixer.SetFloat(AudioKeys.Music.ToString(), Mathf.Log10(value) * 20);

            if (value == 0)
                _audioManager.AudioMixer.SetFloat(AudioKeys.Music.ToString(), -80);
        }
        private void Back()
        {
            Saver.SaveGamePhoneSettingsData(_saveLoadManager, _localizationManager, _audioManager);
            _gameManager.GameState.Value = GameState.Menu;
            _audioManager.State.Value = AudioStates.ButtonApply;
        }
    }
}