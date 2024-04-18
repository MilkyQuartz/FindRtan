using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class EndScore : MonoBehaviour
{
    public Text endTitle;
    public Text endTxt;
    public Text endTimeTxt;
    float timeDate=0;
    public Text TryTxt;
    int tryDate=0;
    public Text scoreTxt;
    int scoreDate=0;
    public Text stageTxt;
    float stageDate=0;
    float stagePercent=0;
    float stageNum=0;
    public Text totalTxt;
    float totalDate=0;
    float clearScore=0;
    // Start is called before the first frame update
    void Start()
    {
        timeDate= 30.0f - GameManager.instance.time; // 남은시간 [성공시 = 30.00f-28.25f] 
        endTimeTxt.text=timeDate.ToString(); 

        tryDate=GameManager.instance.tryCount; 
        tryDate=tryDate*-1; // 시도횟수는 감점요인
        TryTxt.text=tryDate.ToString();

        scoreDate=GameManager.instance.score; // 맞추면 10점[gameManager에서 설정함]
        scoreTxt.text=scoreDate.ToString(); 
        
        stageDate=AudioManager.instance.stageNum; //1난이도 = 0값
        stagePercent=1f+(stageDate*0.2f); // 스테이지당 +20%로 적용
        stageNum=stageDate+1; //표기 사용 0값 +1 
        stageTxt.text=stageNum.ToString();

        totalDate=(timeDate+tryDate+scoreDate)*stagePercent;
        totalTxt.text=totalDate.ToString("N0"); //소수점없이 

        clearScore=(stageDate*10)+60; //1난이도 60점 클리어 ,5난이도 100점 클리어
        Debug.Log("clearScore="+clearScore);
        if(timeDate >= 0){ 
            endTitle.text="성공";

            if(totalDate>=clearScore){
                endTxt.text="더 어렵게?";
                AudioManager.instance.stageNum+=1;
            }else{
                endTxt.text="재도전?";
            }
        }
        else{
            timeDate=0;
            endTitle.text="실패";
            endTxt.text="재도전?";
        }
    }   
}
