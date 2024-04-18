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
        // 그냥... 했어요..ㅎㅎ//
        startBtnTxt.text = "깨부수기";
        PlayerPrefs.DeleteAll();

        // 애니메이션 재생
        image.gameObject.SetActive(true); // Image 활성화
        anim.SetTrigger("PlayAnimation"); // 애니메이션 재생

        // 5초 후에 씬 이동
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        // 5초간 기다림
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene("StageScene");
    }
}
