using Larva.Menu.Data;

namespace Larva.Menu.Tools
{
    public static class Saver
    {
        public static void SaveGamePCSettingsData(SaveLoadManager saveLoadManager, LocalizationManager localizationManager, AudioManager audioManager, VideoManager videoManager)
        {
            saveLoadManager.GameSettingsData.Language = localizationManager.Language.Value;
            saveLoadManager.GameSettingsData.SoundsVolume = audioManager.SoundsVolume;
            saveLoadManager.GameSettingsData.MusicVolume = audioManager.MusicVolume;
            saveLoadManager.GameSettingsData.ScreenResolution = videoManager.ScreenResolution;
            saveLoadManager.GameSettingsData.Fullscreen = videoManager.Fullscreen;

            JSONDataLoadSaver<StructsData.GameSettingsData>.SaveData(saveLoadManager.GameSettingsData, saveLoadManager.GameSettingsDataPath);
        }
        public static void SaveGamePhoneSettingsData(SaveLoadManager saveLoadManager, LocalizationManager localizationManager, AudioManager audioManager)
        {
            saveLoadManager.GameSettingsData.Language = localizationManager.Language.Value;
            saveLoadManager.GameSettingsData.SoundsVolume = audioManager.SoundsVolume;
            saveLoadManager.GameSettingsData.MusicVolume = audioManager.MusicVolume;

            JSONDataLoadSaver<StructsData.GameSettingsData>.SaveData(saveLoadManager.GameSettingsData, saveLoadManager.GameSettingsDataPath);
        }
    }
}