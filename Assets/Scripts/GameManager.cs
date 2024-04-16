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
    Text endText; //endText 컴포넌트를 받을 변수
    public Text tryTxt;  // 카운트표기할 텍스트 연결부
    public Text falseTryTxt; // 시간 추가 텍스트 변수
    public AudioClip clip; //성공
    public AudioClip clip2; //실패
    float time = 0.0f;
    public int cardCount = 0;
    public int tryCount = 0;  // 매칭 시도 카운트
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
        // endText 컴포넌트 불러옴
        endText = endTxt.GetComponent<Text>();
        falseTryTxt = falseTryTxt.GetComponent<Text>();
        tryCount = 0;  // 게임시작,재시작시 카운트 초기화
    }

    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        tryTxt.text = tryCount.ToString();  // 연결된 텍스트에 표시
        if (time >= 30.0f)
        {
            // endTxt의 기본을 실패로 설정하고 매칭 실패시 실패가 나오게 한다.
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }


    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip); // 성공 시 성공 효과음
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;

            if(cardCount == 0)
            {
                //매칭 성공 시, 팀원의 이름 표시 / 실패 시 실패 표시 (종료 시 끝! 나타나는 것처럼)
                endText.text = "<팀장>\n유수정\n<팀원>\n손두혁\n이정호\n안지수\n권지민";
                endText.fontSize = 80;
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            audioSource.PlayOneShot(clip2); // 실패 시 실패 효과음 clip2 재생
            firstCard.ChangeColor(Color.gray);
            secondCard.ChangeColor(Color.gray);
            firstCard.CloseCard();
            secondCard.CloseCard();
            StartCoroutine(FalseTryTime(falseTryTxt, 0.5f)); //0.5f는 투명해지고 비활성화 될때까지의 시간을 보내는 매개변수
            time += 1.0f;
            Debug.Log("맞추기 실패 1초 추가");
        }
        if (thirdCard != null && fourthCard != null)
        {
            thirdCard.ChangeColor(Color.white);
            fourthCard.ChangeColor(Color.white);
        }
        //1,2Card에 저장된 색을 원래색으로 바꾸거나 다른색으로 저장
        thirdCard = firstCard; 
        fourthCard = secondCard;
        firstCard = null;
        secondCard = null;
        tryCount++;  // 매칭함수 불러올때마다 카운트 +1
    }

    // 시간 추가 효과 함수(투명해지기)
    IEnumerator FalseTryTime(Text text, float fadeTime)
    {
        text.gameObject.SetActive(true);
        Color originalColor = text.color;  // 텍스트의 초기 색상

        float timer = 0f;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeTime); // Lerp는 선형보간 함수 (중간값을 계산하여 부드러운 이동을 만들어냄)
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // r,g,b색상은 기존의 색상을 이용하지만 투명도만 조절하여 점점 투명하게 만든다.
            yield return null;
        }

        text.gameObject.SetActive(false); // 반복문이 끝났으면 비활성화
    }



}
