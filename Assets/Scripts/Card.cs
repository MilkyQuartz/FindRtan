using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public GameObject front;
    public GameObject back;
    public Animator anim;
    public int idx = 0;
    public SpriteRenderer frontImage;
    AudioSource audioSourve;
    public AudioClip clip;

    void Start()
    {
        audioSourve = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void Setting(int num)
    {
        idx = num;
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }

    public void OpenCard()
    {
        if (GameManager.Instance.secondCard != null) return;

        audioSourve.PlayOneShot(clip);
        anim.SetBool("IsOpen", true);
        front.SetActive(true);
        back.SetActive(false);


        // 1. firstCard�� ����ٸ� �� ������ �Ѱ��ش�
        if(GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }

        // 2. 1���� �����̰� scondeCard�� ����ٸ� �� ������ �Ѱ��ش�.
        // 3. Matched�Լ��� ȣ��

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
        anim.SetBool("isOepn", false);
        front.SetActive(false);
        back.SetActive(true);
    }

    //hierarchy�� ����� �ִ� ī�� �޸��� ���� �ҷ��������� �۾�
    public void ChangeColor(Color color)
    {
        back.GetComponent<SpriteRenderer>().color = color;
    }
}
