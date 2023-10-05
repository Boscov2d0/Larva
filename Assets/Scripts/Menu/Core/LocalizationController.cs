using Larva.Data;
using Larva.Tools;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization;

namespace Larva.Menu.Core
{
    public class LocalizationController : ObjectsDisposer
    {
        private readonly LocalizationManager _localizationManager;

        public LocalizationController(LocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;

            LocalizationSettings.SelectedLocaleChanged += OnChangedLocale;

            GetTables();
        }
        protected override void OnDispose()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnChangedLocale;
        }
        private void OnChangedLocale(Locale locale)
        {
            GetTables();
        }
        private void GetTables() 
        {
            GetMenuTable();
            GetSettingsTable();
            GetGameTable();
        }
        private void GetMenuTable() 
        {
            StringTable loadingOperation = LocalizationSettings.StringDatabase.GetTable("Menu");
            _localizationManager.MenuTable.Value = loadingOperation;
        }
        private void GetSettingsTable()
        {
            StringTable loadingOperation = LocalizationSettings.StringDatabase.GetTable("Settings");
            _localizationManager.SettingsTable.Value = loadingOperation;
        }
        private void GetGameTable()
        {
            StringTable loadingOperation = LocalizationSettings.StringDatabase.GetTable("Game");
            _localizationManager.GameTable.Value = loadingOperation;
        }
    }
}