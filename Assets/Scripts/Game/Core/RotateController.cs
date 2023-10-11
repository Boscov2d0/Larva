using UnityEngine;

namespace Larva.Game.Core
{
    public class RotateController : MonoBehaviour
    {
        [SerializeField] float _rotateSpeed;
        void FixedUpdate()
        {
            transform.Rotate(new Vector3(0, 1, 0) * _rotateSpeed * Time.fixedDeltaTime);
        }
    }
}