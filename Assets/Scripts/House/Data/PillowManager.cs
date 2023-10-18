using UnityEngine;

namespace Larva.House.Data
{
    [CreateAssetMenu(fileName = nameof(PillowManager), menuName = "Managers/House/PillowManager")]
    public class PillowManager : ScriptableObject
    {
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public bool IsActive = false;
    }
}