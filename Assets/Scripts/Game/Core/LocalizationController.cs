using Larva.Data;
using Larva.Tools;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace Larva.Game.Core
{
    public class LocalizationController : ObjectsDisposer
    {
        private readonly LocalizationManager _localizationManager;

        public LocalizationController(LocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;

            LocalizationSettings.SelectedLocaleChanged += OnChangedLocale;

            GetGameTable();
        }
        protected override void OnDispose()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnChangedLocale;
        }
        private void OnChangedLocale(Locale locale)
        {
            GetGameTable();
        }
        private void GetGameTable()
        {
            StringTable loadingOperation = LocalizationSettings.StringDatabase.GetTable(Keys.TabelsNameKeys.Game.ToString());
            _localizationManager.GameTable.Value = loadingOperation;
        }
    }
}