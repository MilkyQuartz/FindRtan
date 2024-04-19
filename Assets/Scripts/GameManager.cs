using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerPrefab;
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

        if (timeTxt != null)
        {
            timeRect = timeTxt.GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        time += Time.deltaTime;

        // 시간 텍스트가 null이 아닌지 확인
        if (timeTxt != null)
        {
            timeTxt.text = time.ToString("N2");
        }

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
                // 모든 카드를 맞추었으므로 클리어 난이도를 +1 증가시키고 현재 난이도도 증가시킴
                int currentDifficulty = PlayerPrefs.GetInt("Difficulty", 1);
                int nextDifficulty = currentDifficulty + 1;

                // 다음 난이도가 5를 초과하지 않으면 현재 난이도와 클리어 난이도를 갱신하고 메인 씬으로 이동
                if (nextDifficulty <= 5)
                {
                    // 클리어된 난이도가 최대 난이도보다 높은지 확인하여 업데이트
                    int highDifficulty = PlayerPrefs.GetInt("HighDifficulty", 1);
                    if (currentDifficulty > highDifficulty)
                    {
                        PlayerPrefs.SetInt("HighDifficulty", currentDifficulty);
                    }

                    PlayerPrefs.SetInt("Difficulty", nextDifficulty);
                    PlayerPrefs.SetInt("Round" + nextDifficulty + "Cleared", 1); // 다음 난이도의 클리어 상태를 true로 설정
                    endPanel.SetActive(true);
                }
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
            Debug.Log("틀렸습니다 1초 추가");
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
