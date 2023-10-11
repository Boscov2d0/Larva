using Larva.Data;
using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Menu.UI.View;
using Larva.Tools;

namespace Larva.Menu.UI.Controller
{
    public class PhoneSettingsUIController : SettingsUIController
    {
        private readonly SaveLoadManager _saveLoadManager;

        public PhoneSettingsUIController(LocalizationManager localizationManager, SaveLoadManager saveLoadManager, 
                                         GameManager gameManager, UIManager uiManager, 
                                         AudioManager audioManager) : base(localizationManager, gameManager, audioManager)
        {
            _saveLoadManager = saveLoadManager;

            PhoneSettingsCanvasView settingsCanvasView = ResourcesLoader.InstantiateAndGetObject<PhoneSettingsCanvasView>(uiManager.PathForUIObjects + uiManager.PhoneSettingsCanvasPath);
            AddGameObject(settingsCanvasView.gameObject);
            settingsCanvasView.Initialize(SetRuLanguage, SetEnLanguage, SetZhLanguage, SetSoundVolume, SetMusicVolume, Back);
        }
        protected override void Back()
        {
            Saver.SaveGamePhoneSettingsData(_localizationManager, _saveLoadManager, _audioManager);

            base.Back();
        }
    }
}