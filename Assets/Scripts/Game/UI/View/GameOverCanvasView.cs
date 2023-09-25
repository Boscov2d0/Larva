using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Larva.Game.UI.View
{
    public class GameOverCanvasView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _restartText;
        [SerializeField] private Text _exitText;

        private UnityAction _restartVoid;
        private UnityAction _exitVoid;

        public void Initialized(UnityAction restart, UnityAction exit, int score)
        {
            _restartVoid = restart;
            _exitVoid = exit;

            _restartButton.onClick.AddListener(_restartVoid);
            _exitButton.onClick.AddListener(_exitVoid);

            ShowScore(score);
        }
        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(_restartVoid);
            _exitButton.onClick.RemoveListener(_exitVoid);
        }
        private void ShowScore(int value) 
        {
            _scoreText.text = $"Your score : {value}";
        }
    }
}