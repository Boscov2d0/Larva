using Larva.Tools;
using UnityEngine;
using System.Collections.Generic;

namespace Larva.Menu.Data
{
    [CreateAssetMenu(fileName = nameof(LocalizationManager), menuName = "Managers/Menu/LocalizationManager")]
    public class LocalizationManager : ScriptableObject
    {
        [HideInInspector] public SubscriptionProperty<string> Language = new SubscriptionProperty<string>();

        public Dictionary<string, string> LocalizedMenuText;
        public Dictionary<string, string> LocalizedSettingsText;

        [SerializeField] public string MenuTextsPath;
        [SerializeField] public string SettingsTextsPath;
    }
}