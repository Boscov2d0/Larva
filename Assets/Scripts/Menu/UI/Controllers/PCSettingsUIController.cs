using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Menu.UI.View;
using Larva.Tools;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Larva.Menu.UI.Controller
{
    public class PCSettingsUIController : ObjectsDisposer
    {
        private readonly SaveLoadManager _saveLoadManager;
        private readonly GameManager _gameManager;
        private readonly AudioManager _audioManager;
        private readonly VideoManager _videoManager;
        private readonly PCSettingsCanvasView _settingsCanvasView;

        private const int ru = 0;
        private const int en = 1;
        private const int zh = 2;

        private int _width;
        private int _height;
        private int _refreshRate;

        public PCSettingsUIController(SaveLoadManager saveLoadManager, GameManager gameManager, UIManager uiManager, AudioManager audioManager, VideoManager videoManager)
        {
            _saveLoadManager = saveLoadManager;
            _gameManager = gameManager;
            _audioManager = audioManager;
            _videoManager = videoManager;

            _settingsCanvasView = ResourcesLoader.InstantiateAndGetObject<PCSettingsCanvasView>(uiManager.PathForUIObjects + uiManager.PCSettingsCanvasPath);
            AddGameObject(_settingsCanvasView.gameObject);
            _settingsCanvasView.Init(SetRuLanguage, SetEnLanguage, SetZhLanguage, SetSoundVolume, SetMusicVolume, SetScreenResolition, SetFullscreen, Back);
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
        private void SetScreenResolition(int parameters)
        {
            ParseDropdown(_settingsCanvasView.ScreenResolutionDropdown);
            Screen.SetResolution(_width, _height, Screen.fullScreen, _refreshRate);
            _audioManager.State.Value = AudioStates.Button;
        }
        private void SetFullscreen(bool flag)
        {
            Screen.fullScreen = flag;
            _audioManager.State.Value = AudioStates.Button;
        }
        private void Back()
        {
            Saver.SaveGamePCSettingsData(_saveLoadManager, _localizationManager, _audioManager, _videoManager);
            _gameManager.GameState.Value = GameState.Menu;
            _audioManager.State.Value = AudioStates.ButtonApply;
        }
        private void ParseDropdown(Dropdown screenResolutionDropdown)
        {
            char[] delimiterChars = { 'x', '@', 'H' };
            string[] resolutions = screenResolutionDropdown.options[screenResolutionDropdown.value].text.Split(delimiterChars);

            _width = int.Parse(resolutions[0]);
            _height = int.Parse(resolutions[1]);
            _refreshRate = int.Parse(resolutions[2]);
        }

    }
}