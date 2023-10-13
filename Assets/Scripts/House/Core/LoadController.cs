using Larva.Data;
using Larva.House.Data;
using Larva.Tools;
using UnityEngine;

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
        }
    }
}