using UnityEngine;

namespace Larva.Menu.Data
{
    [CreateAssetMenu(fileName = nameof(VideoManager), menuName = "Managers/Menu/VideoManager")]
    public class VideoManager : ScriptableObject
    {
        [HideInInspector] public Vector3 ScreenResolution;
        [HideInInspector] public bool Fullscreen;
    }
}