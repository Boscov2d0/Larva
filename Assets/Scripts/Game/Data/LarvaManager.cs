using Larva.Game.Tools;
using Larva.Tools;
using UnityEngine;

namespace Larva.Game.Data
{
    [CreateAssetMenu(fileName = nameof(LarvaManager), menuName = "Managers/Game/LarvaManager")]
    public class LarvaManager : ScriptableObject
    {
        [field: SerializeField] public string ObjectsPath { get; private set; }
        [field: SerializeField] public string LarvaPath { get; private set; }
        [field: SerializeField] public string BodyPath { get; private set; }
        [field: SerializeField] public string StarsPath { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float BodyNodeDistance { get; private set; }

        [HideInInspector] public MovementPlane MovementPlane;
        [HideInInspector] public SubscriptionProperty<Vector3> Direction = new SubscriptionProperty<Vector3>();
        [HideInInspector] public SubscriptionProperty<States.LarvaState> State = new SubscriptionProperty<States.LarvaState>();
        [HideInInspector] public Material BodySkin { get; set; }
    }
}