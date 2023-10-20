using UnityEngine;

namespace Larva.House.Data
{
    [CreateAssetMenu(fileName = nameof(FamilyMemberProfile), menuName = "Managers/Profiles/FamilyMemberProfile")]
    public class FamilyMemberProfile : ScriptableObject
    {
        [field: SerializeField] public bool IsNew = true;
        [field: SerializeField] public Material HeadMaterial;
        [field: SerializeField] public Material BodyMaterial;
        [field: SerializeField] public int ID = -1;
        [field: SerializeField] public int Cost;
        [field: SerializeField] public int FoodConsumption;
        [field: SerializeField] public bool IsHungry;
    }
}