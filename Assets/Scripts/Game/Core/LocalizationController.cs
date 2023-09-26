using Larva.Tools;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Larva.Game.Data;
using Larva.Data;

namespace Larva.Game.Core
{
    public class LocalizationController : ObjectsDisposer
    {
        private readonly LocalizationManager _localizationManager;

        public LocalizationController(LocalizationManager localizationManager, LarvaProfile larvaProfile)
        {
            _localizationManager = localizationManager;
            _localizationManager.Language.SubscribeOnChange(LoadGameLocalizedText);
            _localizationManager.Language.Value = larvaProfile.Language;
            LoadGameLocalizedText();
        }
        protected override void OnDispose()
        {
            _localizationManager.Language.UnSubscribeOnChange(LoadGameLocalizedText);
        }
        public void LoadGameLocalizedText()
        {
            _localizationManager.LocalizedGameText = new Dictionary<string, string>();
            LoadLocalizedText(_localizationManager.LocalizedGameText, _localizationManager.GameTextsPath);
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