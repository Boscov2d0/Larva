using System.Collections.Generic;
using UnityEngine;

namespace Larva.Game.Core.Player
{
    public class PlayerLarvaView : MonoBehaviour
    {
        [field: SerializeField] public Renderer Head { get; private set; }
        [field: SerializeField] public Renderer Hand { get; private set; }
        [field: SerializeField] public List<Renderer> Body { get; private set; }
    }
}