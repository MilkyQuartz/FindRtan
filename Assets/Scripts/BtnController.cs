using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour
{
    public int difficulty; // 난이도 값을 받을 변수
    public Color initialColor = Color.white; // 이미지 버튼 색 설정할 변수
    private Image buttonImage; 
    private Button button;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();

        // 클리어 상태를 확인하여 버튼 활성화 여부 결정
        bool isCleared = PlayerPrefs.GetInt("Round" + difficulty + "Cleared", 0) == 1;
        bool isFirstDifficulty = difficulty == 1; // 첫번째 스테이지는 풀어놔야해서

        button.interactable = isCleared || isFirstDifficulty;
        buttonImage.color = isCleared ? Color.white : initialColor;
    }

    public void OnClick()
    {
        // PlayerPrefs에 난이도 값을 저장
        PlayerPrefs.SetInt("Difficulty", difficulty);

        // 클리어된 상태를 저장
        PlayerPrefs.SetInt("Round" + difficulty + "Cleared", 1);

        // 다음 난이도를 클리어 상태로 설정
        int nextDifficulty = difficulty + 1;
        if (nextDifficulty <= 5) // 팀원이 5명이라 총 5단계까지 있음
        {
            PlayerPrefs.SetInt("Round" + nextDifficulty + "Cleared", 1);
        }

        SceneManager.LoadScene("MainScene");
    }
}
