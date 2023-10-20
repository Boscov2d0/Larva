using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

using static Larva.Tools.Keys;

namespace Larva.Tools
{
    public class StructsData
    {
        [Serializable]
        public struct GameSettingsData
        {
            public Locale Language;
            public float SoundsVolume;
            public float MusicVolume;
            public Vector3 ScreenResolution;
            public bool Fullscreen;
            public DayTime DayTime;
        }
        public struct HouseData
        {
            public int CountOfFood;
            public List<PotData> Pots;
            public bool BedroomIsActive;
            public bool ChildrenRoomIsActive;
            public bool KitcheIsActive;
            public bool HavePartner;
            public bool HaveChild;
            public int CountOfChildren;
            public GameMode GameMode;
            public DayOfWeek DayForGiveFood;
            public FamilyData Partner;
            public List<bool> PillowsIsActive;
            public List<FamilyData> Childrens;
        }
        [Serializable]
        public struct PotData
        {
            public int BodyIndex;
            public Material Material;
            public bool IsActive;
            public int Capacity;
        }
        [Serializable]
        public struct LarvaData
        {
            public Material HeadSkin;
            public Material BodySkin;
        }
        [Serializable]
        public struct FamilyData
        {
            public bool IsNew;
            public int ID;
            public bool IsHungry;
            public LarvaData LarvaData;
        }
    }
}