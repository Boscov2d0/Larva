using Larva.Data;
using Larva.House.Data;
using Larva.House.UI.Controller;
using Larva.Tools;
using UnityEngine;
using UnityEngine.UI;

using static Larva.Tools.LocalizationTextKeys.LocalizationHouseTextKeys;

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
        [SerializeField] private Text _kitchen3dText;
        [SerializeField] private Text _bedroomInManiHall3dText;
        [SerializeField] private Text _mainHallInBedroom3dText;
        [SerializeField] private Text _childrenRoom3dText;
        [SerializeField] private Text _bedroomInChildrenRoom3dText;
        [SerializeField] private Text _mainHallInKitchen3dText;

        private LoadController _loadController;
        private CameraController _cameraController;
        private HouseRoomsUpgradesController _houseRoomsUpgradesController;
        private HUDController _hudController;

        private void Start()
        {
            ResourcesLoader.InstantiateObject<Storage>(_houseManager.PathForObjects + _houseManager.StoragePath);

            CreateControllers();
            
            _houseManager.HouseState.SubscribeOnChange(onStateChange);

            _houseManager.CountOfFood.Value += _larvaProfile.Food.Value;
            _larvaProfile.Food.Value = 0;
            if (_houseManager.CountOfFood.Value > _houseManager.PotCapacity * _houseManager.StorageCapacity * _houseManager.CountOfStorage)
                _houseManager.CountOfFood.Value = _houseManager.PotCapacity * _houseManager.StorageCapacity * _houseManager.CountOfStorage;


            _localizationManager.HouseTable.SubscribeOnChange(SetRoomsNameText);
            SetRoomsNameText();
        }
        private void OnDestroy()
        {
            _houseManager.HouseState.UnSubscribeOnChange(onStateChange);
            _localizationManager.MenuTable.UnSubscribeOnChange(SetRoomsNameText);

            _loadController?.Dispose();
            _hudController?.Dispose();
            _cameraController?.Dispose();
            _houseRoomsUpgradesController?.Dispose();
        }
        private void CreateControllers() 
        {
            _loadController = new LoadController(_saveLoadManager, _houseManager);
            _cameraController = new CameraController(_houseManager);

            if (!_houseManager.AllBuilded)
                _houseRoomsUpgradesController = new HouseRoomsUpgradesController(_saveLoadManager, _localizationManager, _houseManager, _uiManager, _audioManager);

            _hudController = new HUDController(_houseManager, _uiManager, _audioManager);
        }
        private void SetRoomsNameText()
        {
            _outside3dText.text = _localizationManager.HouseTable.Value.GetEntry(Outside.ToString())?.GetLocalizedString();
            _kitchen3dText.text = _localizationManager.HouseTable.Value.GetEntry(Kitchen.ToString())?.GetLocalizedString();
            _bedroomInManiHall3dText.text = _localizationManager.HouseTable.Value.GetEntry(Bedroom.ToString())?.GetLocalizedString();
            _mainHallInBedroom3dText.text = _localizationManager.HouseTable.Value.GetEntry(MainHall.ToString())?.GetLocalizedString();
            _childrenRoom3dText.text = _localizationManager.HouseTable.Value.GetEntry(ChildrenRoom.ToString())?.GetLocalizedString();
            _bedroomInChildrenRoom3dText.text = _localizationManager.HouseTable.Value.GetEntry(Bedroom.ToString())?.GetLocalizedString();
            _mainHallInKitchen3dText.text = _localizationManager.HouseTable.Value.GetEntry(MainHall.ToString())?.GetLocalizedString();
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
        public void EnterToHouse() => _houseManager.HouseState.Value = Tools.HouseState.MainHall;
    }
}