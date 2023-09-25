using Larva.Menu.Tools;
using Larva.Tools;
using UnityEngine;

namespace Larva.Menu.Data
{
    [CreateAssetMenu(fileName = nameof(GameManager), menuName = "Managers/Menu/GameManager")]
    public class GameManager : ScriptableObject
    {
        [field: SerializeField] public string PathForObjects { get; private set; }
        [field: SerializeField] public string ScenePath { get; private set; }
        [field: SerializeField] public string DirectionalLight { get; private set; }
        [field: SerializeField] public string MenuCameraPath { get; private set; }
        [field: SerializeField] public string MenuLarvaPath { get; private set; }

        [field: SerializeField] public SubscriptionProperty<GameState> GameState = new SubscriptionProperty<GameState>();
    }
}