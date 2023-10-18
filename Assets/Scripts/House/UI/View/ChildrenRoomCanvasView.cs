using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Larva.House.UI.View
{
    public class ChildrenRoomCanvasView : MonoBehaviour
    {
        [SerializeField] private Button _bedroomButton;

        private UnityAction _goToBedroom;

        public void Initialize(UnityAction goToBedroom)
        {
            _goToBedroom = goToBedroom;

            _bedroomButton.onClick.AddListener(_goToBedroom);
        }
        private void OnDestroy()
        {
            _bedroomButton.onClick.RemoveListener(_goToBedroom);
        }
    }
}