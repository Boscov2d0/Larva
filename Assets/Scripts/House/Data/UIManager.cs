using UnityEngine;

namespace Larva.House.Data
{
    [CreateAssetMenu(fileName = nameof(UIManager), menuName = "Managers/House/UIManager")]
    public class UIManager : ScriptableObject
    {
        [field: SerializeField] public string PathForUIObjects { get; private set; }
        [field: SerializeField] public string MainHallCanvasPath { get; private set; }
        [field: SerializeField] public string BedroomCanvasPath { get; private set; }
        [field: SerializeField] public string ChildrenRoomCanvasPath { get; private set; }
        [field: SerializeField] public string KitchenCanvasPath { get; private set; }
        [field: SerializeField] public string AddRoomCanvasPath { get; private set; }
        [field: SerializeField] public string AddFamilyCanvasPath { get; private set; }
        [field: SerializeField] public string WardrobeCanvasPath { get; private set; }
    }
}