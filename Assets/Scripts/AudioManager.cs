    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioSource;
    public AudioClip clip;

    public int stageNum=0;  //ìŠ¤íƒ€íŠ¸ì”¬ì—ì„œ ë‚œì´ë„ ê°’ ë„˜ì–´ì˜¬ê³³

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
    // ½ÃÀÛ¶§ ¹è°æÀ½¾Ç Ãâ·ÂÇØÁÖ´Â ¸Ş¼­µå
    public void StartMusic()
    {
        audioSource.Play();
    }
    // ±âÁ¸ ¹è°æÀ½¾ÇÀ» ¸ØÃß°ÔÇÏ´Â ¸Ş¼­µå
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
