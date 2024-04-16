using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    public Card firstCard;
    public Card secondCard;
    public Text timeTxt;
    public GameObject endTxt;
    AudioSource audioSource;
    Text endText; //endText ������Ʈ�� ���� ����
    public Text tryTxt;  // ī��Ʈǥ���� �ؽ�Ʈ �����
    public Text falseTryTxt; // �ð� �߰� �ؽ�Ʈ ����
    public AudioClip clip; //����
    public AudioClip clip2; //����
    float time = 0.0f;
    public int cardCount = 0;
    public int tryCount = 0;  // ��Ī �õ� ī��Ʈ
    public Card thirdCard;
    public Card fourthCard;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
        // endText ������Ʈ �ҷ���
        endText = endTxt.GetComponent<Text>();
        falseTryTxt = falseTryTxt.GetComponent<Text>();
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

            if(cardCount == 0)
            {
                //��Ī ���� ��, ������ �̸� ǥ�� / ���� �� ���� ǥ�� (���� �� ��! ��Ÿ���� ��ó��)
                endText.text = "<����>\n������\n<����>\n�յ���\n����ȣ\n������\n������";
                endText.fontSize = 80;
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
            Debug.Log("���߱� ���� 1�� �߰�");
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
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // r,g,b������ ������ ������ �̿������� ������ �����Ͽ� ���� �����ϰ� �����.
            yield return null;
        }

        text.gameObject.SetActive(false); // �ݺ����� �������� ��Ȱ��ȭ
    }



}
