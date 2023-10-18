using Larva.House.Data;
using UnityEngine;

namespace Larva.House.Core
{
    public class Pillow : MonoBehaviour
    {
        [SerializeField] private HouseManager _houseManager;
        [SerializeField] private PillowManager _pillowManager;
        [SerializeField] private GameObject _ghostBody;
        [SerializeField] private GameObject _normalBody;
        [SerializeField] private int _id;

        private int _countOfChildren;

        private void Start()
        {
            CheñkState();
        }
        public void AddMember()
        {
            if (_houseManager.ChildrenRoom.IsActive)
            {
                _houseManager.ChildrenID = _id;
                Debug.Log(_houseManager.ChildrenID);
                _countOfChildren = _houseManager.CountOfChildren;
                _houseManager.AddChild = true;
                //State.AddChild
                _houseManager.RoomState.Value = _houseManager.RoomState.Value;
            }
        }
        public void SetState() 
        {
            if (_countOfChildren != _houseManager.CountOfChildren)
            {
                _pillowManager.IsActive = true;
                CheñkState();
            }
        }
        private void CheñkState()
        {
            if (_pillowManager.IsActive) 
            {
                _ghostBody.SetActive(false);
                _normalBody.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}