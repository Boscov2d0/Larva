using Larva.Data;
using Larva.House.Data;
using Larva.Tools;

namespace Larva.House.Core
{
    public class LoadController : ObjectsDisposer
    {
        private readonly SaveLoadManager _saveLoadManager;
        private readonly HouseManager _houseManager;

        public LoadController(SaveLoadManager saveLoadManager, HouseManager houseManager)
        {
            _saveLoadManager = saveLoadManager;
            _houseManager = houseManager;

            LoadHouseData();
        }

        private void LoadHouseData()
        {
            _saveLoadManager.HouseData = JSONDataLoadSaver<StructsData.HouseData>.Load(_saveLoadManager.HouseDataPath);

            _houseManager.CountOfFood.Value = _saveLoadManager.HouseData.CountOfFood;

            for (int i = 0; i < _saveLoadManager.HouseData.Pots.Count; i++)
            {
                if (!_saveLoadManager.HouseData.Pots[i].IsActive)
                    return;

                _houseManager.PotManagers[i].BodyIndex = _saveLoadManager.HouseData.Pots[i].BodyIndex;
                _houseManager.PotManagers[i].Material = _saveLoadManager.HouseData.Pots[i].Material;
                _houseManager.PotManagers[i].IsActive = _saveLoadManager.HouseData.Pots[i].IsActive;
                _houseManager.PotManagers[i].Capacity = _saveLoadManager.HouseData.Pots[i].Capacity;
            }

            _houseManager.Bedroom.IsActive = _saveLoadManager.HouseData.BedroomIsActive;
            _houseManager.ChildrenRoom.IsActive = _saveLoadManager.HouseData.ChildrenRoomIsActive;
            _houseManager.Kitchen.IsActive = _saveLoadManager.HouseData.KitcheIsActive;

            _houseManager.HavePartner = _saveLoadManager.HouseData.HavePartner;
            _houseManager.HaveChild = _saveLoadManager.HouseData.HaveChild;
            _houseManager.CountOfChildren = _saveLoadManager.HouseData.CountOfChildren;

            _houseManager.GameMode = _saveLoadManager.HouseData.GameMode;
            _houseManager.DayForGiveFood = _saveLoadManager.HouseData.DayForGiveFood;
            _houseManager.PartnerProfile.IsNew = _saveLoadManager.HouseData.Partner.IsNew;
            _houseManager.PartnerProfile.IsHungry = _saveLoadManager.HouseData.Partner.IsHungry;

            if (_saveLoadManager.HouseData.PillowsIsActive.Count == 0)
                return;

            for (int i = 0; i < _houseManager.PillowManagers.Count; i++)
            {
                _houseManager.PillowManagers[i].IsActive = _saveLoadManager.HouseData.PillowsIsActive[i];
            }

            if (_saveLoadManager.HouseData.Childrens.Count == 0)
                return;

            for (int i = 0; i < _houseManager.ChildrensProfile.Count; i++)
            {
                _houseManager.ChildrensProfile[i].IsNew = _saveLoadManager.HouseData.Childrens[i].IsNew;
                _houseManager.ChildrensProfile[i].ID = _saveLoadManager.HouseData.Childrens[i].ID;
                _houseManager.ChildrensProfile[i].IsHungry = _saveLoadManager.HouseData.Childrens[i].IsHungry;
            }
        }
    }
}