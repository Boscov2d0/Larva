using Larva.House.Data;
using System.Collections.Generic;
using UnityEngine;

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
            _head.material = _houseManager.ChildrensProfile[_index].HeadMaterial;
            for (int i = 0; i < _body.Count; i++)
                _body[i].material = _houseManager.ChildrensProfile[_index].BodyMaterial;
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