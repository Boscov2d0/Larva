using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Menu.UI.View;
using Larva.Tools;
using UnityEngine;

namespace Larva.Menu.UI.Controller
{
    public class PhoneSettingsUIController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;

        public PhoneSettingsUIController(GameManager gameManager, UIManager uiManager)
        {
            _gameManager = gameManager;

            PhoneSettingsCanvasView settingsCanvasView = ResourcesLoader.InstantiateAndGetObject<PhoneSettingsCanvasView>(uiManager.PathForUIObjects + uiManager.PhoneSettingsCanvasPath);
            AddGameObject(settingsCanvasView.gameObject);
            settingsCanvasView.Init(SetRuLanguage, SetEnLanguage, SetZhLanguage, SetSoundVolume, SetMusicVolume, Back);
        }
        private void SetRuLanguage() => Debug.Log("ru_RU language");
        private void SetEnLanguage() => Debug.Log("en_US language");
        private void SetZhLanguage() => Debug.Log("zh_ZH language");
        private void SetSoundVolume(float value) => Debug.Log(value);
        private void SetMusicVolume(float value) => Debug.Log(value);
        private void Back() => _gameManager.GameState.Value = GameState.Menu;
    }
}