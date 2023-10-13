using System.Collections.Generic;
using UnityEngine;

namespace Larva.Game.Core.Player
{
    public class LarvaView : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Renderer Head { get; set; }
        [field: SerializeField] public Renderer Hand { get; set; }
        [field: SerializeField] public List<Renderer> Body { get; set; }
    }
}