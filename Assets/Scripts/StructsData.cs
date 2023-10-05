using System;
using UnityEngine;

namespace Larva.Menu.Tools
{
    public class StructsData
    {
        [Serializable]
        public struct GameSettingsData
        {
            public string Language;
            public float SoundsVolume;
            public float MusicVolume;
            public Vector3 ScreenResolution;
            public bool Fullscreen;
        }
    }
}