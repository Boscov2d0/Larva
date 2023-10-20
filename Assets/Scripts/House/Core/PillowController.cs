using Larva.Tools;
using Larva.House.Data;
using UnityEngine;

using static Larva.House.Tools.HouseState;

namespace Larva.House.Core
{
    public class PillowController : ObjectsDisposer
    {
        private readonly HouseManager _houseManager;

        private Ray raycast;
        private RaycastHit raycastHit;
        private Pillow _pillow;
        private int _countOfChildren;

        public PillowController(HouseManager houseManager)
        {
            _houseManager = houseManager;
        }
        public void Execute() 
        {
            if (_houseManager.RoomState.Value != RoomState.ChildrenRoom)
                return;

            if (!_houseManager.AddChild)
            {
                raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (raycastHit.collider.TryGetComponent(out Pillow pillow))
                        {
                            _countOfChildren = _houseManager.CountOfChildren;
                            _pillow = pillow;
                            _pillow.AddMember();
                        }
                    }
                }
            }

            if (_pillow != null)
                _pillow?.SetState();

            if (_countOfChildren == _houseManager.CountOfChildren && !_houseManager.AddChild)
                _pillow = null;
        }
    }
}