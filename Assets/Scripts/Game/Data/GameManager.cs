using Larva.Game.Tools;
using Larva.Tools;
using UnityEngine;

namespace Larva.Game.Data
{
    [CreateAssetMenu(fileName = nameof(GameManager), menuName = "Managers/Game/GameManager")]
    public class GameManager : ScriptableObject
    {
        [field: SerializeField] public bool PlayHellowAnimation { get; set; }
        [field: SerializeField] public Vector3 StartPosition { get; private set; }
        [field: SerializeField] public int CountOfGoodFood { get; private set; }
        [field: SerializeField] public int CountOfBadFood { get; private set; }
        [field: SerializeField] public int CountOfObstacles { get; private set; }
        [field: SerializeField] public int IncreasePointsValue { get; private set; }
        [TextArea(1, 1)]
        public string Discription1;
        [field: SerializeField] public string PathForObjects { get; private set; }
        [field: SerializeField] public string LoadingPath { get; private set; }
        [field: SerializeField] public string GameAreaPath { get; private set; }
        [field: SerializeField] public string DayDirectionalLightPath { get; private set; }
        [field: SerializeField] public string EveningDirectionalLightPath { get; private set; }
        [field: SerializeField] public string NightDirectionalLightPath { get; private set; }
        [field: SerializeField] public string CameraPath { get; private set; }
        [field: SerializeField] public string AudioControllerPath { get; private set; }

        public SubscriptionProperty<States.GameState> GameState = new SubscriptionProperty<States.GameState>();
        public SubscriptionProperty<int> CurrentCountOfGoodFood = new SubscriptionProperty<int>();
        public SubscriptionProperty<int> Score = new SubscriptionProperty<int>();
    }
}