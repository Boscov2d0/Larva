using Larva.Menu.Tools;
using UnityEngine;

namespace Larva.Menu.Data
{
    [CreateAssetMenu(fileName = nameof(SaveLoadManager), menuName = "Managers/Menu/SaveLoadManager")]
    public class SaveLoadManager : ScriptableObject
    {
        [field: SerializeField] public string GameSettingsDataPath { get; private set; }

        [field: SerializeField] public StructsData.GameSettingsData GameSettingsData = new StructsData.GameSettingsData();
    }
}