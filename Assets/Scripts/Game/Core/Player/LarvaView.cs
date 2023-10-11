using System.Collections.Generic;
using UnityEngine;

namespace Larva.Game.Core.Player
{
    public class LarvaView : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }
    }
}