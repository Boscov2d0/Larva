using Larva.Tools;
using Larva.Menu.Tools;
using UnityEngine;
using UnityEngine.Audio;

namespace Larva.Menu.Data
{
    [CreateAssetMenu(fileName = nameof(AudioManager), menuName = "Managers/" + nameof(AudioManager))]
    public class AudioManager : ScriptableObject
    {
        [field: SerializeField] public float SoundsVolume = 1f;
        [field: SerializeField] public float MusicVolume = 1f;
        [field: SerializeField] public AudioMixer AudioMixer;
        [field: SerializeField] public SubscriptionProperty<AudioStates> State = new SubscriptionProperty<AudioStates>();
    }
}