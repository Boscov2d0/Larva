using UnityEngine;

namespace Larva.Game.Data
{
    [CreateAssetMenu(fileName = nameof(PreStartManager), menuName = "Managers/Game/PreStartManager")]
    public class PreStartManager : ScriptableObject
    {
        [field: SerializeField] public string StartCameraPath { get; private set; }
        [field: SerializeField] public int CameraMoveSpeed { get; private set; }
        [field: SerializeField] public int CameraRotateSpeed { get; private set; }
        [field: SerializeField] public Vector3 CameraPosition { get; private set; }
        [field: SerializeField] public Quaternion CameraRotation { get; private set; }
    }
}