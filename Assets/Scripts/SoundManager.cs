using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("AudioSource")]
    public AudioSource bgmAudioSource;
    public AudioSource buttonAudioSource;
    public AudioSource playAudioSource;

    [Header("AudioClip")]
    public AudioClip lobySceneBGM;
    public AudioClip drawSceneBGM;
    public AudioClip mainSceneBGM;
    public AudioClip buttonClickClip;
    public AudioClip nextLevelClip;
    public AudioClip instantCircleClip;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
