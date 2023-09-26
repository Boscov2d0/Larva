using Larva.Game.Data;
using Larva.Game.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Larva.Game.Core
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private List<AudioSource> _musics;
        [SerializeField] private AudioSource _gameOverSound;

        private int _index;

        private void Start()
        {
            _index = Random.Range(0, _musics.Count);
            _musics[_index].Play();

            _gameManager.GameState.SubscribeOnChange(OnStateChange);
        }
        private void OnDestroy()
        {
            _gameManager.GameState.UnSubscribeOnChange(OnStateChange);
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
        private void OnStateChange()
        {
            if (_gameManager.GameState.Value == GameState.Lose)
            {
                _gameOverSound.Play();
            }
        }
    }
}