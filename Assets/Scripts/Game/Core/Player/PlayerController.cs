using Larva.Data;
using Larva.Game.Core.SpawnObjects;
using Larva.Game.Data;
using Larva.Game.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Larva.Game.Core.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LarvaProfile _larvaProfile;
        [SerializeField] private LarvaManager _larvaManager;
        [SerializeField] private Transform _headTransform;
        [SerializeField] private List<Transform> _bodyNodeList;
        [SerializeField] private List<AudioSource> _eatSounds;

        private PlayerMoveController _playerMoveController;
        private PlayerDeathController _playerDeathController;
        private PlayerBodyController _playerBodyController;
        private LarvaSoundController _larvaSoundController;

        private Vector3 _forward;
        private Ray _ray;
        private RaycastHit _hit;

        private void Start()
        {
            _playerMoveController = new PlayerMoveController(_headTransform, _bodyNodeList, _larvaManager);
            _playerDeathController = new PlayerDeathController(_headTransform, _larvaManager);
            _playerBodyController = new PlayerBodyController(_larvaProfile, _bodyNodeList, _larvaManager);
            _larvaSoundController = new LarvaSoundController(_larvaManager, _eatSounds);

            _larvaManager.State.SubscribeOnChange(OnChangeState);
            _larvaManager.State.Value = PlayerState.Null;
        }
        private void OnDestroy()
        {
            _larvaManager.State.UnSubscribeOnChange(OnChangeState);
            _playerMoveController?.Dispose();
            _playerDeathController?.Dispose();
            _playerBodyController?.Dispose();
            _larvaSoundController?.Dispose();
        }
        private void FixedUpdate()
        {
            if (_larvaManager.State.Value == PlayerState.Death)
                return;

            _playerMoveController?.FixedExecute();
            CheckIntersectionBodyByRay();
        }
        private void OnChangeState()
        {
            switch (_larvaManager.State.Value)
            {
                case PlayerState.Death:
                    _playerMoveController?.Dispose();
                    _playerDeathController.Death();
                    break;
            }
        }
        private void CheckIntersectionBodyByRay()
        {
            _forward = transform.TransformDirection(Vector3.forward) * 0.5f;
            _ray = new Ray(transform.position, _forward);

            if (Physics.Raycast(_ray, out _hit, 1f))
            {
                if (_hit.collider.tag == "Player")
                {
                    _larvaManager.State.Value = PlayerState.Death;
                }
            }
        }
        public void OnCollisionEnter(Collision coll)
        {
            if (coll.transform.TryGetComponent(out SpawnObjectDeleteController spawnObject)) 
            {
                switch (spawnObject.ObjectType)
                {
                    case SpawnObjectsType.GoodLeaves:
                        _larvaManager.State.Value = PlayerState.EatGoodFood;
                        break;
                    case SpawnObjectsType.BadLeaves:
                        _larvaManager.State.Value = PlayerState.EatBadFood;
                        break;
                    case SpawnObjectsType.Obstacle:
                        _larvaManager.State.Value = PlayerState.Death;
                        break;
                }

                spawnObject.Delete();
            }
        }
    }
}