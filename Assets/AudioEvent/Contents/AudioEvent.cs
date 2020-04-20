using UnityEngine;
using UnityEngine.Audio;

public abstract class AudioEvent : ScriptableObject {

    public AudioMixerGroup audioMixerGroup;
    public bool loop;
    public AudioClip[] clips;
    public abstract void Play(AudioSource sourceGiven = null);
}
