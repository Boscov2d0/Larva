using Larva.Tools;
using UnityEngine;
using System.Collections.Generic;

namespace Larva.Game.Data
{
    [CreateAssetMenu(fileName = nameof(LocalizationManager), menuName = "Managers/Game/LocalizationManager")]
    public class LocalizationManager : ScriptableObject
    {
        [HideInInspector] public SubscriptionProperty<string> Language = new SubscriptionProperty<string>();

        public Dictionary<string, string> LocalizedGameText;

        [SerializeField] public string GameTextsPath;
    }
}