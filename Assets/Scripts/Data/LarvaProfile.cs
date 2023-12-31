using Larva.Tools;
using UnityEngine;

using static Larva.Tools.Keys;

namespace Larva.Data
{
    [CreateAssetMenu(fileName = nameof(LarvaProfile), menuName = "Managers/Profiles/LarvaProfile")]
    public class LarvaProfile : ScriptableObject
    {
        [HideInInspector] public SubscriptionProperty<int> Food = new SubscriptionProperty<int>();
        [field: SerializeField] public Material HeadSkin { get; set; }
        [field: SerializeField] public Material BodySkin { get; set; }
        [HideInInspector] public DayTime DayTime { get; set; }
    }
}