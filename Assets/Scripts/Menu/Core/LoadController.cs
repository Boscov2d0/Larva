using Larva.Data;
using Larva.Menu.Data;
using Larva.Tools;
using System.Collections.Generic;

namespace Larva.Menu.Core
{
    public class LoadController : ObjectsDisposer
    {
        private readonly LocalizationManager _localizationManager;
        private readonly SaveLoadManager _saveLoadManager;
        private readonly AudioManager _audioManager;
        private readonly VideoManager _videoManager;

        public LoadController(LocalizationManager localizationManager, SaveLoadManager saveLoadManager, 
                              AudioManager audioManager, VideoManager videoManager)
        {
            _localizationManager = localizationManager;
            _saveLoadManager = saveLoadManager;
            _audioManager = audioManager;
            _videoManager = videoManager;

            _saveLoadManager.HouseData.Pots = new List<StructsData.PotData>();

            LoadGameSettingsData();
        }
        private void LoadGameSettingsData()
        {
            _saveLoadManager.GameSettingsData = JSONDataLoadSaver<StructsData.GameSettingsData>.Load(_saveLoadManager.GameSettingsDataPath);

            _localizationManager.Language = _saveLoadManager.GameSettingsData.Language;
            _audioManager.SoundsVolume = _saveLoadManager.GameSettingsData.SoundsVolume;
            _audioManager.MusicVolume = _saveLoadManager.GameSettingsData.MusicVolume;
            _videoManager.ScreenResolution = _saveLoadManager.GameSettingsData.ScreenResolution;
            _videoManager.Fullscreen = _saveLoadManager.GameSettingsData.Fullscreen;
        }
    }
}