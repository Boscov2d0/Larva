using Larva.Data;
using Larva.House.Data;
using Larva.Tools;
using UnityEngine;

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

            saveLoadManager.HouseData.BedroomIsActive = houseManager.Bedroom.IsActive;
            saveLoadManager.HouseData.ChildrenRoomIsActive = houseManager.ChildrenRoom.IsActive;
            saveLoadManager.HouseData.KitcheIsActive = houseManager.Kitchen.IsActive;

            saveLoadManager.HouseData.HavePartner = houseManager.HavePartner;
            saveLoadManager.HouseData.HaveChild = houseManager.HaveChild;
            saveLoadManager.HouseData.CountOfChildren = houseManager.CountOfChildren;
            saveLoadManager.HouseData.GameMode = houseManager.GameMode;
            saveLoadManager.HouseData.DayForGiveFood = houseManager.DayForGiveFood;
            saveLoadManager.HouseData.Partner.IsNew = houseManager.PartnerProfile.IsNew;
            saveLoadManager.HouseData.Partner.IsHungry = houseManager.PartnerProfile.IsHungry;

            for (int i = 0; i < saveLoadManager.HouseData.PillowsIsActive.Count; i++)
            {
                saveLoadManager.HouseData.PillowsIsActive[i] = houseManager.PillowManagers[i].IsActive;
            }
            for (int i = 0; i < saveLoadManager.HouseData.Childrens.Count; i++)
            {
                StructsData.FamilyData children = new StructsData.FamilyData();
                children.IsNew = houseManager.ChildrensProfile[i].IsNew;
                children.ID = houseManager.ChildrensProfile[i].ID;
                children.IsHungry = houseManager.ChildrensProfile[i].IsHungry;
                saveLoadManager.HouseData.Childrens[i] = children;
            }

            JSONDataLoadSaver<StructsData.HouseData>.SaveData(saveLoadManager.HouseData, saveLoadManager.HouseDataPath);
        }
        public static void SaveLarvaData(SaveLoadManager saveLoadManager, LarvaProfile larvaProfile)
        {
            saveLoadManager.LarvaData.HeadSkin = larvaProfile.HeadSkin;
            saveLoadManager.LarvaData.BodySkin = larvaProfile.BodySkin;

            JSONDataLoadSaver<StructsData.LarvaData>.SaveData(saveLoadManager.LarvaData, saveLoadManager.LarvaDataPath);
        }
    }
}