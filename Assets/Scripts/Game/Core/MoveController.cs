using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Tools;
using UnityEngine;

namespace Larva.Game.Core
{
    public class MoveController : ObjectsDisposer
    {
        private LarvaManager _playerManager;
        public MoveController(){}
        public MoveController(GameManager gameManager, LarvaManager playerManager)
        {
            _playerManager = playerManager;
        }
        public virtual void Execute() { }
        public void TurnLeft()
        {
            _playerManager.MovementPlane = MovementPlane.Horizontal;
            _playerManager.Direction.Value = new Vector3(0, 90, 0);
        }
        public void TurnRight()
        {
            _playerManager.MovementPlane = MovementPlane.Horizontal;
            _playerManager.Direction.Value = new Vector3(0, -90, 0);
        }
        public void TurnUp()
        {
            _playerManager.MovementPlane = MovementPlane.Vertical;
            _playerManager.Direction.Value = new Vector3(0, 180, 0);
        }
        public void TurnDown()
        {
            _playerManager.MovementPlane = MovementPlane.Vertical;
            _playerManager.Direction.Value = new Vector3(0, 0, 0);
        }
    }
}