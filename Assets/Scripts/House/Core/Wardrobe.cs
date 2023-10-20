using Larva.House.Data;
using UnityEngine;

using static Larva.House.Tools.HouseState;

namespace Larva.House.Core
{
    public class Wardrobe : MonoBehaviour
    {
        [SerializeField] private HouseManager _houseManager;
        [SerializeField] private Transform _door;
        [SerializeField] private float _doorSpeed;
        [SerializeField] private float _openDoorPoint;
        [SerializeField] private float _closeDoorPoint;

        private bool _openDoor;
        private bool _isOpen;
        private Ray raycast;
        private RaycastHit raycastHit;

        private void Start()
        {
            _houseManager.ActionState.SubscribeOnChange(OnStateChange);
        }
        private void OnDestroy()
        {
            _houseManager.ActionState.UnSubscribeOnChange(OnStateChange);
        }
        private void Update()
        {
            if (!_houseManager.Bedroom.IsActive || _houseManager.ActionState.Value == ActionState.OpenWardrobe)
                return;

            if (_houseManager.RoomState.Value == RoomState.Bedroom)
            {
                raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (raycastHit.collider.TryGetComponent(out Wardrobe wardrobe))
                        {
                            _houseManager.ActionState.Value = ActionState.OpenWardrobe;
                        }
                    }
                }
            }
        }
        private void FixedUpdate()
        {
            if (_openDoor)
                CheckDoor();
        }
        private void OnStateChange()
        {
            switch (_houseManager.ActionState.Value)
            {
                case ActionState.OpenWardrobe:
                    _isOpen = false;
                    _openDoor = true;
                    break;
                case ActionState.CloseWardrobe:
                    _isOpen = true;
                    _openDoor = true;
                    break;
            }
        }
        public void CheckDoor()
        {
            if (_isOpen)
                CloseWardrobe();
            else
                OpenWardrobe();
        }
        public void CloseWardrobe()
        {
            if (_door.localRotation.eulerAngles.y < _openDoorPoint)
                _door.Rotate(new Vector3(0, 1, 0), _doorSpeed * Time.fixedDeltaTime);
            else
            {
                _openDoor = false;
                _isOpen = !_isOpen;
            }
        }
        private void OpenWardrobe()
        {
            if (_door.localEulerAngles.y > _closeDoorPoint)
                _door.Rotate(new Vector3(0, -1, 0), _doorSpeed * Time.fixedDeltaTime);
            else
            {
                _openDoor = false;
                _isOpen = !_isOpen;
            }
        }
    }
}