using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Larva.Game.UI.View
{
    public class PauseCanvasView : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private Text _continueText;
        [SerializeField] private Text _exitText;

        private UnityAction _continueVoid;
        private UnityAction _exitVoid;

        public void Initialized(UnityAction continueVoid, UnityAction exit)
        {
            _continueVoid = continueVoid;
            _exitVoid = exit;

            _continueButton.onClick.AddListener(_continueVoid);
            _exitButton.onClick.AddListener(_exitVoid);
        }
        private void OnDestroy()
        {
            _continueButton.onClick.RemoveListener(_continueVoid);
            _exitButton.onClick.RemoveListener(_exitVoid);
        }
    }
}