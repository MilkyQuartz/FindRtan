using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Card firstCard;
    public Card secondCard;
    public Text timeTxt;
    private RectTransform timeRect;
    public GameObject endPanel;
    AudioSource audioSource;
    public Text tryTxt;
    public Text falseTryTxt;
    public AudioClip clip;
    public AudioClip clip2;
    public AudioClip clip3;
    public float time = 0.0f;
    public int score = 0;
    public int tryCount = 0;
    public Card thirdCard;
    public Card fourthCard;
    bool isPlay = true;
    public int cardCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
        falseTryTxt = falseTryTxt.GetComponent<Text>();
        timeRect = timeTxt.GetComponent<RectTransform>();
        tryCount = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        tryTxt.text = tryCount.ToString();
        if (time >= 30.0f)
        {
            Time.timeScale = 0.0f;
            audioSource.Stop();
            endPanel.SetActive(true);
        }
        if (time >= 20.0f)
        {
            timeTxt.color = Color.red;
            timeTxt.fontSize = (int)(70 * 1.2f);
            timeRect.sizeDelta = new Vector2(timeTxt.preferredWidth, timeTxt.preferredHeight);
        }
        if (time >= 20.0f && isPlay)
        {
            AudioManager.instance.StopMusic();
            audioSource.clip = clip3;
            audioSource.Play();
            isPlay = false;
        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            GameManager.instance.cardCount -= 2;
            score += 10;

            if (GameManager.instance.cardCount == 0)
            {
                Time.timeScale = 0.0f;
                endPanel.SetActive(true);
            }
        }
        else
        {
            audioSource.PlayOneShot(clip2);
            firstCard.ChangeColor(Color.gray);
            secondCard.ChangeColor(Color.gray);
            firstCard.CloseCard();
            secondCard.CloseCard();
            StartCoroutine(FalseTryTime(falseTryTxt, 0.5f));
            time += 1.0f;
            Debug.Log("틀렸습니다. 1초 추가!");
        }
        if (thirdCard != null && fourthCard != null)
        {
            thirdCard.ChangeColor(Color.white);
            fourthCard.ChangeColor(Color.white);
        }
        thirdCard = firstCard;
        fourthCard = secondCard;
        firstCard = null;
        secondCard = null;
        tryCount++;
    }

    IEnumerator FalseTryTime(Text text, float fadeTime)
    {
        text.gameObject.SetActive(true);
        Color originalColor = text.color;

        float timer = 0f;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeTime);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        text.gameObject.SetActive(false);
    }
}
