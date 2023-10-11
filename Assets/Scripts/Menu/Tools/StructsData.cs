using System;
using UnityEngine;
using UnityEngine.Localization;

namespace Larva.Menu.Tools
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
    }
}