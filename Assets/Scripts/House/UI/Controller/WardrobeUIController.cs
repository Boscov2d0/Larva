using Larva.Core;
using Larva.Data;
using Larva.House.Data;
using Larva.House.Tools;
using Larva.House.UI.View;
using Larva.Tools;
using System.Collections.Generic;
using UnityEngine;

using static Larva.Tools.AudioKeys;

namespace Larva.House.UI.Controller
{
    public class WardrobeUIController : ObjectsDisposer
    {
        private readonly SaveLoadManager _saveLoadManager;
        private readonly LarvaProfile _larvaProfile;
        private readonly HouseManager _houseManager;
        private readonly AudioManager _audioManager;

        private LarvaView _menuLarvaView;
        private Renderer _larvaHead;
        private List<Renderer> _larvaBody = new List<Renderer>();

        public WardrobeUIController(SaveLoadManager saveLoadManager, LarvaProfile larvaProfile, HouseManager houseManager, UIManager uiManager, AudioManager audioManager)
        {
            _saveLoadManager = saveLoadManager;
            _larvaProfile = larvaProfile;
            _houseManager = houseManager;
            _audioManager = audioManager;

            _menuLarvaView = _houseManager.MenuLarva;
            _larvaHead = _menuLarvaView.Head;

            _larvaBody.Clear();
            for (int i = 0; i < _menuLarvaView.Body.Count; i++)
                _larvaBody.Add(_menuLarvaView.Body[i]);

            _houseManager.ChoosedHeadSkin.SubscribeOnChange(SetHeadSkinOnLarva);
            _houseManager.ChoosedBodySkin.SubscribeOnChange(SetBodySkinOnLarva);

            WardrobeCanvasView changeSkinView = ResourcesLoader.InstantiateAndGetObject<WardrobeCanvasView>(uiManager.PathForUIObjects + uiManager.WardrobeCanvasPath);
            AddGameObject(changeSkinView.gameObject);
            changeSkinView.Initialize(ChangeSkin, BackToLarvaHouse);
        }
        protected override void OnDispose()
        {
            _houseManager.ChoosedHeadSkin.UnSubscribeOnChange(SetHeadSkinOnLarva);
            _houseManager.ChoosedBodySkin.UnSubscribeOnChange(SetBodySkinOnLarva);
            _larvaBody.Clear();
            base.OnDispose();
        }
        private void ChangeSkin()
        {
            if (_houseManager.ChoosedHeadSkin.Value)
                _larvaProfile.HeadSkin = _houseManager.ChoosedHeadSkin.Value;
            if (_houseManager.ChoosedBodySkin.Value)
                _larvaProfile.BodySkin = _houseManager.ChoosedBodySkin.Value;

            Saver.SaveLarvaData(_saveLoadManager, _larvaProfile);
        }
        private void BackToLarvaHouse()
        {
            ResetSkin();
            _houseManager.HouseState.Value = HouseState.Bedroom;
            _audioManager.State.Value = AudioStates.Button;
            Dispose();
        }
        private void SetHeadSkinOnLarva()
        {
            _larvaHead.material = _houseManager.ChoosedHeadSkin.Value;
            _audioManager.State.Value = AudioStates.ButtonApply;
        }
        private void SetBodySkinOnLarva()
        {
            for (int i = 0; i < _larvaBody.Count; i++)
            {
                _larvaBody[i].material = _houseManager.ChoosedBodySkin.Value;
            }
        }
        private void ResetSkin()
        {
            _larvaHead.material = _larvaProfile.HeadSkin;
            for (int i = 0; i < _larvaBody.Count; i++)
            {
                _larvaBody[i].material = _larvaProfile.BodySkin;
            }
        }
    }
}