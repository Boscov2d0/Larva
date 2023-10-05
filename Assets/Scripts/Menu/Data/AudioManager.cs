using Larva.Tools;
using Larva.Menu.Tools;
using UnityEngine;
using UnityEngine.Audio;

namespace Larva.Menu.Data
{
    [CreateAssetMenu(fileName = nameof(AudioManager), menuName = "Managers/" + nameof(AudioManager))]
    public class AudioManager : ScriptableObject
    {
        [field: SerializeField] public AudioMixer AudioMixer;
        [HideInInspector] public float SoundsVolume = 1f;
        [HideInInspector] public float MusicVolume = 1f;
        [HideInInspector] public SubscriptionProperty<AudioStates> State = new SubscriptionProperty<AudioStates>();
    }
}