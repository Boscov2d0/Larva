using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Menu.UI.View;
using Larva.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Larva.Menu.UI.Controller
{
    public class PCSettingsUIController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;
        private readonly PCSettingsCanvasView _settingsCanvasView;

        private int _width;
        private int _height;
        private int _refreshRate;

        public PCSettingsUIController(GameManager gameManager, UIManager uiManager)
        {
            _gameManager = gameManager;

            _settingsCanvasView = ResourcesLoader.InstantiateAndGetObject<PCSettingsCanvasView>(uiManager.PathForUIObjects + uiManager.PCSettingsCanvasPath);
            AddGameObject(_settingsCanvasView.gameObject);
            _settingsCanvasView.Init(SetRuLanguage, SetEnLanguage, SetZhLanguage, SetSoundVolume, SetMusicVolume, SetScreenResolition, SetFullscreen, Back);
        }
        private void SetRuLanguage() => Debug.Log("ru_RU language");
        private void SetEnLanguage() => Debug.Log("en_US language");
        private void SetZhLanguage() => Debug.Log("zh_ZH language");
        private void SetSoundVolume(float value) => Debug.Log(value);
        private void SetMusicVolume(float value) => Debug.Log(value);
        private void SetScreenResolition(int parameters)
        {
            ParseDropdown(_settingsCanvasView.ScreenResolutionDropdown);
            Screen.SetResolution(_width, _height, Screen.fullScreen, _refreshRate);
        }
        private void SetFullscreen(bool flag) => Screen.fullScreen = flag;
        private void Back() => _gameManager.GameState.Value = GameState.Menu;
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