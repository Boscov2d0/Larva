using UnityEngine;

namespace Larva.House.Tools
{
    [CreateAssetMenu(fileName = nameof(ChildrensPlaces), menuName = "Managers/House/ChildrensPlaces")]
    public class ChildrensPlaces : ScriptableObject
    {
        [field: SerializeField] public bool IsHave;
        [field: SerializeField] public Vector3 Position;
    }
}