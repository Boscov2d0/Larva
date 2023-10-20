using Larva.Data;
using Larva.House.Core;
using Larva.House.Data;
using Larva.House.Tools;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using static Larva.Tools.LocalizationTextKeys.LocalizationWardrobeTextKeys;

namespace Larva.House.UI.View
{
    public class WardrobeCanvasView : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private HouseManager _houseManager; 
        [SerializeField] private Button _openHeadSkinButton;
        [SerializeField] private Button _openBodySkinButton;
        [SerializeField] private Button _changeSkinButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private GameObject _headSkinsPanel;
        [SerializeField] private GameObject _bodySkinsPanel;
        [SerializeField] private List<SkinButton> _headSkinsButtons;
        [SerializeField] private List<SkinButton> _bodySkinsButtons;

        [SerializeField] private Text _headText;
        [SerializeField] private Text _bodyText;
        [SerializeField] private Text _changeText;
        [SerializeField] private Text _closeText;


        private UnityAction _changeSkin;
        private UnityAction _backToLarvaHouse;

        public void Initialize(UnityAction changeSkin, UnityAction backToLarvaHouse)
        {
            _changeSkin = changeSkin;
            _backToLarvaHouse = backToLarvaHouse;

            _openHeadSkinButton.onClick.AddListener(OpenHeadSkinPanel);
            _openBodySkinButton.onClick.AddListener(OpenBodySkinPanel);
            _changeSkinButton.onClick.AddListener(_changeSkin);
            _backButton.onClick.AddListener(_backToLarvaHouse);

            _openHeadSkinButton.Select();

            for (int i = 0; i < _headSkinsButtons.Count; i++)
            {
                _headSkinsButtons[i].Initialize(_houseManager, SkinType.Head);
            }
            for (int i = 0; i < _bodySkinsButtons.Count; i++)
            {
                _bodySkinsButtons[i].Initialize(_houseManager, SkinType.Body);
            }

            _localizationManager.HouseTable.SubscribeOnChange(TranslateText);
            TranslateText();
        }
        private void OnDestroy()
        {
            _localizationManager.HouseTable.UnSubscribeOnChange(TranslateText);

            _openHeadSkinButton.onClick.RemoveListener(OpenHeadSkinPanel);
            _openBodySkinButton.onClick.RemoveListener(OpenBodySkinPanel);
            _changeSkinButton.onClick.RemoveListener(_changeSkin);
            _backButton.onClick.RemoveListener(_backToLarvaHouse);
        }
        private void TranslateText()
        {
            _headText.text = _localizationManager.HouseTable.Value.GetEntry(Head.ToString())?.GetLocalizedString();
            _bodyText.text = _localizationManager.HouseTable.Value.GetEntry(Body.ToString())?.GetLocalizedString();
            _changeText.text = _localizationManager.HouseTable.Value.GetEntry(ChangeSkin.ToString())?.GetLocalizedString();
            _closeText.text = _localizationManager.HouseTable.Value.GetEntry(Close.ToString())?.GetLocalizedString();
        }
        private void OpenHeadSkinPanel()
        {
            _headSkinsPanel.SetActive(true);
            _bodySkinsPanel.SetActive(false);
        }
        private void OpenBodySkinPanel()
        {
            _headSkinsPanel.SetActive(false);
            _bodySkinsPanel.SetActive(true);
        }
    }
}