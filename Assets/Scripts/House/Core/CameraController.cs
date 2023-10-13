using UnityEngine;
using Larva.Tools;
using Larva.House.Data;
using Larva.House.Tools;

namespace Larva.House.Core
{
    public class CameraController : ObjectsDisposer
    {
        private readonly HouseManager _houseManager;

        private Camera _camera;
        private Vector3 _finishPosition;
        private bool _move;

        public CameraController(HouseManager houseManager)
        {
            
            _houseManager = houseManager;

            _camera = Camera.main;

            _houseManager.HouseState.SubscribeOnChange(OnStateChange);

            //_houseManager.MenuLarva.transform.SetParent(_camera.transform);

            OnStateChange();
        }
        protected override void OnDispose()
        {
            _houseManager.HouseState.UnSubscribeOnChange(OnStateChange);

            base.OnDispose();
        }
        public void FixedExecute()
        {
            if (_move)
                MoveCamera();
        }
        private void OnStateChange() 
        {
            switch (_houseManager.HouseState.Value) 
            {
                case HouseState.OutSideMenu:
                    SetCameraPositionsPoint(_houseManager.OutSideCameraPosition);
                    break;
                case HouseState.MainHall:
                    SetCameraPositionsPoint(_houseManager.MainHallCameraPosition);
                    break;
            }
        }
        private void SetCameraPositionsPoint(Vector3 finishPosition)
        {
            _finishPosition = finishPosition;
            _move = true;
        }
        private void MoveCamera()
        {
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _finishPosition, _houseManager.CameraMoveSpeed * Time.fixedDeltaTime);

            if (_camera.transform.position.y == _finishPosition.y && _camera.transform.position.x == _finishPosition.x)
                _move = false;
        }
    }
}