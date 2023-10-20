using Larva.Tools;
using UnityEngine;

namespace Larva.Data
{
    [CreateAssetMenu(fileName = nameof(SaveLoadManager), menuName = "Managers/Menu/SaveLoadManager")]
    public class SaveLoadManager : ScriptableObject
    {
        [field: SerializeField] public string GameSettingsDataPath { get; private set; }
        [field: SerializeField] public string HouseDataPath { get; private set; }
        [field: SerializeField] public string LarvaDataPath { get; private set; }

        [field: SerializeField] public StructsData.GameSettingsData GameSettingsData = new StructsData.GameSettingsData();
        [field: SerializeField] public StructsData.HouseData HouseData = new StructsData.HouseData();
        [field: SerializeField] public StructsData.LarvaData LarvaData = new StructsData.LarvaData();
    }
}