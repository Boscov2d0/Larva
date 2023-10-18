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

        private const int _gamePanelIndex = 0;
        private const int _audioPanelIndex = 1;
        private const int _videoPanelIndex = 2;

        private int _panelIndex;
        private int _width;
        private int _height;
        private int _refreshRate;

        public PCSettingsUIController(LocalizationManager localizationManager, SaveLoadManager saveLoadManager,
                                      GameManager gameManager, UIManager uiManager,
                                      AudioManager audioManager, VideoManager videoManager,
                                      House.Data.HouseManager houseManager) : base(localizationManager, gameManager, audioManager, houseManager)
        {
            _saveLoadManager = saveLoadManager;
            _videoManager = videoManager;

            _settingsCanvasView = ResourcesLoader.InstantiateAndGetObject<PCSettingsCanvasView>(uiManager.PathForUIObjects + uiManager.PCSettingsCanvasPath);
            AddGameObject(_settingsCanvasView.gameObject);
            _settingsCanvasView.Initialize(OpenGamePanel, OpenAudioPanel, OpenVideoPanel,
                                           SetRuLanguage, SetEnLanguage, SetZhLanguage,
                                           SimpleMode, RealMode, SetDayOfFeed,
                                           SetSoundVolume, SetMusicVolume,
                                           SetScreenResolition, SetFullscreen,
                                           Back);
        }
        private void OpenGamePanel()
        {
            _panelIndex = _gamePanelIndex;
            OnPanelActivate();
        }
        private void OpenAudioPanel()
        {
            _panelIndex = _audioPanelIndex;
            OnPanelActivate();
        }
        private void OpenVideoPanel()
        {
            _panelIndex = _videoPanelIndex;
            OnPanelActivate();
        }
        private void OnPanelActivate()
        {
            for (int i = 0; i < _settingsCanvasView.Panels.Count; i++)
                _settingsCanvasView.Panels[i].SetActive(false);

            _settingsCanvasView.Panels[_panelIndex].SetActive(true);
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