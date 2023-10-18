using Larva.Data;
using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Tools;
using System;
using UnityEngine;
using UnityEngine.Localization.Settings;

using static Larva.Tools.AudioKeys;
using static Larva.Tools.Keys;

namespace Larva.Menu.UI.Controller
{
    public class SettingsUIController : ObjectsDisposer
    {
        protected readonly LocalizationManager _localizationManager;
        protected readonly GameManager _gameManager;
        protected readonly AudioManager _audioManager;
        protected readonly House.Data.HouseManager _houseManager;
        private const int ru = 0;
        private const int en = 1;
        private const int zh = 2;

        public SettingsUIController() { }
        public SettingsUIController(LocalizationManager localizationManager, GameManager gameManager,
                                    AudioManager audioManager, House.Data.HouseManager houseManager)
        {
            _localizationManager = localizationManager;
            _gameManager = gameManager;
            _audioManager = audioManager;
            _houseManager = houseManager;
        }
        protected void SimpleMode() => _houseManager.GameMode = GameMode.Simple;
        protected void RealMode() => _houseManager.GameMode = GameMode.Real;
        protected void SetDayOfFeed(int parameters) => _houseManager.DayForGiveFood = (DayOfWeek)parameters + 1;
        protected void SetRuLanguage()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[ru];
            _localizationManager.Language = LocalizationSettings.AvailableLocales.Locales[ru];
            _audioManager.State.Value = AudioStates.Button;
        }
        protected void SetEnLanguage()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[en];
            _localizationManager.Language = LocalizationSettings.AvailableLocales.Locales[en];
            _audioManager.State.Value = AudioStates.Button;
        }
        protected void SetZhLanguage()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[zh];
            _localizationManager.Language = LocalizationSettings.AvailableLocales.Locales[zh];
            _audioManager.State.Value = AudioStates.Button;
        }
        protected void SetSoundVolume(float value)
        {
            _audioManager.SoundsVolume = value;
            _audioManager.AudioMixer.SetFloat(MixerGroups.Sound.ToString(), Mathf.Log10(value) * 20);

            if (value == 0)
                _audioManager.AudioMixer.SetFloat(MixerGroups.Sound.ToString(), -80);
        }
        protected void SetMusicVolume(float value)
        {
            _audioManager.MusicVolume = value;
            _audioManager.AudioMixer.SetFloat(MixerGroups.Music.ToString(), Mathf.Log10(value) * 20);

            if (value == 0)
                _audioManager.AudioMixer.SetFloat(MixerGroups.Music.ToString(), -80);
        }
        protected virtual void Back()
        {
            _gameManager.GameState.Value = GameState.Menu;
            _audioManager.State.Value = AudioStates.ButtonApply;
            _houseManager.SaveLoadState.Value = SaveState.SaveHouseData;
        }
    }
}