using UnityEngine;

namespace Larva.Menu.Data
{
    [CreateAssetMenu(fileName = nameof(UIManager), menuName = "Managers/Menu/UIManager")]
    public class UIManager : ScriptableObject
    {
        [field: SerializeField] public string PathForUIObjects { get; private set; }
        [field: SerializeField] public string MainMenuCanvasPath { get; private set; }
        [field: SerializeField] public string PCSettingsCanvasPath { get; private set; }
        [field: SerializeField] public string PhoneSettingsCanvasPath { get; private set; }
        [field: SerializeField] public string GameInstructionCanvasPath { get; private set; }
}
}