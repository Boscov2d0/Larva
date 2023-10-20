using Larva.Core;
using Larva.Data;
using Larva.House.Data;
using Larva.House.Tools;
using Larva.House.UI.View;
using Larva.Tools;
using System.Collections.Generic;
using UnityEngine;

using static Larva.Tools.AudioKeys;
using static Larva.Tools.Keys;

namespace Larva.House.UI.Controller
{
    public class WardrobeUIController : ObjectsDisposer
    {
        private readonly LarvaProfile _larvaProfile;
        private readonly HouseManager _houseManager;
        private readonly AudioManager _audioManager;

        private LarvaView _menuLarvaView;
        private Renderer _larvaHead;
        private List<Renderer> _larvaBody = new List<Renderer>();

        public WardrobeUIController(LarvaProfile larvaProfile, HouseManager houseManager, 
                                    UIManager uiManager, AudioManager audioManager)
        {
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

            _houseManager.SaveLoadState.Value = SaveState.SaveLarvaData;
        }
        private void BackToLarvaHouse()
        {
            ResetSkin();
            _houseManager.ActionState.Value = HouseState.ActionState.CloseWardrobe;
            _houseManager.RoomState.Value = HouseState.RoomState.Bedroom;
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