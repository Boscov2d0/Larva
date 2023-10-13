using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Larva.House.UI.View
{
    public class MainHallCanvasView : MonoBehaviour
    {
        [SerializeField] private Button _outsideButton;
        [SerializeField] private Button _bedroomButton;
        [SerializeField] private Button _kitchenButton;

        private UnityAction _goToOutside;
        private UnityAction _goToBedroom;
        private UnityAction _goToKitchen;

        public void Initialize(UnityAction goToOutside, UnityAction goToBedroom, UnityAction goToKitchen)
        {
            _goToOutside = goToOutside;
            _goToBedroom = goToBedroom;
            _goToKitchen = goToKitchen;

            _outsideButton.onClick.AddListener(_goToOutside);
            _bedroomButton.onClick.AddListener(_goToBedroom);
            _kitchenButton.onClick.AddListener(_goToKitchen);
        }
        private void OnDestroy()
        {
            _outsideButton.onClick.RemoveListener(_goToOutside);
            _bedroomButton.onClick.RemoveListener(_goToBedroom);
            _kitchenButton.onClick.RemoveListener(_goToKitchen);
        }
    }
}