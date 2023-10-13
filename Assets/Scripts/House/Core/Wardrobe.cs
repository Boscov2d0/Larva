using Larva.House.Data;
using Larva.House.Tools;
using UnityEngine;

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
            _houseManager.HouseState.SubscribeOnChange(OnStateChange);
        }
        private void OnDestroy()
        {
            _houseManager.HouseState.UnSubscribeOnChange(OnStateChange);
        }
        private void Update()
        {
            if (!_houseManager.Bedroom.IsActive)
                return;

            if (_houseManager.HouseState.Value == HouseState.Bedroom)
            {
                raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (raycastHit.collider.TryGetComponent(out Wardrobe wardrobe))
                        {
                            _houseManager.HouseState.Value = HouseState.Wardrobe;
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
            switch (_houseManager.HouseState.Value)
            {
                case HouseState.Wardrobe:
                    _isOpen = false;
                    _openDoor = true;
                    break;
                case HouseState.Bedroom:
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