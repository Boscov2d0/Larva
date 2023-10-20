using Larva.Data;
using Larva.House.Data;
using Larva.House.Tools;
using Larva.House.UI.Controller;
using Larva.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static Larva.House.Tools.HouseState;
using static Larva.Tools.Keys;
using static Larva.Tools.LocalizationTextKeys.LocalizationHouseTextKeys;
using Random = UnityEngine.Random;

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
        [SerializeField] private Text _familyNeedFood3dTex;

        private LoadController _loadController;
        private CameraController _cameraController;
        private HUDController _hudController;
        private TouchScreenMoveCameraController _touchScreenMoveCameraController;
        private FoodFamilyController _foodFamilyController;

        private Light _pointLight;
        private PartnerView _partner;
        private List<ChildView> _childrens = new List<ChildView>();

        private void Start()
        {
            _houseManager.MenuLarva = _gameManager.MenuLarva;

            ResourcesLoader.InstantiateObject<Storage>(_houseManager.PathForObjects + _houseManager.StoragePath);

            _gameManager.DayTime.SubscribeOnChange(SetLight);

            CreateControllers();

            _houseManager.RoomState.SubscribeOnChange(OnRoomStateChange);
            _houseManager.ActionState.SubscribeOnChange(OnActionStateChange);
            _houseManager.SaveLoadState.SubscribeOnChange(SaveData);

            SetLight();
            SetLarvaSkin();

            _houseManager.CountOfFood.Value += _larvaProfile.Food.Value;
            _larvaProfile.Food.Value = 0;
            if (_houseManager.CountOfFood.Value > _houseManager.PotCapacity * _houseManager.StorageCapacity * _houseManager.CountOfStorage)
                _houseManager.CountOfFood.Value = _houseManager.PotCapacity * _houseManager.StorageCapacity * _houseManager.CountOfStorage;

            for (int i = 0; i < _houseManager.ChildrensPositions.Count; i++)
                _houseManager.ChildrensPositions[i].IsHave = false;

            _houseManager.ActionState.Value = ActionState.InstantiateChild;

            _localizationManager.HouseTable.SubscribeOnChange(SetRoomsNameText);
            SetRoomsNameText();
        }
        private void OnDestroy()
        {
            _gameManager.DayTime.UnSubscribeOnChange(SetLight);
            _houseManager.RoomState.UnSubscribeOnChange(OnRoomStateChange);
            _houseManager.ActionState.UnSubscribeOnChange(OnActionStateChange);
            _localizationManager.HouseTable.UnSubscribeOnChange(SetRoomsNameText);
            _houseManager.SaveLoadState.UnSubscribeOnChange(SaveData);

            _loadController?.Dispose();
            _hudController?.Dispose();
            _cameraController?.Dispose();
            _touchScreenMoveCameraController?.Dispose();

            _childrens.Clear();
        }
        private void CreateControllers()
        {
            _loadController = new LoadController(_saveLoadManager, _houseManager);
            _cameraController = new CameraController(_houseManager);

#if UNITY_ANDROID && !UNITY_EDITOR
            _touchScreenMoveCameraController = new TouchScreenMoveCameraController(_houseManager);
#endif
            _hudController = new HUDController(_saveLoadManager, _localizationManager, _houseManager, _uiManager, _audioManager, _larvaProfile);

            if (_houseManager.GameMode == GameMode.Real)
            {
                _foodFamilyController = new FoodFamilyController(_houseManager);
                _foodFamilyController.Initialize();
            }
        }
        private void SetLight()
        {
            Destroy(_pointLight?.gameObject);

            switch (_gameManager.DayTime.Value)
            {
                case DayTime.Auto:
                    DateTime time = DateTime.Now;
                    if ((time.Hour >= 7 && time.Minute > 0) && (time.Hour <= 16 && time.Minute > 0))
                        _pointLight = ResourcesLoader.InstantiateAndGetObject<Light>(_houseManager.PathForObjects + _houseManager.DaySpotLightPath);
                    else if ((time.Hour >= 17 && time.Minute > 0) && (time.Hour <= 22 && time.Minute > 0))
                        _pointLight = ResourcesLoader.InstantiateAndGetObject<Light>(_houseManager.PathForObjects + _houseManager.EveningSpotLightPath);
                    else
                        _pointLight = ResourcesLoader.InstantiateAndGetObject<Light>(_houseManager.PathForObjects + _houseManager.NightSpotLightPath);
                    break;
                case DayTime.Day:
                    _pointLight = ResourcesLoader.InstantiateAndGetObject<Light>(_houseManager.PathForObjects + _houseManager.DaySpotLightPath);
                    break;
                case DayTime.Evening:
                    _pointLight = ResourcesLoader.InstantiateAndGetObject<Light>(_houseManager.PathForObjects + _houseManager.EveningSpotLightPath);
                    break;
                case DayTime.Night:
                    _pointLight = ResourcesLoader.InstantiateAndGetObject<Light>(_houseManager.PathForObjects + _houseManager.NightSpotLightPath);
                    break;
            }
        }
        private void SetLarvaSkin()
        {
            if (_larvaProfile.HeadSkin == null)
            {
                _larvaProfile.HeadSkin = _houseManager.HeadMaterial[Random.Range(0, _houseManager.HeadMaterial.Count)];
                _larvaProfile.BodySkin = _houseManager.BodyMaterial[Random.Range(0, _houseManager.BodyMaterial.Count)];
            }

            _houseManager.MenuLarva.Head.material = _larvaProfile.HeadSkin;

            for (int i = 0; i < _houseManager.MenuLarva.Body.Count; i++)
            {
                _houseManager.MenuLarva.Body[i].material = _larvaProfile.BodySkin;
            }

            if (_houseManager.MenuLarva.Head.material == null || _houseManager.MenuLarva.Head.material.name == "Default-Material (Instance)")
            {
                _larvaProfile.HeadSkin = _houseManager.HeadMaterial[Random.Range(0, _houseManager.HeadMaterial.Count)];
                _larvaProfile.BodySkin = _houseManager.BodyMaterial[Random.Range(0, _houseManager.BodyMaterial.Count)];
                SetLarvaSkin();
            }
            _houseManager.SaveLoadState.Value = SaveState.SaveLarvaData;
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
        private void Update()
        {
            _hudController?.Execute();

#if UNITY_ANDROID && !UNITY_EDITOR
            _touchScreenMoveCameraController?.Execute();
#endif
        }
        private void FixedUpdate()
        {
            _cameraController?.FixedExecute();
        }
        private void OnRoomStateChange()
        {
            switch (_houseManager.RoomState.Value)
            {
                case RoomState.OutSideMenu:
                    _gameManager.GameState.Value = Menu.Tools.GameState.OutSideMenu;
                    break;
                case RoomState.Kitchen:
                    InstantiatePartner();
                    SetConsumedFoodText();
                    break;
            }
        }
        private void OnActionStateChange()
        {
            switch (_houseManager.ActionState.Value)
            {
                case ActionState.InstantiateParent:
                    InstantiatePartner();
                    break;
                case ActionState.InstantiateChild:
                    InstantiateChild();
                    break;
            }
        }
        private void InstantiatePartner()
        {
            if (!_houseManager.HavePartner)
                return;

            if (_houseManager.PartnerProfile.IsNew)
            {
                _houseManager.PartnerProfile.IsNew = false;
                _houseManager.PartnerProfile.HeadMaterial = _houseManager.HeadMaterial[Random.Range(0, _houseManager.HeadMaterial.Count)];
                _houseManager.PartnerProfile.BodyMaterial = _houseManager.BodyMaterial[Random.Range(0, _houseManager.BodyMaterial.Count)];
            }

            if (_partner == null)
            {
                _partner = ResourcesLoader.InstantiateAndGetObject<PartnerView>(_houseManager.PathForObjects + _houseManager.PartnerPath);
                _partner.Initialize();
            }

            _houseManager.ActionState.Value = ActionState.Null;
        }
        private void InstantiateChild()
        {
            if (!_houseManager.HavePartner && !_houseManager.HaveChild)
                return;

            for (int i = 0; i < _houseManager.CountOfChildren; i++)
            {
                if (_houseManager.ChildrensProfile[i].IsNew)
                {
                    _houseManager.ChildrensProfile[i].IsNew = false;
                    _houseManager.ChildrensProfile[i].ID = _houseManager.ChildrenID;
                    _houseManager.ChildrensProfile[i].HeadMaterial = _houseManager.HeadMaterial[Random.Range(0, _houseManager.HeadMaterial.Count)];
                    _houseManager.ChildrensProfile[i].BodyMaterial = _houseManager.BodyMaterial[Random.Range(0, _houseManager.BodyMaterial.Count)];
                }
            }

            for (int i = _childrens.Count; i < _houseManager.CountOfChildren; i++)
            {
                if (_childrens.Count != _houseManager.CountOfChildren)
                {
                    ChildView child = ResourcesLoader.InstantiateAndGetObject<ChildView>(_houseManager.PathForObjects + _houseManager.ChildrensPath);
                    _childrens.Add(child);
                }
                _childrens[i].Initialize(i);
            }

            _houseManager.ActionState.Value = ActionState.Null;
        }
        private void SaveData()
        {
            switch (_houseManager.SaveLoadState.Value)
            {
                case SaveState.SaveLarvaData:
                    Saver.SaveLarvaData(_saveLoadManager, _larvaProfile);
                    _houseManager.SaveLoadState.Value = SaveState.Null;
                    break;
                case SaveState.SaveHouseData:
                    Saver.SaveHouseData(_saveLoadManager, _houseManager);
                    _houseManager.SaveLoadState.Value = SaveState.Null;
                    break;
            }
        }
        private void SetConsumedFoodText()
        {
            if (_houseManager.GameMode == GameMode.Real)
                _familyNeedFood3dTex.text = $"{_localizationManager.HouseTable.Value.GetEntry(FamilyEat.ToString())?.GetLocalizedString()} " +
                                            $"{_houseManager.ConsumedFood}";
        }
        public void EnterToHouse() => _houseManager.RoomState.Value = RoomState.MainHall;
    }
}