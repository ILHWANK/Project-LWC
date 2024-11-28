using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;


    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channelCount;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum SFXType
    {
        Walk,
        Click,
        Success,
        Fail,
        Interaction
    }

    private void Awake()
    {
        instance = this;
        Init();
    }

    // SoundPlayer Init
    private void Init()
    {
        // BGM
        var bgmObject = new GameObject("BGMObject");
        bgmObject.transform.parent = transform;

        bgmPlayer = bgmObject.AddComponent<AudioSource>();

        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        // SFX
        var sfxObject = new GameObject("SFXObject");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channelCount];

        for (int i = 0; i < channelCount; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].loop = false;
            sfxPlayers[i].volume = sfxVolume;
        }
    }

    public void BGMPlay(bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer?.Play();
        }
        else
        {
            bgmPlayer?.Stop();
        }
    }

    public void SFXPlay(SFXType sfxType)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfxType];
            sfxPlayers[loopIndex].Play();

            break;
        }
    }
}
