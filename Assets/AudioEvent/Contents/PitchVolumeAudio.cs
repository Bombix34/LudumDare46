using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="FafaTools/Audio/PitchVolumeAudio")]
public class PitchVolumeAudio : AudioEvent {
	public RangedFloat volume;

	[MinMaxRange(0,2)]
	public RangedFloat pitch;
	public override void Play(AudioSource source)
    {
        AudioClip toPlay = clips[Random.Range(0, clips.Length)];
        source.outputAudioMixerGroup = audioMixerGroup;
        source.volume = Random.Range(volume.minValue, volume.maxValue);
        source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
        if (loop)
        {
            source.loop = loop;
            source.clip = toPlay;
            source.Play();
        }
        else
        {
            source.loop = false;
            source.PlayOneShot(toPlay);
        }
    }
}
