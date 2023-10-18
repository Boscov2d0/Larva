using Larva.House.Data;
using Larva.House.Tools;
using UnityEngine;

namespace Larva.House.Core
{
    public class Bed : MonoBehaviour
{
        [SerializeField] private HouseManager _houseManager;
        [SerializeField] private GameObject _ghostBody;
        [SerializeField] private GameObject _normalBody;

        private Ray _raycast;
        private RaycastHit _raycastHit;

        private void Update()
        {
            if (_houseManager.RoomState.Value == HouseState.RoomState.Bedroom)
            {
                _raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_raycast, out _raycastHit))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_raycastHit.collider.TryGetComponent(out Bed bed))
                            AddMember();
                    }
                }
            }

            CheñkState();
        }
        private void AddMember() 
        {
            if (_houseManager.Bedroom.IsActive)
            {
                _houseManager.AddPartner = true;
                _houseManager.RoomState.Value = _houseManager.RoomState.Value;
            }
        }
        private void CheñkState() 
        {
            if (_houseManager.HavePartner)
            {
                _ghostBody.SetActive(false);
                _normalBody.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}