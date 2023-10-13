using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

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
        }
        public struct HouseData
        {
            public int CountOfFood;
            public List<PotData> Pots;
        }
        [Serializable]
        public struct PotData
        {
            public int BodyIndex;
            public Material Material;
            public bool IsActive;
            public int Capacity;
        }
    }
}