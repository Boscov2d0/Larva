using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Larva.House.UI.View
{
    public class KitchenCanvasView : MonoBehaviour
    {
        [SerializeField] private Button _mainHallButton;

        private UnityAction _goToMainHall;

        public void Initialize(UnityAction goToMainHall)
        {
            _goToMainHall = goToMainHall;

            _mainHallButton.onClick.AddListener(_goToMainHall);
        }
        private void OnDestroy()
        {
            _mainHallButton.onClick.RemoveListener(_goToMainHall);
        }
    }
}