using UnityEngine;

namespace Larva.Game.Data
{
    [CreateAssetMenu(fileName = nameof(UIManager), menuName = "Managers/Game/UIManager")]
    public class UIManager : ScriptableObject
    {
        [field: SerializeField] public string PathForUIObjects { get; private set; }
        [field: SerializeField] public string GameCanvasPath { get; private set; }
        [field: SerializeField] public string PauseCanvasPath { get; private set; }
        [field: SerializeField] public string GameOverCanvasPath { get; private set; }
    }
}