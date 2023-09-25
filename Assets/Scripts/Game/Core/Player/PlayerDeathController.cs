using Larva.Game.Data;
using Larva.Tools;
using UnityEngine;

namespace Larva.Game.Core.Player
{
    public class PlayerDeathController : ObjectsDisposer
    {
        private Transform _headTransform;
        private LarvaManager _larvaManager;

        public PlayerDeathController(Transform headTransform, LarvaManager larvaManager)
        {
            _headTransform = headTransform;
            _larvaManager = larvaManager;
        }
        public void Death() => ResourcesLoader.InstantiateAndGetObject<GameObject>(_larvaManager.ObjectsPath + _larvaManager.StarsPath, _headTransform);
    }
}