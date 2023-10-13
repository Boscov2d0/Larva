using Larva.House.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Larva.House.Core
{
    public class Pot : MonoBehaviour
    {
        [SerializeField] private HouseManager _houseManager;
        [SerializeField] private List<GameObject> _bodyList;

        private GameObject _body;
        private int _index;
        private int _capacity;

        public void Initialize(int index, int capacity)
        {
            _index = index;
            _capacity = capacity;

            if (!_houseManager.PotManagers[_index].IsActive)
                SetBody();

            _body = _bodyList[_houseManager.PotManagers[_index].BodyIndex];
            _body.transform.GetComponent<Renderer>().material = _houseManager.PotManagers[_index].Material;
            _body.SetActive(true);
            _houseManager.PotManagers[_index].Capacity = _capacity;
        }
        private void SetBody()
        {
            _houseManager.PotManagers[_index].BodyIndex = Random.Range(0, _bodyList.Count);
            _houseManager.PotManagers[_index].Material = _houseManager.PotMaterials[Random.Range(0, _houseManager.PotMaterials.Count)];
            _houseManager.PotManagers[_index].IsActive = true;
        }
    }
}