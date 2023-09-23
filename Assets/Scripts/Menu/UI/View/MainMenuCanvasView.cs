using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Larva.Menu.UI.View
{
    public class MainMenuCanvasView : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitGameButton;

        private UnityAction _startGame;
        private UnityAction _openSettings;
        private UnityAction _exitGame;

        public void Init(UnityAction startGame, UnityAction openSettings, UnityAction exitGame)
        {
            _startGame = startGame;
            _openSettings = openSettings;
            _exitGame = exitGame;

            _startGameButton.onClick.AddListener(_startGame);
            _settingsButton.onClick.AddListener(_openSettings);
            _exitGameButton.onClick.AddListener(_exitGame);
        }
        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(_startGame);
            _settingsButton.onClick.RemoveListener(_openSettings);
            _exitGameButton.onClick.RemoveListener(_exitGame);
        }
    }
}