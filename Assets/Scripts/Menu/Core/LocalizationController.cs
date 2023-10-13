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

            LocalizationSettings.SelectedLocale = _localizationManager.Language;
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
            GetHouseTable();
        }
        private void GetMenuTable() 
        {
            StringTable loadingOperation = LocalizationSettings.StringDatabase.GetTable(Keys.TabelsNameKeys.Menu.ToString());
            _localizationManager.MenuTable.Value = loadingOperation;
        }
        private void GetSettingsTable()
        {
            StringTable loadingOperation = LocalizationSettings.StringDatabase.GetTable(Keys.TabelsNameKeys.Settings.ToString());
            _localizationManager.SettingsTable.Value = loadingOperation;
        }
        private void GetHouseTable()
        {
            StringTable loadingOperation = LocalizationSettings.StringDatabase.GetTable(Keys.TabelsNameKeys.House.ToString());
            _localizationManager.HouseTable.Value = loadingOperation;
        }
    }
}