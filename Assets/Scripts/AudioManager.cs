    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioSource;
    public AudioClip clip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;        
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = this.clip;
        StartMusic();
    }
    // 시작때 배경음악 출력해주는 메서드
    public void StartMusic()
    {
        audioSource.Play();
    }
    // 기존 배경음악을 멈추게하는 메서드
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
