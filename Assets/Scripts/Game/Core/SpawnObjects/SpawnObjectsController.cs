using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Larva.Game.Core.SpawnObjects
{
    public class SpawnObjectsController : ObjectsDisposer
    {
        private readonly GameManager _gameManager;

        private CreateObjectsController _goodFoodCreator;
        private CreateObjectsController _badFoodCreator;
        private CreateObjectsController _obstaclesCreator;

        private List<Vector3> _usedPositions = new List<Vector3>();

        public SpawnObjectsController(GameManager gameManager)
        {
            _gameManager = gameManager;

            _gameManager.CurrentCountOfGoodFood.Value = _gameManager.CountOfGoodFood;
            _gameManager.CurrentCountOfGoodFood.SubscribeOnChange(RecreateFood);

            CreateControllers();
        }
        protected override void OnDispose()
        {
            _gameManager.CurrentCountOfGoodFood.UnSubscribeOnChange(RecreateFood);

            _goodFoodCreator?.Dispose();
            _badFoodCreator?.Dispose();
            _obstaclesCreator?.Dispose();

            base.OnDispose();
        }
        private void CreateControllers()
        {
            _goodFoodCreator = new CreateObjectsController(_usedPositions, _gameManager.CountOfGoodFood, SpawnObjectsType.GoodLeaves);
            AddController(_goodFoodCreator);
            _badFoodCreator = new CreateObjectsController(_usedPositions, _gameManager.CountOfBadFood, SpawnObjectsType.BadLeaves);
            AddController(_badFoodCreator);
            _obstaclesCreator = new CreateObjectsController(_usedPositions, _gameManager.CountOfObstacles, SpawnObjectsType.Obstacle);
            AddController(_obstaclesCreator);
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