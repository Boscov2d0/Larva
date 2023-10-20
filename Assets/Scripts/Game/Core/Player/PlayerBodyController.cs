using Larva.Game.Data;
using Larva.Tools;
using System.Collections.Generic;
using UnityEngine;

using static Larva.Game.Tools.States;

namespace Larva.Game.Core.Player
{
    public class PlayerBodyController : ObjectsDisposer
    {
        private const string _nameOfPool = "PoolOfBody";

        private LarvaManager _larvaManager;
        private List<Transform> _bodyNodeList;
        private List<Transform> _freeObjects;
        private Transform _poolOfBody;
        private Transform _body;
        private Transform _bodyPrefab;

        public PlayerBodyController(List<Transform> bodyNodeList, LarvaManager larvaManager)
        {
            _larvaManager = larvaManager;
            _bodyNodeList = bodyNodeList;

            _poolOfBody = new GameObject(_nameOfPool).transform;
            _poolOfBody.transform.position = new Vector3(100, 0, 0);
            _freeObjects = new List<Transform>();

            _larvaManager.State.SubscribeOnChange(OnChangeState);

            _bodyPrefab = ResourcesLoader.InstantiateAndGetObject<Transform>(_larvaManager.ObjectsPath + _larvaManager.BodyPath, _poolOfBody);
        }
        protected override void OnDispose() 
        {
            _bodyNodeList.Clear();
            _freeObjects.Clear();
            _larvaManager.State.UnSubscribeOnChange(OnChangeState);

            base.OnDispose();
        }
        private void OnChangeState() 
        {
            switch (_larvaManager.State.Value) 
            {
                case LarvaState.EatGoodFood:
                    GrouBody();
                    break;
                case LarvaState.EatBadFood:
                    LoseBody();
                    break;
            }
        }
        private void GrouBody()
        {
            if (_freeObjects.Count > 0)
                _body = _freeObjects[0];
            else
            {
                _body = GameObject.Instantiate(_bodyPrefab);
                _body.transform.SetParent(_poolOfBody);
                _body.transform.position = _poolOfBody.position;
            }

            _bodyNodeList.Add(_body.transform);
            _body.GetComponent<Renderer>().material = _larvaManager.BodySkin;
            _body = null;
        }
        private void LoseBody()
        {
            if (_bodyNodeList.Count > 2)
            {
                _body = _bodyNodeList[_bodyNodeList.Count - 1];
                _body.transform.position = _poolOfBody.transform.position;
                _bodyNodeList.Remove(_bodyNodeList[_bodyNodeList.Count - 1]);
            }
            else
                _larvaManager.State.Value = LarvaState.Death;
        }
    }
}