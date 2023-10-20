using Larva.House.Data;
using Larva.Tools;
using UnityEngine;

using static Larva.House.Tools.HouseState;

namespace Larva.House.Core
{
    public class TouchScreenMoveCameraController : ObjectsDisposer
    {
        private readonly HouseManager _houseManager;
        private Touch _touch;
        private Vector2 _startTouchposition;
        private Vector2 _endTouchposition;

        public TouchScreenMoveCameraController(HouseManager houseManager) 
        {
            _houseManager = houseManager;
        }

        public void Execute()
        {
            if (Input.touchCount > 0)
                _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began)
                _startTouchposition = _touch.position;

            if (Input.touchCount > 0 && _touch.phase == TouchPhase.Ended)
            {
                _endTouchposition = _touch.position;

                if (Mathf.Abs(_touch.deltaPosition.y) > Mathf.Abs(_touch.deltaPosition.x))
                {
                    if (_endTouchposition.y > _startTouchposition.y)
                        MoveUp();
                }

                if (Mathf.Abs(_touch.deltaPosition.x) > Mathf.Abs(_touch.deltaPosition.y))
                {
                    if (_endTouchposition.x > _startTouchposition.x)
                        MoveRigth();
                    else
                        MoveLeft();
                }
            }
        }
        private void MoveUp()
        {
            if (_houseManager.RoomState.Value == RoomState.MainHall)
                _houseManager.RoomState.Value = RoomState.OutSideMenu;
        }
        private void MoveLeft()
        {
            switch (_houseManager.RoomState.Value)
            {
                case RoomState.MainHall:
                    _houseManager.RoomState.Value = RoomState.Kitchen;
                    break;
                case RoomState.Bedroom:
                    _houseManager.RoomState.Value = RoomState.MainHall;
                    break;
                case RoomState.ChildrenRoom:
                    _houseManager.RoomState.Value = RoomState.Bedroom;
                    break;
            }
        }
        private void MoveRigth()
        {
            switch (_houseManager.RoomState.Value)
            {
                case RoomState.Kitchen:
                    _houseManager.RoomState.Value = RoomState.MainHall;
                    break;
                case RoomState.MainHall:
                    _houseManager.RoomState.Value = RoomState.Bedroom;
                    break;
                case RoomState.Bedroom:
                    _houseManager.RoomState.Value = RoomState.ChildrenRoom;
                    break;
            }
        }
    }
}