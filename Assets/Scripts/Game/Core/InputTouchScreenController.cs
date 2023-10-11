using Larva.Game.Data;
using UnityEngine;

namespace Larva.Game.Core
{
    public class InputTouchScreenController : MoveController
    {
        private Touch _touch;
        private Vector2 _startTouchposition;
        private Vector2 _endTouchposition;

        public InputTouchScreenController(LarvaManager larvaManager) : base(larvaManager) { }

        public override void Execute()
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
                        TurnUp();
                    else
                        TurnDown();
                }

                if (Mathf.Abs(_touch.deltaPosition.x) > Mathf.Abs(_touch.deltaPosition.y))
                {
                    if (_endTouchposition.x > _startTouchposition.x)
                        TurnRight();
                    else
                        TurnLeft();
                }
            }
        }
    }
}