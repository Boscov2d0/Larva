using UnityEngine;

namespace Larva.House.Data
{
    [CreateAssetMenu(fileName = nameof(UIManager), menuName = "Managers/House/UIManager")]
    public class UIManager : ScriptableObject
    {
        [field: SerializeField] public string PathForUIObjects { get; private set; }
        [field: SerializeField] public string MainHallCanvasPath { get; private set; }
    }
}