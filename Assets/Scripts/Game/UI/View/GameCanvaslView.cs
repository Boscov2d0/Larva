using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Larva.Game.UI.View
{
    public class GameCanvaslView : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private Button _pauseButton;

        private UnityAction _pauseVoid;

        public void Initialize(UnityAction pause)
        {
            _pauseVoid = pause;
            _pauseButton.onClick.AddListener(_pauseVoid);
        }
        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveListener(_pauseVoid);
        }
        public void OnChangeScoreText(int value) 
        {
            _scoreText.text = value.ToString();
        }
    }
}