using Larva.Game.Data;
using Larva.Game.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Larva.Game.Core.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private Transform _headTransform;
        [SerializeField] private List<Transform> _bodyNodeList;

        private PlayerMoveController _playerMoveController;

        private Vector3 _forward;
        private Ray _ray;
        private RaycastHit _hit;

        private void Start()
        {
            _playerMoveController = new PlayerMoveController(_headTransform, _bodyNodeList, _playerManager);
        }
        private void OnDestroy()
        {
            _playerMoveController?.Dispose();
        }
        private void FixedUpdate()
        {
            _playerMoveController?.FixedExecute();
            CheckIntersectionBodyByRay();
        }
        private void CheckIntersectionBodyByRay()
        {
            _forward = transform.TransformDirection(Vector3.forward) * 0.5f;
            _ray = new Ray(transform.position, _forward);

            if (Physics.Raycast(_ray, out _hit, 1f))
            {
                if (_hit.collider.tag == "Player")
                {
                    _playerManager.State.Value = PlayerState.Death;
                }
            }
        }
        public void OnCollisionEnter(Collision coll)
        {
            string name = coll.transform.name;
            switch (name)
            {
                case "GoodFood":
                    _playerManager.State.Value = PlayerState.EatGoodFood;
                    break;
                case "BadFood":
                    _playerManager.State.Value = PlayerState.EatBadFood;
                    break;
                case "Obsracle":
                    _playerManager.State.Value = PlayerState.Death;
                    break;
            }
        }
    }
}