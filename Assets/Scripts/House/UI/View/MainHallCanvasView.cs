using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Larva.House.UI.View
{
    public class MainHallCanvasView : MonoBehaviour
    {
        [SerializeField] private Button _outsideButton;

        private UnityAction _goToOutside;

        public void Initialize(UnityAction goToOutside)
        {
            _goToOutside = goToOutside;

            _outsideButton.onClick.AddListener(_goToOutside);
        }
        private void OnDestroy()
        {
            _outsideButton.onClick.RemoveListener(_goToOutside);
        }
    }
}