using System.Collections;
using System.Collections.Generic;
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
        // 이미 열린 카드는 클릭 무시
        if (anim.GetBool("IsOpen"))
            return;

        audioSource.PlayOneShot(clip);
        anim.SetBool("IsOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        // 1. firstCard가 비었다면 내 정보를 넘겨준다
        if (GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
            Invoke("CloseCardInvoke", 3f); //3초후 카드닫기함수
        }
        else
        {
            // 두 번째 카드가 이미 열려있는 경우 무시
            if (GameManager.instance.secondCard != null && GameManager.instance.secondCard.anim.GetBool("IsOpen"))
                return;

            GameManager.instance.secondCard = this;
            GameManager.instance.Matched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    private void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }
    void CloseCardInvoke()
    {
        anim.SetBool("IsOpen", false);
        front.SetActive(false);
        back.SetActive(true);
        if (front.activeSelf) //카드가 앞면이라면
        {
            front.SetActive(false); //앞면 숨김
            back.SetActive(true);   //뒷면 보임
            GameManager.instance.firstCard = null; //첫카드자리 비움
        }
    }

    // hierarchy에 저장돼 있는 카드 뒷면의 색을 변경하는 작업
    public void ChangeColor(Color color)
    {
        back.GetComponent<SpriteRenderer>().color = color;
    }
}