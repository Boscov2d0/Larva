using Larva.House.Data;
using System.Collections.Generic;
using UnityEngine;

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
            _head.material = _houseManager.PartnerProfile.HeadMaterial;

            _hand.material = _houseManager.PartnerProfile.BodyMaterial;
            for (int i = 0; i < _body.Count; i++)
                _body[i].material = _houseManager.PartnerProfile.BodyMaterial;
        }
    }
}