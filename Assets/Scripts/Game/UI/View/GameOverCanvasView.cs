using Larva.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using static Larva.Tools.LocalizationTextKeys.LocalizationGameTextKeys;

namespace Larva.Game.UI.View
{
    public class GameOverCanvasView : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _restartText;
        [SerializeField] private Text _exitText;

        private UnityAction _restartVoid;
        private UnityAction _exitVoid;

        public void Initialize(UnityAction restart, UnityAction exit, int score)
        {
            _restartVoid = restart;
            _exitVoid = exit;

            _restartButton.onClick.AddListener(_restartVoid);
            _exitButton.onClick.AddListener(_exitVoid);

            TranslateText(score);
        }
        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(_restartVoid);
            _exitButton.onClick.RemoveListener(_exitVoid);
        }
        private void TranslateText(int value)
        {
            _scoreText.text = $"{_localizationManager.GameTable.Value.GetEntry(YouScore.ToString())?.GetLocalizedString()} : {value}";
            _restartText.text = _localizationManager.GameTable.Value.GetEntry(Restart.ToString())?.GetLocalizedString();
            _exitText.text = _localizationManager.GameTable.Value.GetEntry(MainMenu.ToString())?.GetLocalizedString();
        }
    }
}