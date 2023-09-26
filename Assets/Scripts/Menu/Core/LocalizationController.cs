using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Larva.Tools;
using Larva.Menu.Tools;
using Larva.Menu.Data;
using Larva.Data;

namespace Larva.Menu.Core
{
    public class LocalizationController : ObjectsDisposer
    {
        private readonly LocalizationManager _localizationManager;
        private readonly LarvaProfile _larvaProfile;

        public LocalizationController(LocalizationManager localizationManager, LarvaProfile larvaProfile)
        {
            _localizationManager = localizationManager;
            _larvaProfile = larvaProfile;

            _localizationManager.Language.SubscribeOnChange(LoadMainMenuLocalizedText);
            _localizationManager.Language.SubscribeOnChange(LoadSettingsLocalizedText);

            SetLanguage();
            _localizationManager.Language.SubscribeOnChange(SetLanguage);      

            LoadMainMenuLocalizedText();
            LoadSettingsLocalizedText();
        }
        protected override void OnDispose()
        {
            _localizationManager.Language.UnSubscribeOnChange(LoadMainMenuLocalizedText);
            _localizationManager.Language.UnSubscribeOnChange(LoadSettingsLocalizedText);

            _localizationManager.Language.UnSubscribeOnChange(SetLanguage);
        }
        private void SetLanguage()
        {
            if (!string.IsNullOrEmpty(_localizationManager.Language.Value))
            {
                _larvaProfile.Language = _localizationManager.Language.Value;
                return;
            }

            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
                _localizationManager.Language.Value = LanguageKeys.ru_RU.ToString();
            else if (Application.systemLanguage == SystemLanguage.Chinese || Application.systemLanguage == SystemLanguage.ChineseSimplified || Application.systemLanguage == SystemLanguage.ChineseTraditional)
                _localizationManager.Language.Value = LanguageKeys.zh_ZH.ToString();
            else
                _localizationManager.Language.Value = LanguageKeys.en_US.ToString();

            _larvaProfile.Language = _localizationManager.Language.Value;
        }
        public void LoadMainMenuLocalizedText()
        {
            _localizationManager.LocalizedMenuText = new Dictionary<string, string>();
            LoadLocalizedText(_localizationManager.LocalizedMenuText, _localizationManager.MenuTextsPath);
        }
        public void LoadSettingsLocalizedText()
        {
            _localizationManager.LocalizedSettingsText = new Dictionary<string, string>();
            LoadLocalizedText(_localizationManager.LocalizedSettingsText, _localizationManager.SettingsTextsPath);
        }
        public void LoadLocalizedText(Dictionary<string, string> text, string path)
        {
            string dataAsJson;
            string fullPath = Application.streamingAssetsPath + path + _localizationManager.Language.Value + ".json";

            if (Application.platform == RuntimePlatform.Android)
            {/*
                WWW reader = new(path);
                while (!reader.isDone) { }

                dataAsJson = reader.text;
                */
                UnityWebRequest reader = new(fullPath);
                while (!reader.isDone) { }

                dataAsJson = reader.result.ToString();

            }
            else
                dataAsJson = File.ReadAllText(fullPath);

            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
                text.Add(loadedData.items[i].key, loadedData.items[i].value);
        }
    }
}