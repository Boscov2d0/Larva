using Larva.Core;
using Larva.House.Tools;
using Larva.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Larva.House.Data
{
    [CreateAssetMenu(fileName = nameof(HouseManager), menuName = "Managers/House/HouseManager")]
    public class HouseManager : ScriptableObject
    {
        [TextArea(1, 1)]
        public string Discription1;
        [field: SerializeField] public string PathForObjects { get; private set; }
        [field: SerializeField] public string PotPath { get; private set; }
        [field: SerializeField] public string StoragePath { get; private set; }

        [Space]
        [TextArea(1, 1)]
        public string Discription2;
        [field: SerializeField] public SubscriptionProperty<int> CountOfFood = new SubscriptionProperty<int>();
        [field: SerializeField] public List<PotManager> PotManagers { get; private set; }
        [field: SerializeField] public List<Material> PotMaterials { get; private set; }
        [field: SerializeField] public int PotCapacity { get; private set; }
        [field: SerializeField] public int StorageCapacity { get; private set; }
        [field: SerializeField] public int CountOfStorage { get; private set; }

        [field: SerializeField] public SubscriptionProperty<HouseState> HouseState = new SubscriptionProperty<HouseState>();

        [Space]
        [TextArea(1, 1)]
        public string Discription3;
        [HideInInspector] public bool AllBuilded;
        [field: SerializeField] public RoomManager Bedroom { get; private set; }
        [field: SerializeField] public RoomManager ChildrenRoom { get; private set; }
        [field: SerializeField] public RoomManager Kitchen { get; private set; }

        [Space]
        [TextArea(1, 1)]
        public string Discription4;
        [field: SerializeField] public Vector3 OutSideCameraPosition { get; private set; }
        [field: SerializeField] public Vector3 MainHallCameraPosition { get; private set; }
        [field: SerializeField] public Vector3 BedroomCameraPosition { get; private set; }
        [field: SerializeField] public Vector3 ChildrenRoomCameraPosition { get; private set; }
        [field: SerializeField] public Vector3 KitchenCameraPosition { get; private set; }
        [field: SerializeField] public float CameraMoveSpeed { get; private set; }

        [Space]
        [TextArea(1, 1)]
        public string Discription5;
        [field: SerializeField] public SubscriptionProperty<Material> ChoosedHeadSkin = new SubscriptionProperty<Material>();
        [field: SerializeField] public SubscriptionProperty<Material> ChoosedBodySkin = new SubscriptionProperty<Material>();
        [HideInInspector] public LarvaView MenuLarva { get; set; }
    }
}