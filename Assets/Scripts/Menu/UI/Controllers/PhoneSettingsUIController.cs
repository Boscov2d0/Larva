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
        private readonly AudioManager _audioManager;

        public PhoneSettingsUIController(GameManager gameManager, UIManager uiManager, AudioManager audioManager)
        {
            _gameManager = gameManager;
            _audioManager = audioManager;

            PhoneSettingsCanvasView settingsCanvasView = ResourcesLoader.InstantiateAndGetObject<PhoneSettingsCanvasView>(uiManager.PathForUIObjects + uiManager.PhoneSettingsCanvasPath);
            AddGameObject(settingsCanvasView.gameObject);
            settingsCanvasView.Init(SetRuLanguage, SetEnLanguage, SetZhLanguage, SetSoundVolume, SetMusicVolume, Back);
        }
        private void SetRuLanguage()
        {
            Debug.Log("ru_RU language");
            _audioManager.State.Value = AudioStates.Button;
        }
        private void SetEnLanguage()
        {
            Debug.Log("en_US language");
            _audioManager.State.Value = AudioStates.Button;
        }
        private void SetZhLanguage()
        {
            Debug.Log("zh_ZH language");
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