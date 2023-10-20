using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Larva.House.UI.View
{
    public class BedroomCanvasView : MonoBehaviour
    {
        [SerializeField] private Button _mainHallButton;
        [SerializeField] private Button _chldrenRoomButton;

        private UnityAction _goToMainHall;
        private UnityAction _goToChldrenRoom;

        public void Initialize(UnityAction goToMainHall, UnityAction goToChldrenRoom)
        {
            _goToMainHall = goToMainHall;
            _goToChldrenRoom = goToChldrenRoom;

            _mainHallButton.onClick.AddListener(_goToMainHall);
            _chldrenRoomButton.onClick.AddListener(_goToChldrenRoom);
        }
        private void OnDestroy()
        {
            _mainHallButton.onClick.RemoveListener(_goToMainHall);
            _chldrenRoomButton.onClick.RemoveListener(_goToChldrenRoom);
        }
    }
}