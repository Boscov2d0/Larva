using UnityEngine;

namespace Larva.House.Data
{
    [CreateAssetMenu(fileName = nameof(PotManager), menuName = "Managers/Menu/PotManager")]
    public class PotManager : ScriptableObject
    {
        [field: SerializeField] public int BodyIndex;
        [field: SerializeField] public Material Material;
        [field: SerializeField] public bool IsActive;
        [field: SerializeField] public int Capacity;
    }
}