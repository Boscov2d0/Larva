using Larva.Tools;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

namespace Larva.Data
{
    [CreateAssetMenu(fileName = nameof(LocalizationManager), menuName = "Managers/Menu/LocalizationManager")]
    public class LocalizationManager : ScriptableObject
    {
        [HideInInspector] public Locale Language;

        public SubscriptionProperty<StringTable> MenuTable = new SubscriptionProperty<StringTable>();
        public SubscriptionProperty<StringTable> SettingsTable = new SubscriptionProperty<StringTable>();
        public SubscriptionProperty<StringTable> GameTable = new SubscriptionProperty<StringTable>();
    }
}