using UnityEngine;

namespace Larva.Data
{
    [CreateAssetMenu(fileName = nameof(LarvaProfile), menuName = "Managers/Profiles/LarvaProfile")]
    public class LarvaProfile : ScriptableObject
    {
        [field: SerializeField] public Material HeadSkin { get; set; }
        [field: SerializeField] public Material BodySkin { get; set; }
        
        [HideInInspector] public string Language;
    }
}