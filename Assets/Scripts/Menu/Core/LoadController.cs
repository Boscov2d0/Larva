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
        private readonly GameManager _gameManager;
        private readonly AudioManager _audioManager;
        private readonly VideoManager _videoManager;
        private readonly LarvaProfile _larvaProfile;

        public LoadController(LocalizationManager localizationManager, SaveLoadManager saveLoadManager, 
                              GameManager gameManager, AudioManager audioManager, 
                              VideoManager videoManager, LarvaProfile larvaProfile)
        {
            _localizationManager = localizationManager;
            _saveLoadManager = saveLoadManager;
            _gameManager = gameManager;
            _audioManager = audioManager;
            _videoManager = videoManager;
            _larvaProfile = larvaProfile;

            _saveLoadManager.HouseData.Pots = new List<StructsData.PotData>();

            LoadGameSettingsData();
            LoadLarvaData();
        }
        private void LoadGameSettingsData()
        {
            _saveLoadManager.GameSettingsData = JSONDataLoadSaver<StructsData.GameSettingsData>.Load(_saveLoadManager.GameSettingsDataPath);

            _localizationManager.Language = _saveLoadManager.GameSettingsData.Language;
            _audioManager.SoundsVolume = _saveLoadManager.GameSettingsData.SoundsVolume;
            _audioManager.MusicVolume = _saveLoadManager.GameSettingsData.MusicVolume;
            _videoManager.ScreenResolution = _saveLoadManager.GameSettingsData.ScreenResolution;
            _videoManager.Fullscreen = _saveLoadManager.GameSettingsData.Fullscreen;
            _gameManager.DayTime.Value = _saveLoadManager.GameSettingsData.DayTime;
        }
        private void LoadLarvaData()
        {
            _saveLoadManager.LarvaData = JSONDataLoadSaver<StructsData.LarvaData>.Load(_saveLoadManager.LarvaDataPath);

            _larvaProfile.HeadSkin = _saveLoadManager.LarvaData.HeadSkin;
            _larvaProfile.BodySkin = _saveLoadManager.LarvaData.BodySkin;
        }
    }
}