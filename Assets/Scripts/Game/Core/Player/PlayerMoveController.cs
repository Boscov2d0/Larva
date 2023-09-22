using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Larva.Game.Core.Player
{
    public class PlayerMoveController : ObjectsDisposer
    {
        private Transform _transform;
        private LarvaManager _playerManager;
        private List<Transform> _bodyNodeList;
        private float _speed;
        private float _bodyNodeDistance;
        private float _sqrDistance;
        private Vector3 _previousPosition;
        private Vector3 _temp;
        private Vector3 _topScreenBorder;
        private Vector3 _bottomScreenBorder;
        private Vector3 _leftScreenBorder;
        private Vector3 _rightScreenBorder;
        private MovementPlane _currentMovementPlane;

        public PlayerMoveController(Transform transform, List<Transform> bodyNodeList, LarvaManager playerManager)
        {
            _transform = transform;
            _bodyNodeList = bodyNodeList;
            _playerManager = playerManager;
            _speed = playerManager.Speed;
            _bodyNodeDistance = playerManager.BodyNodeDistance;
            _playerManager.Direction.SubscribeOnChange(OnChangeDirection);

            _currentMovementPlane = MovementPlane.Vertical;

            SetScreenBorders();
        }
        protected override void OnDispose()
        {
            _playerManager.Direction.UnSubscribeOnChange(OnChangeDirection);
            _transform = null;
            _bodyNodeList.Clear();

            base.OnDispose();
        }
        private void SetScreenBorders()
        {
            Camera camera = Camera.main;

            _topScreenBorder = camera.ScreenToWorldPoint(new Vector3((camera.pixelWidth / 2), camera.pixelHeight + 4, 0));
            _bottomScreenBorder = camera.ScreenToWorldPoint(new Vector3((camera.pixelWidth / 2), -4, 0));
            _leftScreenBorder = camera.ScreenToWorldPoint(new Vector3(-4, (camera.pixelHeight / 2), 0));
            _rightScreenBorder = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth + 4, (camera.pixelHeight / 2), 0));
        }
        public void FixedExecute()
        {
            if (!_transform)
                return;

            Move(_transform.position + _transform.forward * _speed);
            BordersTeleport();
        }
        private void Move(Vector3 newPosition)
        {
            _sqrDistance = _bodyNodeDistance * _bodyNodeDistance;
            _previousPosition = _transform.position;

            foreach (Transform bone in _bodyNodeList)
            {
                if ((bone.position - _previousPosition).sqrMagnitude > _sqrDistance)
                {
                    _temp = bone.position;
                    bone.position = _previousPosition;
                    _previousPosition = _temp;
                }
                else
                {
                    break;
                }
            }

            _transform.position = newPosition;
        }
        private void OnChangeDirection()
        {
            if (_currentMovementPlane != _playerManager.MovementPlane)
            {
                _transform.localEulerAngles = _playerManager.Direction.Value;
                _currentMovementPlane = _playerManager.MovementPlane;
            }
        }
        private void BordersTeleport()
        {
            if (_transform.position.x > _rightScreenBorder.x)
            {
                _transform.position = new Vector3(_leftScreenBorder.x, 0, _transform.position.z);
            }
            if (_transform.position.x < _leftScreenBorder.x)
            {
                _transform.position = new Vector3(_rightScreenBorder.x, 0, _transform.position.z);
            }
            if (_transform.position.z > _topScreenBorder.z)
            {
                _transform.position = new Vector3(_transform.position.x, 0, _bottomScreenBorder.z);
            }
            if (_transform.position.z < _bottomScreenBorder.z)
            {
                _transform.position = new Vector3(_transform.position.x, 0, _topScreenBorder.z);
            }
        }
    }
}