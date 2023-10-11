using Larva.Data;
using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Larva.Game.Core
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private List<AudioSource> _musics;
        [SerializeField] private AudioSource _gameOverSound;
        [SerializeField] private AudioSource _button;
        [SerializeField] private AudioSource _buttonApply;
        [SerializeField] private AudioSource _buttonCancel;

        private int _index;

        private void Start()
        {
            _index = Random.Range(0, _musics.Count);
            _musics[_index].Play();

            _gameManager.GameState.SubscribeOnChange(OnGameStateChange);
            _audioManager.State.SubscribeOnChange(OnAudioStateCahnge);
        }
        private void OnDestroy()
        {
            _gameManager.GameState.UnSubscribeOnChange(OnGameStateChange);
            _audioManager.State.UnSubscribeOnChange(OnAudioStateCahnge);
        }
        private void Update()
        {
            if (_musics[_index].isPlaying)
                return;

            SetNewMusic();
        }
        private void SetNewMusic()
        {
            _musics[_index].Stop();

            _index++;

            if (_index >= _musics.Count)
                _index = 0;

            _musics[_index].Play();
        }
        private void OnGameStateChange()
        {
            if (_gameManager.GameState.Value == GameState.Lose)
            {
                _gameOverSound.Play();
            }
        }
        private void OnAudioStateCahnge()
        {
            switch (_audioManager.State.Value)
            {
                case AudioKeys.AudioStates.Button:
                    PLayButtonSound();
                    break;
                case AudioKeys.AudioStates.ButtonApply:
                    PLayButtonApplySound();
                    break;
                case AudioKeys.AudioStates.ButtonCancel:
                    PLayButtonCancelSound();
                    break;
            }
        }
        private void PLayButtonSound() => _button.Play();
        private void PLayButtonApplySound() => _buttonApply.Play();
        private void PLayButtonCancelSound() => _buttonCancel.Play();
    }
}