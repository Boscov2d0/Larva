using Larva.Data;
using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Menu.UI.View;
using Larva.Tools;
using UnityEngine;
using UnityEngine.UI;

using static Larva.Tools.AudioKeys;

namespace Larva.Menu.UI.Controller
{
    public class PCSettingsUIController : SettingsUIController
    {
        private readonly SaveLoadManager _saveLoadManager;
        private readonly VideoManager _videoManager;
        private readonly PCSettingsCanvasView _settingsCanvasView;

        private int _width;
        private int _height;
        private int _refreshRate;

        public PCSettingsUIController(LocalizationManager localizationManager, SaveLoadManager saveLoadManager,
                                      GameManager gameManager, UIManager uiManager,
                                      AudioManager audioManager, VideoManager videoManager) : base(localizationManager, gameManager, audioManager)
        {
            _saveLoadManager = saveLoadManager;
            _videoManager = videoManager;

            _settingsCanvasView = ResourcesLoader.InstantiateAndGetObject<PCSettingsCanvasView>(uiManager.PathForUIObjects + uiManager.PCSettingsCanvasPath);
            AddGameObject(_settingsCanvasView.gameObject);
            _settingsCanvasView.Initialize(SetRuLanguage, SetEnLanguage, SetZhLanguage, SetSoundVolume, SetMusicVolume, SetScreenResolition, SetFullscreen, Back);
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
            _videoManager.Fullscreen = flag;
            _audioManager.State.Value = AudioStates.Button;
        }
        private void ParseDropdown(Dropdown screenResolutionDropdown)
        {
            char[] delimiterChars = { 'x', '@', 'H' };
            string[] resolutions = screenResolutionDropdown.options[screenResolutionDropdown.value].text.Split(delimiterChars);

            _width = int.Parse(resolutions[0]);
            _height = int.Parse(resolutions[1]);
            _refreshRate = int.Parse(resolutions[2]);
        }
        protected override void Back()
        {
            Saver.SaveGamePCSettingsData(_localizationManager, _saveLoadManager, _audioManager, _videoManager);

            base.Back();
        }
    }
}