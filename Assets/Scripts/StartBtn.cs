using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartBtn : MonoBehaviour
{
    public Image image;
    public Animator anim;
    private Button startBtn;
    private Text startBtnTxt;

    void Start()
    {
        startBtn = GetComponent<Button>();
        startBtnTxt = startBtn.GetComponentInChildren<Text>();
    }

    public void StartGame()
    {
        // �׳�... �߾��..����//
        startBtnTxt.text = "���μ���";
        PlayerPrefs.DeleteAll();

        // �ִϸ��̼� ���
        image.gameObject.SetActive(true); // Image Ȱ��ȭ
        anim.SetTrigger("PlayAnimation"); // �ִϸ��̼� ���

        // 5�� �Ŀ� �� �̵�
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        // 5�ʰ� ��ٸ�
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene("StageScene");
    }
}
