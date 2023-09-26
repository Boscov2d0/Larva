using Larva.Menu.Data;
using Larva.Menu.Tools;
using Larva.Menu.UI.View;
using Larva.Tools;
using UnityEngine;

namespace Larva.Menu.UI.Controller
{
    public class PhoneSettingsUIController : ObjectsDisposer
    {
        private readonly LocalizationManager _localizationManager;
        private readonly GameManager _gameManager;
        private readonly AudioManager _audioManager;

        public PhoneSettingsUIController(LocalizationManager localizationManager, GameManager gameManager, UIManager uiManager, AudioManager audioManager)
        {
            _localizationManager = localizationManager;
            _gameManager = gameManager;
            _audioManager = audioManager;

            PhoneSettingsCanvasView settingsCanvasView = ResourcesLoader.InstantiateAndGetObject<PhoneSettingsCanvasView>(uiManager.PathForUIObjects + uiManager.PhoneSettingsCanvasPath);
            AddGameObject(settingsCanvasView.gameObject);
            settingsCanvasView.Init(SetRuLanguage, SetEnLanguage, SetZhLanguage, SetSoundVolume, SetMusicVolume, Back);
        }
        private void SetRuLanguage()
        {
            _localizationManager.Language.Value = LanguageKeys.ru_RU.ToString();
            _audioManager.State.Value = AudioStates.Button;
        }
        private void SetEnLanguage()
        {
            _localizationManager.Language.Value = LanguageKeys.en_US.ToString();
            _audioManager.State.Value = AudioStates.Button;
        }
        private void SetZhLanguage()
        {
            _localizationManager.Language.Value = LanguageKeys.zh_ZH.ToString();
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
        private void Back()
        {
            _gameManager.GameState.Value = GameState.Menu;
            _audioManager.State.Value = AudioStates.ButtonApply;
        }
    }
}