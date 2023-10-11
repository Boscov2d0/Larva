using Larva.Game.Data;
using Larva.Game.Tools;
using UnityEngine;

namespace Larva.Game.Core
{
    public class AnimationEvent : StateMachineBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        public void OnStateEnter()
        {
            _gameManager.GameState.Value = GameState.Start;
        }
    }
}