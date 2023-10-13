using Larva.Data;
using Larva.House.Data;
using Larva.House.UI.Controller;
using Larva.Tools;
using UnityEngine;
using UnityEngine.UI;

using LHTK = Larva.Tools.LocalizationTextKeys.LocalizationHouseTextKeys;

namespace Larva.House.Core
{
    public class HouseController : MonoBehaviour
    {
        [SerializeField] private SaveLoadManager _saveLoadManager;
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private LarvaProfile _larvaProfile;
        [SerializeField] private Menu.Data.GameManager _gameManager;
        [SerializeField] private HouseManager _houseManager;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private AudioManager _audioManager;

        [SerializeField] private Text _outside3dText;

        private LoadController _loadController;
        private HUDController _hudController;
        private CameraController _cameraController;

        private void Start()
        {
            SetRoomsNameText();

            ResourcesLoader.InstantiateObject<Storage>(_houseManager.PathForObjects + _houseManager.StoragePath);

            _loadController = new LoadController(_saveLoadManager, _houseManager);
            _hudController = new HUDController(_houseManager, _uiManager, _audioManager);
            _cameraController = new CameraController(_houseManager);
            
            _houseManager.HouseState.SubscribeOnChange(onStateChange);

            _houseManager.CountOfFood.Value += _larvaProfile.Food.Value;
            _larvaProfile.Food.Value = 0;
            if (_houseManager.CountOfFood.Value > _houseManager.PotCapacity * _houseManager.StorageCapacity * _houseManager.CountOfStorage)
                _houseManager.CountOfFood.Value = _houseManager.PotCapacity * _houseManager.StorageCapacity * _houseManager.CountOfStorage;
        }
        private void OnDestroy()
        {
            _houseManager.HouseState.UnSubscribeOnChange(onStateChange);

            _hudController?.Dispose();
            _cameraController?.Dispose();
        }
        private void SetRoomsNameText()
        {
            _outside3dText.text = _localizationManager.MenuTable.Value.GetEntry(LHTK.Outside.ToString())?.GetLocalizedString();
        }
        private void FixedUpdate()
        {
            _cameraController?.FixedExecute();
        }
        private void onStateChange()
        {
            switch (_houseManager.HouseState.Value)
            {
                case Tools.HouseState.OutSideMenu:
                    _gameManager.GameState.Value = Menu.Tools.GameState.OutSideMenu;
                    break;
            }
        }
        public void EnterToHouse()
        {
            _houseManager.HouseState.Value = Tools.HouseState.MainHall;
        }
    }
}