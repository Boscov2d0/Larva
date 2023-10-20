using Larva.Data;
using UnityEngine;
using System.Collections.Generic;

using static Larva.Tools.AudioKeys;

namespace Larva.Menu.Core
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private AudioSource _button;
        [SerializeField] private AudioSource _buttonApply;
        [SerializeField] private AudioSource _buttonCancel;
        [SerializeField] private List<AudioSource> _musics;

        private int _index;

        private void Start()
        {
            if (_audioManager.SoundsVolume == 0)
                _audioManager.SoundsVolume = 0.0001f;
            if (_audioManager.MusicVolume == 0)
                _audioManager.MusicVolume = 0.0001f;

            _audioManager.AudioMixer.SetFloat(MixerGroups.Sound.ToString(), Mathf.Log10(_audioManager.SoundsVolume) * 20);
            _audioManager.AudioMixer.SetFloat(MixerGroups.Music.ToString(), Mathf.Log10(_audioManager.MusicVolume) * 20);

            _index = Random.Range(0, _musics.Count);
            _musics[_index].Play();

            _audioManager.State.SubscribeOnChange(OnStateCahnge);
        }
        private void OnDestroy()
        {
            _audioManager.State.UnSubscribeOnChange(OnStateCahnge);
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
        private void OnStateCahnge()
        {
            switch (_audioManager.State.Value)
            {
                case AudioStates.Button:
                    PLayButtonSound();
                    break;
                case AudioStates.ButtonApply:
                    PLayButtonApplySound();
                    break;
                case AudioStates.ButtonCancel:
                    PLayButtonCancelSound();
                    break;
            }
        }
        private void PLayButtonSound() => _button.Play();
        private void PLayButtonApplySound() => _buttonApply.Play();
        private void PLayButtonCancelSound() => _buttonCancel.Play();
    }
}