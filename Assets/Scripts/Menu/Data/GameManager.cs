using Larva.Core;
using Larva.Menu.Tools;
using Larva.Tools;
using UnityEngine;

using static Larva.Tools.Keys;

namespace Larva.Menu.Data
{
    [CreateAssetMenu(fileName = nameof(GameManager), menuName = "Managers/Menu/GameManager")]
    public class GameManager : ScriptableObject
    {
        [field: SerializeField] public string PathForObjects { get; private set; }
        [field: SerializeField] public string ScenePath { get; private set; }
        [field: SerializeField] public string DayDirectionalLightPath { get; private set; }
        [field: SerializeField] public string EveningDirectionalLightPath { get; private set; }
        [field: SerializeField] public string NightDirectionalLightPath { get; private set; }
        [field: SerializeField] public string MenuCameraPath { get; private set; }
        [field: SerializeField] public string HouseCameraPath { get; private set; }
        [field: SerializeField] public string MenuLarvaPath { get; private set; }
        [field: SerializeField] public string AudioControllerPath { get; private set; }
        [field: SerializeField] public string LarvaHousePath { get; private set; }

        [field: SerializeField] public SubscriptionProperty<GameState> GameState = new SubscriptionProperty<GameState>();
        [HideInInspector] public LarvaView MenuLarva { get; set; }
        [HideInInspector] public SubscriptionProperty<DayTime> DayTime = new SubscriptionProperty<DayTime>();
    }
}