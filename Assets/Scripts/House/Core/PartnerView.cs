using Larva.Data;
using Larva.House.Data;
using System.Collections.Generic;
using UnityEngine;

using static Larva.Tools.Keys;

namespace Larva.House.Core
{
    public class PartnerView : MonoBehaviour
    {
        [SerializeField] private HouseManager _houseManager;
        [SerializeField] private Renderer _head;
        [SerializeField] private Renderer _hand;
        [SerializeField] private List<Renderer> _body;

        public void Initialize()
        {
            SetSkin();

            transform.position = _houseManager.PartnerPosition;
        }
        private void SetSkin()
        {
            if (_houseManager.PartnerProfile.HeadMaterial == null)
            {
                _houseManager.PartnerProfile.HeadMaterial = _houseManager.HeadMaterial[Random.Range(0, _houseManager.HeadMaterial.Count)];
                _houseManager.PartnerProfile.BodyMaterial = _houseManager.BodyMaterial[Random.Range(0, _houseManager.BodyMaterial.Count)];
            }

            _head.material = _houseManager.PartnerProfile.HeadMaterial;

            _hand.material = _houseManager.PartnerProfile.BodyMaterial;
            for (int i = 0; i < _body.Count; i++)
                _body[i].material = _houseManager.PartnerProfile.BodyMaterial;

            if (_head.material == null || _head.material.name == "Default-Material (Instance)")
            {
                _head.material = _houseManager.HeadMaterial[Random.Range(0, _houseManager.HeadMaterial.Count)];
                _head.material = _houseManager.BodyMaterial[Random.Range(0, _houseManager.BodyMaterial.Count)];

                _houseManager.PartnerProfile.HeadMaterial = _head.material;
                _houseManager.PartnerProfile.BodyMaterial = _body[0].material;
            }

            _houseManager.SaveLoadState.Value = SaveState.SaveLarvaData;
        }
    }
}