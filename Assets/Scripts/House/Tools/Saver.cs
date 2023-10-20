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
            saveLoadManager.HouseData.PillowsIsActive.Clear();
            saveLoadManager.HouseData.Childrens.Clear();

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

            saveLoadManager.HouseData.Partner.IsNew = houseManager.PartnerProfile.IsNew;
            saveLoadManager.HouseData.Partner.IsHungry = houseManager.PartnerProfile.IsHungry;
            saveLoadManager.HouseData.Partner.LarvaData.HeadSkin = houseManager.PartnerProfile.HeadMaterial;
            saveLoadManager.HouseData.Partner.LarvaData.BodySkin = houseManager.PartnerProfile.BodyMaterial;

            for (int i = 0; i < houseManager.PillowManagers.Count; i++)
                saveLoadManager.HouseData.PillowsIsActive.Add(houseManager.PillowManagers[i].IsActive);
            
            for (int i = 0; i < houseManager.ChildrensProfile.Count; i++)
            {
                StructsData.FamilyData children = new StructsData.FamilyData();
                children.IsNew = houseManager.ChildrensProfile[i].IsNew;
                children.ID = houseManager.ChildrensProfile[i].ID;
                children.IsHungry = houseManager.ChildrensProfile[i].IsHungry;
                children.LarvaData.HeadSkin = houseManager.ChildrensProfile[i].HeadMaterial;
                children.LarvaData.BodySkin = houseManager.ChildrensProfile[i].BodyMaterial;
                saveLoadManager.HouseData.Childrens.Add(children);
            }

            saveLoadManager.HouseData.GameMode = houseManager.GameMode;
            saveLoadManager.HouseData.DayForGiveFood = houseManager.DayForGiveFood;

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