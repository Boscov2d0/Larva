using System.Collections.Generic;
using UnityEngine;

namespace Larva.Core
{
    public class LarvaView : MonoBehaviour
    {
        [field: SerializeField] public Renderer Head { get; set; }
        [field: SerializeField] public Renderer Hand { get; set; }
        [field: SerializeField] public List<Renderer> Body { get; set; }
    }
}