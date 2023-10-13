using Larva.Tools;
using Larva.House.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Larva.House.Tools;
using Larva.Data;

namespace Larva.House.Core
{
    public class Storage : MonoBehaviour
    {
        [SerializeField] private SaveLoadManager _saveLoadManager;
        [SerializeField] private HouseManager _houseManager;
        [SerializeField] private List<Transform> _storage1PlacesPosition;
        [SerializeField] private List<Transform> _storage2PlacesPosition;
        [SerializeField] private Text _storage1FoodCountText;
        [SerializeField] private Text _storage2FoodCountText;

        private int _countOfFood;
        private int _countOfPot;
        private int _lastPot;

        private List<Vector3> _storagePlaces = new List<Vector3>();
        private List<Pot> _potsList = new List<Pot>();

        private void Start()
        {
            _countOfFood = _houseManager.CountOfFood.Value;

            _houseManager.CountOfFood.SubscribeOnChange(SetFoodCountText);

            CountPotsNumber();
            PlaceFood();
            CleanFreePots();

            SetFoodCountText();
        }
        private void OnDestroy()
        {
            _storagePlaces.Clear();
            _potsList.Clear();

            _houseManager.CountOfFood.UnSubscribeOnChange(SetFoodCountText);
        }
        private void CountPotsNumber()
        {
            _countOfPot = _houseManager.CountOfFood.Value / _houseManager.PotCapacity;

            if (_houseManager.CountOfFood.Value % _houseManager.PotCapacity > 0)
                _countOfPot++;

            _lastPot = _countOfPot - 1;
        }
        private void PlaceFood()
        {
            CreatePlaces(_storage1PlacesPosition);

            int freePlace = _houseManager.StorageCapacity;
            int countOfPot = _countOfPot;
            int countOfFood = _houseManager.CountOfFood.Value;

            for (int i = 0, p = 0; i < countOfPot; i++, p++)
            {
                Pot pot = ResourcesLoader.InstantiateAndGetObject<Pot>(_houseManager.PathForObjects + _houseManager.PotPath);
                pot.transform.position = _storagePlaces[p];

                if (countOfFood > _houseManager.PotCapacity)
                {
                    pot.Initialize(i, _houseManager.PotCapacity);
                    countOfFood -= _houseManager.PotCapacity;
                }
                else
                    pot.Initialize(i, countOfFood);

                _potsList.Add(pot);

                freePlace--;

                if (freePlace <= 0)
                {
                    CreatePlaces(_storage2PlacesPosition);
                    p = -1;
                    freePlace = _houseManager.StorageCapacity;
                }
            }

            Saver.SaveHouseData(_saveLoadManager, _houseManager);
        }
        private void CreatePlaces(List<Transform> placesList)
        {
            _storagePlaces.Clear();

            for (int i = 0; i < placesList.Count; i++)
            {
                _storagePlaces.Add(placesList[i].position);
            }
        }
        private void CleanFreePots()
        {
            for (int i = _countOfPot; i < _houseManager.PotManagers.Count; i++)
            {
                _houseManager.PotManagers[i].IsActive = false;
                _houseManager.PotManagers[i].Capacity = 0;
            }
        }
        private void SetFoodCountText()
        {
            int storage1CountOfFood = 0;
            int storage2CountOfFood = 0;
            int storage2 = _houseManager.StorageCapacity + 1;

            for (int i = 0; i < _houseManager.PotManagers.Count && _houseManager.PotManagers[i].IsActive; i++)
            {
                if (i < _houseManager.StorageCapacity)
                    storage1CountOfFood += _houseManager.PotManagers[i].Capacity;
                else
                    storage2CountOfFood += _houseManager.PotManagers[i].Capacity;
            }

            DisplayFoodCountText(storage1CountOfFood, storage2CountOfFood);
        }
        private void DisplayFoodCountText(int storage1FoodCount, int storage2FoodCount)
        {
            _storage1FoodCountText.text = storage1FoodCount.ToString();
            _storage2FoodCountText.text = storage2FoodCount.ToString();
        }
    }
}