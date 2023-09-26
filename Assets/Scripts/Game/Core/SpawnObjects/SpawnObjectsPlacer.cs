using Larva.Tools;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Larva.Game.Core.SpawnObjects
{
    public class SpawnObjectsPlacer : ObjectsDisposer
    {
        private float _maxSize;
        private Vector3 _curentPosition;
        private List<Vector3> _usedPositions;
        private Camera _camera;
        private Vector3 _topScreenBorder;
        private Vector3 _bottomScreenBorder;
        private Vector3 _leftScreenBorder;
        private Vector3 _rightScreenBorder;

        private static int _valueOfEmergencyEndCreateOperation = 5000;

        public SpawnObjectsPlacer(List<Vector3>  usedPositions)
        {
            _usedPositions = usedPositions;

            _camera = Camera.main;
            SetBorders();
        }
        private void SetBorders() 
        {
            _topScreenBorder = _camera.ScreenToWorldPoint(new Vector3((_camera.pixelWidth / 2), _camera.pixelHeight + 4, 0));
            _bottomScreenBorder = _camera.ScreenToWorldPoint(new Vector3((_camera.pixelWidth / 2), -4, 0));
            _leftScreenBorder = _camera.ScreenToWorldPoint(new Vector3(-4, (_camera.pixelHeight / 2), 0));
            _rightScreenBorder = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth + 4, (_camera.pixelHeight / 2), 0));
        }
        protected override void OnDispose()
        {
            _usedPositions.Clear();

            base.OnDispose();
        }
        public void Place(GameObject spawnObject)
        {
            _maxSize = GetObjectSize(spawnObject);

            bool placed = false;

            int numberOfAttempts = 0;

            for (; !placed; numberOfAttempts++)
            {
                int obstaclesHave = 0;

                _curentPosition = new Vector3((int)Random.Range(_leftScreenBorder.x, _rightScreenBorder.x), 0, (int)Random.Range(_bottomScreenBorder.z, _topScreenBorder.z));

                for (int i = 0; i < _usedPositions.Count; i++)
                {
                    float distanceX = _usedPositions[i].x - _curentPosition.x;
                    float distanceZ = _usedPositions[i].z - _curentPosition.z;

                    if (Mathf.Abs(distanceX) <= _maxSize && Mathf.Abs(distanceZ) <= _maxSize)
                        obstaclesHave++;
                }
                if (obstaclesHave == 0)
                {
                    _usedPositions.Add(_curentPosition);
                    spawnObject.transform.position = _curentPosition;
                    placed = true;
                }
                if(numberOfAttempts > _valueOfEmergencyEndCreateOperation)
                    placed = true;
            }
        }
        private float GetObjectSize(GameObject spawnObject)
        {
            if (spawnObject.transform.localScale.x > spawnObject.transform.localScale.z)
                return spawnObject.transform.localScale.x;
            else
                return spawnObject.transform.localScale.z;
        }
    }
}