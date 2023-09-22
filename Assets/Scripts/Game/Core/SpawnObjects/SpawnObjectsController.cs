using Larva.Game.Data;
using Larva.Game.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Larva.Game.Core.SpawnObjects
{
    public class SpawnObjectsController : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        private CreateObjectsController _goodFoodCreator;
        private CreateObjectsController _badFoodCreator;
        private CreateObjectsController _obstaclesCreator;

        private List<Vector3> _usedPositions = new List<Vector3>();

        private Ray _ray;

        private void Start()
        {
            _gameManager.CurrentCountOfGoodFood.Value = _gameManager.CountOfGoodFood;
            _gameManager.CurrentCountOfGoodFood.SubscribeOnChange(RecreateFood);

            CreateControllers();
        }
        private void OnDestroy()
        {
            _gameManager.CurrentCountOfGoodFood.UnSubscribeOnChange(RecreateFood);

            _goodFoodCreator?.Dispose();
            _badFoodCreator?.Dispose();
            _obstaclesCreator?.Dispose();
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(_ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.TryGetComponent(out SpawnObjectDeleteController spawnObject))
                    {
                        if (spawnObject.ObjectType == SpawnObjectsType.GoodLeaves)
                        {
                            spawnObject.Delete();
                            _gameManager.CurrentCountOfGoodFood.Value--;
                        }
                        if (spawnObject.ObjectType == SpawnObjectsType.Obstacle)
                        {
                            spawnObject.Delete();
                            _gameManager.GameState.Value = GameState.Lose;
                        }
                    }
                }
            }
        }
        private void CreateControllers()
        {
            _goodFoodCreator = new CreateObjectsController(_usedPositions, _gameManager.CountOfGoodFood, SpawnObjectsType.GoodLeaves);
            _badFoodCreator = new CreateObjectsController(_usedPositions, _gameManager.CountOfBadFood, SpawnObjectsType.BadLeaves);
            _obstaclesCreator = new CreateObjectsController(_usedPositions, _gameManager.CountOfObstacles, SpawnObjectsType.Obstacle);
        }
        private void RecreateFood()
        {
            if (_gameManager.CurrentCountOfGoodFood.Value <= 0)
            {
                _goodFoodCreator.GetObjectSpecies(SpawnObjectsType.GoodLeaves);
                _badFoodCreator.GetObjectSpecies(SpawnObjectsType.BadLeaves);

                _gameManager.CurrentCountOfGoodFood.Value = _gameManager.CountOfGoodFood;
            }
        }
    }
}