using Larva.House.Tools;
using UnityEngine;

namespace Larva.House.Data
{
    [CreateAssetMenu(fileName = nameof(RoomManager), menuName = "Managers/House/RoomManager")]
    public class RoomManager : ScriptableObject
    {
        [SerializeField] public HouseState.RoomState RoomName;
        [SerializeField] public bool IsActive;
        [SerializeField] public int Cost;
    }
}