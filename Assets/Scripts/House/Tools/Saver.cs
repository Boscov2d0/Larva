using Larva.Data;
using Larva.House.Data;
using Larva.Tools;

namespace Larva.House.Tools
{
    public static class Saver
    {
        public static void SaveHouseData(SaveLoadManager saveLoadManager, HouseManager houseManager)
        {
            saveLoadManager.HouseData.CountOfFood = houseManager.CountOfFood.Value;

            saveLoadManager.HouseData.Pots.Clear();

            for (int i = 0; i < houseManager.PotManagers.Count; i++)
            {
                if (!houseManager.PotManagers[i].IsActive)
                    break;

                StructsData.PotData pot = new StructsData.PotData();
                pot.BodyIndex = houseManager.PotManagers[i].BodyIndex;
                pot.Material = houseManager.PotManagers[i].Material;
                pot.IsActive = houseManager.PotManagers[i].IsActive;
                pot.Capacity = houseManager.PotManagers[i].Capacity;
                saveLoadManager.HouseData.Pots.Add(pot);
            }
            JSONDataLoadSaver<StructsData.HouseData>.SaveData(saveLoadManager.HouseData, saveLoadManager.HouseDataPath);
        }
    }
}