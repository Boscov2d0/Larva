using Larva.Game.Tools;
using Larva.Tools;
using UnityEngine;

namespace Larva.Game.Data
{
    [CreateAssetMenu(fileName = nameof(GameManager), menuName = "Managers/Game/GameManager")]
    public class GameManager : ScriptableObject
    {
        [TextArea(1, 1)]
        public string Discription1;
        [field: SerializeField] public string PathForObjects { get; private set; }
        [field: SerializeField] public string GameAreaPath { get; private set; }
        [field: SerializeField] public string DirectionalLight { get; private set; }
        [field: SerializeField] public string CameraPath { get; private set; }

        public SubscriptionProperty<GameState> GameState = new SubscriptionProperty<GameState>();

        public SubscriptionProperty<int> Score = new SubscriptionProperty<int>();
    }
}