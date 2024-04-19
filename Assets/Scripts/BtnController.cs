using UnityEngine;
using UnityEngine.UI;

public class BtnController : MonoBehaviour
{
    public int difficulty; // 난이도 값을 받을 변수
    private Image buttonImage;
    private Button button;
    private PlayerController playerController;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();

        // Player 프리팹에서 스크립트 가져오기
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }

        bool isCleared = playerController.IsLevelCleared(difficulty);
        bool isFirstDifficulty = difficulty == 1; // 첫번째 스테이지는 풀어놔야해서

        // 1단계거나 클리어 됐거나하는 버튼 활성화
        if (isCleared || isFirstDifficulty)
        {
            button.interactable = true;
            buttonImage.color = Color.white;
        }
        else
        {
            // 버튼 비활성화
            button.interactable = false;
        }
    }

    public void OnClick()
    {
        // 플레이어의 클리어 상태 저장
        playerController.SetLevelCleared(difficulty, true);

        // 다음 난이도가 존재하고 클리어되지 않았다면 클리어 상태 설정
        int nextDifficulty = difficulty + 1;
        if (nextDifficulty <= 5) // 팀원이 5명이라 총 5단계까지 있음
        {
            if (!playerController.IsLevelCleared(nextDifficulty))
            {
                playerController.SetLevelCleared(nextDifficulty, false); // 다음 난이도의 클리어 상태를 false로 설정
            }
        }

        // 난이도 값 저장 보드가 배열을 만들 수 있게 해야함
        PlayerPrefs.SetInt("Difficulty", difficulty);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
