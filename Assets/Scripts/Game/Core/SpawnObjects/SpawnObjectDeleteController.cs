using Larva.Game.Tools;
using UnityEngine;

namespace Larva.Game.Core.SpawnObjects
{
    public class SpawnObjectDeleteController : MonoBehaviour
    {
        [field: SerializeField] public SpawnObjectsType ObjectType { get; private set; }
        public void Delete() => gameObject.SetActive(false);
    }
}