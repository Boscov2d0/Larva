using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Larva.Menu.UI.View
{
    public class SettingsCanvasView : MonoBehaviour
    {
        [SerializeField] private Button _backButton;

        private UnityAction _back;

        public void Init(UnityAction back)
        {
            _back = back;

            _backButton.onClick.AddListener(_back);
        }
        private void OnDestroy()
        {
            _backButton.onClick.RemoveListener(_back);
        }
    }
}