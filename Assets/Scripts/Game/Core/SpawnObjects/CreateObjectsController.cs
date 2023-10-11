using Larva.Game.Tools;
using Larva.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Larva.Game.Core.SpawnObjects
{
    public class CreateObjectsController : ObjectsDisposer
    {
        private const string _prefabPath = "Game/SpawnObjects/";

        private int _startCount;
        private GameObject _object;
        private GameObject _prefab;
        private Transform _objectsPool;
        private List<Transform> _freeObjects;
        private SpawnObjectsPlacer _spawnObjectsPlacer;

        public CreateObjectsController(List<Vector3> usedPositions, int startCount, SpawnObjectsType spawnObjectsType)
        {
            _startCount = startCount;
            _freeObjects = new List<Transform>();
            _spawnObjectsPlacer = new SpawnObjectsPlacer(usedPositions);
            AddController(_spawnObjectsPlacer);

            CreatePool(spawnObjectsType);
            GetObjectSpecies(spawnObjectsType);
        }
        protected override void OnDispose()
        {
            _freeObjects.Clear();
            _spawnObjectsPlacer?.Dispose();
        }
        public void GetObjectSpecies(SpawnObjectsType spawnObjectsType)
        {
            switch (spawnObjectsType)
            {
                case SpawnObjectsType.GoodLeaves:
                    CreateObjecst<GoodLeaves>();
                    break;
                case SpawnObjectsType.BadLeaves:
                    CreateObjecst<BadLeaves>();
                    break;
                case SpawnObjectsType.Obstacle:
                    CreateObjecst<Obstacle>();
                    break;
            }
        }
        private string GetObjectSubspecies<T>()
        {
            var count = Enum.GetValues(typeof(T)).Length;
            int randomObstacle = Random.Range(0, count);
            return Enum.GetName(typeof(T), randomObstacle);
        }
        private void CreatePool(SpawnObjectsType spawnObjectsType)
        {
            _objectsPool = new GameObject(spawnObjectsType.ToString()).transform;
        }
        private void CreateObjecst<T>()
        {
            for (int i = 0; i < _startCount; i++)
            {
                _prefab = Resources.Load(_prefabPath + GetObjectSubspecies<T>()) as GameObject;

                if (_freeObjects.Count == 0)
                {
                    _object = GameObject.Instantiate(_prefab);
                    _object.transform.SetParent(_objectsPool);
                }
                else
                {
                    _object = _freeObjects[0].gameObject;
                    _object.SetActive(true);
                }

                _spawnObjectsPlacer.Place(_object);

                _object = null;
            }
        }
    }
}