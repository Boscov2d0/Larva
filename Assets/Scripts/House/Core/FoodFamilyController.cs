using Larva.House.Data;
using Larva.House.Tools;
using Larva.Tools;
using System;

using static Larva.Tools.Keys;

namespace Larva.House.Core
{
    public class FoodFamilyController : ObjectsDisposer
    {
        private readonly HouseManager _houseManager;

        public FoodFamilyController(HouseManager houseManager)
        {
            _houseManager = houseManager;

            _houseManager.RoomState.SubscribeOnChange(OnStateChange);

            _houseManager.ConsumedFood = 0;
        }
        public void Initialize()
        {
            CheckFFStatuce();
            FeedFamily();
        }
        protected override void OnDispose()
        {
            _houseManager.RoomState.UnSubscribeOnChange(OnStateChange);

            base.OnDispose();
        }
        private void OnStateChange()
        {
            if (_houseManager.RoomState.Value == HouseState.RoomState.Kitchen)
                CalculateConsumedFood();
        }
        private void CalculateConsumedFood()
        {
            _houseManager.ConsumedFood = 0;

            if (_houseManager.HavePartner)
                _houseManager.ConsumedFood += _houseManager.PartnerProfile.FoodConsumption;

            for (int i = 0; i < _houseManager.CountOfChildren; i++)
            {
                _houseManager.ConsumedFood += _houseManager.ChildrensProfile[i].FoodConsumption;
            }
        }
        private void FeedFamily()
        {
            if (DateTime.UtcNow.DayOfWeek != _houseManager.DayForGiveFood)
                return;

            if (_houseManager.PartnerProfile.IsHungry)
            {
                if (_houseManager.CountOfFood.Value >= _houseManager.PartnerProfile.FoodConsumption)
                {
                    _houseManager.CountOfFood.Value -= _houseManager.PartnerProfile.FoodConsumption;
                    _houseManager.PartnerProfile.IsHungry = false;
                }
            }

            for (int i = 0; i < _houseManager.CountOfChildren; i++)
            {
                if (_houseManager.ChildrensProfile[i].IsHungry)
                {
                    if (_houseManager.CountOfFood.Value >= _houseManager.ChildrensProfile[i].FoodConsumption)
                    {
                        _houseManager.CountOfFood.Value -= _houseManager.ChildrensProfile[i].FoodConsumption;
                        _houseManager.ChildrensProfile[i].IsHungry = false;
                    }
                }
            }

            _houseManager.SaveLoadState.Value = SaveState.SaveHouseData;
        }
        private void CheckFFStatuce()
        {
            if (DateTime.UtcNow.DayOfWeek == _houseManager.DayForGiveFood - 1)
            {
                _houseManager.PartnerProfile.IsHungry = true;

                for (int i = 0; i < _houseManager.CountOfChildren; i++)
                {
                    _houseManager.ChildrensProfile[i].IsHungry = true;
                }
            }
            if (DateTime.UtcNow.DayOfWeek == _houseManager.DayForGiveFood + 1)
            {

                for (int i = 0; i < _houseManager.CountOfChildren; i++)
                {
                    if (_houseManager.ChildrensProfile[i].IsHungry)
                    {
                        int index = _houseManager.ChildrensProfile[i].ID;
                        _houseManager.PillowManagers[index].IsActive = false;
                        _houseManager.ChildrensProfile[i].IsNew = true;
                        _houseManager.ChildrensProfile[i].ID = -1;
                        _houseManager.ChildrensProfile[i].IsHungry = false;
                        _houseManager.ChildrensProfile[i].HeadMaterial = null;
                        _houseManager.ChildrensProfile[i].BodyMaterial = null;
                        _houseManager.CountOfChildren--;
                    }
                }

                if (_houseManager.CountOfChildren == 0)
                    _houseManager.HaveChild = false;

                if (_houseManager.PartnerProfile.IsHungry)
                {
                    _houseManager.HavePartner = false;
                    _houseManager.PartnerProfile.IsNew = true;
                    _houseManager.PartnerProfile.IsHungry = false;
                    _houseManager.PartnerProfile.HeadMaterial = null;
                    _houseManager.PartnerProfile.BodyMaterial = null;
                }
            }

            CalculateConsumedFood();

            _houseManager.SaveLoadState.Value = SaveState.SaveHouseData;
        }
    }
}