using Larva.Game.Data;
using Larva.Game.Tools;
using UnityEngine;

namespace Larva.Game.Core
{
    public class InputKeyBoardController : MoveController
    {
        private GameManager _gameManager;

        public InputKeyBoardController(GameManager gameManager, LarvaManager larvaManager) : base(larvaManager)
        {
            _gameManager = gameManager;
        }

        public override void Execute()
        {
            if (Input.GetAxis("Horizontal") > 0)
                TurnRight();

            if (Input.GetAxis("Horizontal") < 0)
                TurnLeft();

            if (Input.GetAxis("Vertical") > 0)
                TurnUp();

            if (Input.GetAxis("Vertical") < 0)
                TurnDown();

            if (Input.GetKeyDown(KeyCode.Escape))
                _gameManager.GameState.Value = GameState.Pause;
        }
    }
}