using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Tools;
using UnityEngine;

namespace Larva.Game.Core
{
    public class MoveController : ObjectsDisposer
    {
        private LarvaManager _larvaManager;
        public MoveController(){}
        public MoveController(LarvaManager larvaManager)
        {
            _larvaManager = larvaManager;
        }
        public virtual void Execute() {}
        public void TurnLeft()
        {
            _larvaManager.MovementPlane = MovementPlane.Horizontal;
            _larvaManager.Direction.Value = new Vector3(0, 90, 0);
        }
        public void TurnRight()
        {
            _larvaManager.MovementPlane = MovementPlane.Horizontal;
            _larvaManager.Direction.Value = new Vector3(0, -90, 0);
        }
        public void TurnUp()
        {
            _larvaManager.MovementPlane = MovementPlane.Vertical;
            _larvaManager.Direction.Value = new Vector3(0, 180, 0);
        }
        public void TurnDown()
        {
            _larvaManager.MovementPlane = MovementPlane.Vertical;
            _larvaManager.Direction.Value = new Vector3(0, 0, 0);
        }
    }
}