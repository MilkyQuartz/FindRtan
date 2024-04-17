using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public GameObject front;
    public GameObject back;
    public Animator anim;
    public int idx = 0;
    public SpriteRenderer frontImage;
    AudioSource audioSource;
    public AudioClip clip;

    bool isFliped=false;
        

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public void Setting(int num)
    {
        idx = num;
        frontImage.sprite = Resources.Load<Sprite>($"{idx}");
    }

    public void OpenCard()
    {
        if (GameManager.Instance.secondCard != null) return;

        audioSource.PlayOneShot(clip);
        anim.SetBool("IsOpen", true); 

        // 1. firstCard가 비었다면 내 정보를 넘겨준다
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
            isFliped=true;
            Invoke("SingleFlip", 3f); //3초후 카드닫기함수
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();            
        }

    }

    public void DestroyCard()
    {
        isFliped=false; //매칭후 결과 [SingleFlip()사용]
        Invoke("DestroyCardInvoke", 0.5f);
    }

    private void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        isFliped=false; //매칭후 결과 [SingleFlip()사용]
        Invoke("CloseCardInvoke", 0.8f);        
    }
    void CloseCardInvoke()
    {          
        anim.SetBool("IsOpen", true);        
    }
    void SingleFlip() // 카드닫기함수(3초뒤 발동)
    {   
        if(isFliped==true){  // 1장 오픈상태
            anim.SetBool("IsOpen", true); //카드플립
            GameManager.Instance.firstCard = null; //자리비움
        }
    }

    // hierarchy에 저장돼 있는 카드 뒷면의 색을 변경하는 작업
    public void ChangeColor(Color color)
    {
        back.GetComponent<SpriteRenderer>().color = color;
    }

    public void ChangeCard() //애니메이샨으로 호출됨
    {
        if(!front.activeSelf){ //뒤면이면
            front.SetActive(true); 
            back.SetActive(false);                              
        }
        else //앞면이면
        {            
            front.SetActive(false);
            back.SetActive(true); 
        }
        anim.SetBool("IsOpen", false); //카드뒤집기 에니동작
    }    
}
