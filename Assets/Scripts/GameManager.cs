using System;
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
    public GameObject endTxt;
    AudioSource audioSource;
    Text endText; //endText ������Ʈ�� ���� ����
    public Text tryTxt;  // ī��Ʈǥ���� �ؽ�Ʈ �����
    public Text falseTryTxt; // �ð� �߰� �ؽ�Ʈ ����
    public AudioClip clip; //����
    public AudioClip clip2; //����
    public AudioClip clip3; // 20�� ���� �������
    public float time = 0.0f;
    public int score = 0;
    public int cardCount = 0;
    public int tryCount = 0;  // ��Ī �õ� ī��Ʈ
    public Card thirdCard;
    public Card fourthCard;
    bool isPlay = true;


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
        // endText ������Ʈ �ҷ���
        endText = endTxt.GetComponent<Text>();
        falseTryTxt = falseTryTxt.GetComponent<Text>();
        timeRect = timeTxt.GetComponent<RectTransform>();
        tryCount = 0;  // ���ӽ���,����۽� ī��Ʈ �ʱ�ȭ
    }

    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        tryTxt.text = tryCount.ToString();  // ����� �ؽ�Ʈ�� ǥ��
        if (time >= 30.0f)
        {
            // endTxt�� �⺻�� ���з� �����ϰ� ��Ī ���н� ���а� ������ �Ѵ�.
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
            audioSource.Stop();
        }
        if (time >= 20.0f)
        {
            // ���󺯰�
            timeTxt.color = Color.red;
            // ��Ʈ ������ 1 -> 1.1 ������� ����
            timeTxt.fontSize = (int)(70 * 1.2f);
            // �̰� ���� ����µ� �𸣰ھ��..
            timeRect.sizeDelta = new Vector2(timeTxt.preferredWidth, timeTxt.preferredHeight);
            //timeTxt.transform.localScale = Vector3.one * 1.1f;
        }
        // 20�ʿ� ������� �ٲ�� �κ�
        if (time >= 20.0f && isPlay)
        {
            // ���� ������� ����
            AudioManager.instance.StopMusic();
            // 20�� ���� ���ο� ������� ���
            audioSource.clip = clip3;
            audioSource.Play();
            isPlay = false;
        }
    }


    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip); // ���� �� ���� ȿ����
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            score+=10; // 맞추면 10점

            if(cardCount == 0)
            {
                //��Ī ���� ��, ������ �̸� ǥ�� / ���� �� ���� ǥ�� (���� �� ��! ��Ÿ���� ��ó��)
                //endText.text = "<����>\n������\n<����>\n�յ���\n����ȣ\n������\n������";
                //endText.fontSize = 80;
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            audioSource.PlayOneShot(clip2); // ���� �� ���� ȿ���� clip2 ���
            firstCard.ChangeColor(Color.gray);
            secondCard.ChangeColor(Color.gray);
            firstCard.CloseCard();
            secondCard.CloseCard();
            StartCoroutine(FalseTryTime(falseTryTxt, 0.5f)); //0.5f�� ���������� ��Ȱ��ȭ �ɶ������� �ð��� ������ �Ű�����
            time += 1.0f;
            Debug.Log("틀렷습니다");
        }
        if (thirdCard != null && fourthCard != null)
        {
            thirdCard.ChangeColor(Color.white);
            fourthCard.ChangeColor(Color.white);
        }
        //1,2Card�� ����� ���� ���������� �ٲٰų� �ٸ������� ����
        thirdCard = firstCard; 
        fourthCard = secondCard;
        firstCard = null;
        secondCard = null;
        tryCount++;  // ��Ī�Լ� �ҷ��ö����� ī��Ʈ +1
    }

    // �ð� �߰� ȿ�� �Լ�(����������)
    IEnumerator FalseTryTime(Text text, float fadeTime)
    {
        text.gameObject.SetActive(true);
        Color originalColor = text.color;  // �ؽ�Ʈ�� �ʱ� ����

        float timer = 0f;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeTime); // Lerp�� �������� �Լ� (�߰����� ����Ͽ� �ε巯�� �̵��� ����)
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // r,g,b������ ������ ������ �̿������� �������� �����Ͽ� ���� �����ϰ� �����.
            yield return null;
        }

        text.gameObject.SetActive(false); // �ݺ����� �������� ��Ȱ��ȭ
    }



}
