using Larva.Tools;
using Larva.Game.Data;
using Larva.Game.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Larva.Game.Core
{
    public class LarvaSoundController : ObjectsDisposer
    {
        private readonly LarvaManager _larvaManager;
        private List<AudioSource> _eatSounds;

        public LarvaSoundController(LarvaManager larvaManager, List<AudioSource> eatSounds)
        {
            _larvaManager = larvaManager;
            _eatSounds = eatSounds;
            _larvaManager.State.SubscribeOnChange(OnStateChange);
        }
        protected override void OnDispose()
        {
            _larvaManager.State.UnSubscribeOnChange(OnStateChange);

            base.OnDispose();
        }
        private void OnStateChange()
        {
            switch (_larvaManager.State.Value)
            {
                case LarvaState.EatGoodFood:
                    _eatSounds[Random.Range(0, _eatSounds.Count)].Play();
                    break;
                case LarvaState.EatBadFood:
                    _eatSounds[Random.Range(0, _eatSounds.Count)].Play();
                    break;
            }
        }
    }
}