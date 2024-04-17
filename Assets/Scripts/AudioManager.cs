    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioSource;
    public AudioClip clip;

    public int stageNum=0;  //스타트씬에서 난이도 값 넘어올곳

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
    }
    // ���۶� ������� ������ִ� �޼���
    public void StartMusic()
    {
        audioSource.Play();
    }
    // ���� ��������� ���߰��ϴ� �޼���
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
