using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource[] audiosources;
    public PitchVolumeAudio plantGrowStartSound;                  //0
    public PitchVolumeAudio plantGrowEndSound;                      //1
    public PitchVolumeAudio dashSound;                               //2
    public PitchVolumeAudio jumpSound;                              //3
    public PitchVolumeAudio filetSound;                             //4
    public PitchVolumeAudio jumpingLeafSound;                //5
    public PitchVolumeAudio doubleJumpSound;                   //6
    public PitchVolumeAudio feedingSound;                  //7

    void Start()
    {
        for (int i = 0; i < audiosources.Length; i++)
        {
            audiosources[i].loop = false;
        }
    }

    public void PlaySound(int sound)
    {
        switch(sound)
        {
            case 0:
                plantGrowStartSound.Play(GetAudioSourceAvailable());
                break;
            case 1:
                plantGrowEndSound.Play(GetAudioSourceAvailable());
                break;
            case 2:
                dashSound.Play(GetAudioSourceAvailable());
                break;
            case 3:
                jumpSound.Play(GetAudioSourceAvailable());
                break;
            case 4:
                filetSound.Play(GetAudioSourceAvailable());
                break;
            case 5:
                jumpingLeafSound.Play(GetAudioSourceAvailable());
                break;
            case 6:
                doubleJumpSound.Play(GetAudioSourceAvailable());
                break;
            case 7:
                feedingSound.Play(GetAudioSourceAvailable());
                break;
        }
    }

    public void GetAudioSourceAvailable(AudioClip clip)
    {
        for (int i = 0; i < audiosources.Length; i++)
        {
            if (!audiosources[i].isPlaying)
            {
                audiosources[i].loop = false;
                audiosources[i].clip = clip;
                audiosources[i].Play();
                return;
            }
        }
    }

    public AudioSource GetAudioSourceAvailable()
    {
        for (int i = 0; i < audiosources.Length; i++)
        {
            if (!audiosources[i].isPlaying)
            {
                return audiosources[i];
            }
        }
        return null;
    }
}
