using Larva.Game.Core.Player;
using Larva.Game.Data;
using Larva.Game.Tools;
using Larva.Tools;
using UnityEngine;

namespace Larva.Game.Core
{
    public class PreStartController : ObjectsDisposer
    {
        private GameManager _gameManager;
        private Camera _startCamera;
        private Vector3 _cameraPosition;
        private Quaternion _cameraRotation;
        private float _cameraMoveSpeed;
        private float _cameraRotateSpeed;

        /// <summary>
        /// Play hellow animation
        /// </summary>
        /// <param name="gameManager"></param>
        /// <param name="preStartManager"></param>
        /// <param name="larva"></param>
        public PreStartController( GameManager gameManager, PreStartManager preStartManager, LarvaView larva)
        {
            _gameManager = gameManager;

            _startCamera = ResourcesLoader.InstantiateAndGetObject<Camera>(gameManager.PathForObjects + preStartManager.StartCameraPath);
            AddGameObject(_startCamera.gameObject);

            _cameraPosition = preStartManager.CameraPosition;
            _cameraRotation = preStartManager.CameraRotation;
            _cameraMoveSpeed = preStartManager.CameraMoveSpeed;
            _cameraRotateSpeed = preStartManager.CameraRotateSpeed;

            larva.Animator.enabled = true;
        }
        public void FixedExecute()
        {
            if (_gameManager.GameState.Value == GameState.Start)
            {
                if (_startCamera.transform.position != _cameraPosition)
                    MoveCamera();
                else
                    _gameManager.GameState.Value = GameState.PreGame;
            }
        }
        private void MoveCamera()
        {
            _startCamera.transform.position = Vector3.MoveTowards(_startCamera.transform.position, _cameraPosition, _cameraMoveSpeed * Time.fixedDeltaTime);
            _startCamera.transform.rotation = Quaternion.RotateTowards(_startCamera.transform.rotation, _cameraRotation, _cameraRotateSpeed * Time.fixedDeltaTime);
        }
    }
}