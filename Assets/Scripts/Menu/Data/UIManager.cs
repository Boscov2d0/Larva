using UnityEngine;

namespace Larva.Menu.Data
{
    [CreateAssetMenu(fileName = nameof(UIManager), menuName = "Managers/Menu/UIManager")]
    public class UIManager : ScriptableObject
    {
        [field: SerializeField] public string PathForUIObjects { get; private set; }
        [field: SerializeField] public string MainMenuCanvasPath { get; private set; }
        [field: SerializeField] public string SettingsCanvasPath { get; private set; }
    }
}