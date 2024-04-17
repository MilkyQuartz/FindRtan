using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioSource;
    public AudioClip clip;

    public int stageNum=0;
    public GameObject stageNumObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(stageNumObject);
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
        audioSource.Play();
    }

}
