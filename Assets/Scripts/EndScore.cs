using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScore : MonoBehaviour
{
    public Text endTitle; // 성공 실패 텍스트
    public Text endTimeTxt; // 몇초 남았는지
    float timeDate = 0;
    public Text TryTxt; // 시도 횟수
    int tryDate = 0;
    public Text scoreTxt; // 점수 텍스트
    int scoreDate = 0;
    public Text stageTxt; // 스테이지 텍스트
    float stageDate = 0;
    float stagePercent = 0;
    float stageNum = 0;
    public Text totalTxt;
    float totalDate = 0;
    float clearScore = 0;
    public Text bestScoreTxt; // 베스트 스코어를 표시할 UI Text 요소
    public Button retryBtn; // 다시하기 버튼
    public Button nextRoundBtn; // 다음 단계 버튼
    float bestScore = 0;

    void Start()
    {
        // PlayerPrefs에서 저장된 난이도 값을 가져옴
        int difficulty = PlayerPrefs.GetInt("Difficulty", 1);

        timeDate = 30.0f - GameManager.instance.time;
        endTimeTxt.text = timeDate.ToString("N2");

        tryDate = GameManager.instance.tryCount;
        TryTxt.text = tryDate.ToString();

        scoreDate = GameManager.instance.score;
        scoreTxt.text = scoreDate.ToString();

        stageDate = difficulty; // ImageButton 스크립트에서 저장된 난이도 값을 사용
        stagePercent = 1f + (stageDate * 0.2f);
        stageNum = stageDate;
        stageTxt.text = stageNum.ToString();

        totalDate = (timeDate + tryDate + scoreDate) * stagePercent;
        totalTxt.text = totalDate.ToString("N0");

        clearScore = (stageDate * 10) + 60;

        // 이전에 저장된 베스트 스코어 가져오기
        bestScore = PlayerPrefs.GetFloat("BestScore" + difficulty, 0);

        // 현재 스코어가 베스트 스코어보다 높으면 베스트 스코어를 갱신
        if (totalDate > bestScore)
        {
            bestScore = totalDate;
            PlayerPrefs.SetFloat("BestScore" + difficulty, bestScore);
        }

        // 베스트 스코어 표시
        bestScoreTxt.text = bestScore.ToString("N0");

        if (timeDate >= 0)
        {
            endTitle.text = "성공";

            // 현재 라운드의 클리어 상태를 확인
            bool isCurrentRoundCleared = PlayerPrefs.GetInt("Round" + difficulty + "Cleared", 0) == 1;

            if (totalDate >= clearScore && !isCurrentRoundCleared)
            {
                // 클리어 조건을 충족했을 때, 현재 라운드를 클리어 상태로 변경하고 다음 라운드를 해금하고 저장
                PlayerPrefs.SetInt("Round" + difficulty + "Cleared", 1);
                int nextDifficulty = difficulty + 1;
                PlayerPrefs.SetInt("Difficulty", nextDifficulty);

                // 다음 라운드 버튼 활성화 여부 설정
                bool isNextRoundUnlocked = nextDifficulty <= 5;
                nextRoundBtn.interactable = isNextRoundUnlocked;
            }
            else
            {
                retryBtn.onClick.AddListener(RetryGame);
            }
        }
        else
        {
            timeDate = 0;
            endTitle.text = "실패";
        }
    }

    void RetryGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void NextRound()
    {
        // 현재 난이도를 가져옴
        int difficulty = PlayerPrefs.GetInt("Difficulty", 1);

        // 현재 클리어한 난이도의 다음 난이도를 확인
        int nextDifficulty = difficulty + 1;

        // 다음 단계가 해금되어 있는지 확인
        bool isNextRoundUnlocked = PlayerPrefs.GetInt("Round" + nextDifficulty + "Cleared", 0) == 1;
        if (isNextRoundUnlocked)
        {
            PlayerPrefs.SetInt("Difficulty", nextDifficulty);
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
