using Larva.House.Data;
using System.Collections.Generic;
using UnityEngine;

using static Larva.Tools.Keys;

namespace Larva.House.Core
{
    public class ChildView : MonoBehaviour
    {
        [SerializeField] private HouseManager _houseManager;
        [SerializeField] private Renderer _head;
        [SerializeField] private List<Renderer> _body;

        private int _index;

        public void Initialize(int index)
        {
            _index = index;

            SetSkin();
            Place();
        }
        private void SetSkin()
        {
            if (_houseManager.ChildrensProfile[_index].HeadMaterial == null)
            {
                _houseManager.ChildrensProfile[_index].HeadMaterial = _houseManager.HeadMaterial[Random.Range(0, _houseManager.HeadMaterial.Count)];
                _houseManager.ChildrensProfile[_index].BodyMaterial = _houseManager.BodyMaterial[Random.Range(0, _houseManager.BodyMaterial.Count)];
            }

            _head.material = _houseManager.ChildrensProfile[_index].HeadMaterial;
            for (int i = 0; i < _body.Count; i++)
                _body[i].material = _houseManager.ChildrensProfile[_index].BodyMaterial;
            
            if (_head.material == null || _head.material.name == "Default-Material (Instance)")
            {
                _head.material = _houseManager.HeadMaterial[Random.Range(0, _houseManager.HeadMaterial.Count)];
                _head.material = _houseManager.BodyMaterial[Random.Range(0, _houseManager.BodyMaterial.Count)];

                _houseManager.ChildrensProfile[_index].HeadMaterial = _head.material;
                _houseManager.ChildrensProfile[_index].BodyMaterial = _body[0].material;
            }

            _houseManager.SaveLoadState.Value = SaveState.SaveLarvaData;
        }
        private void Place()
        {
            int placePosition = Random.Range(0, 1);
            if (placePosition == 0)
                PlaceInHouse();
            else
                PlaceInBed();
        }
        private void PlaceInHouse()
        {
            int housePositionIndex = Random.Range(0, _houseManager.ChildrensPositions.Count);
            if (!_houseManager.ChildrensPositions[housePositionIndex].IsHave)
            {
                transform.position = _houseManager.ChildrensPositions[housePositionIndex].Position;
                _houseManager.ChildrensPositions[housePositionIndex].IsHave = true;
            }
            else
                PlaceInBed();
        }
        private void PlaceInBed() => transform.position = _houseManager.PillowManagers[_houseManager.ChildrensProfile[_index].ID].Position;
    }
}