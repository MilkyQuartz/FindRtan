using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScore : MonoBehaviour
{
    public Image weName; // 매칭성공시 우리이름
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
    public Text bestScoreTxt; // 최고점수 텍스트
    public Button retryBtn; // 다시하기 버튼
    public Button nextRoundBtn; // 다음 단계 버튼
    public Button checkBtn; // 확인 버튼
    float bestScore = 0;

    void Start()
    {
        // 플레이어프리팹스에서 저장된 난이도 값을 가져옴
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
            weName.gameObject.SetActive(true);

            // 다음단계 버튼 활성화 여부 설정
            bool isNextRoundUnlocked = difficulty < 5;
            nextRoundBtn.interactable = isNextRoundUnlocked;
            if (isNextRoundUnlocked)
            {
                nextRoundBtn.onClick.AddListener(NextRoundButtonClicked);
            }
            else
            {
                nextRoundBtn.gameObject.SetActive(false); // 다음 단계 버튼을 비활성화
                checkBtn.gameObject.SetActive(true);
                checkBtn.onClick.AddListener(SaveAndExit);
            }
            retryBtn.gameObject.SetActive(true);
            retryBtn.onClick.AddListener(RetryGame);
        }
        else
        {
            timeDate = 0;
            endTitle.text = "실패";
            weName.gameObject.SetActive(false);
            retryBtn.gameObject.SetActive(true);
            retryBtn.onClick.AddListener(RetryGame);

            checkBtn.gameObject.SetActive(true);
            checkBtn.onClick.AddListener(SaveAndExit);
            nextRoundBtn.gameObject.SetActive(false); // 다음 단계 버튼을 비활성화
        }
    }

    public void SaveAndExit()
    {
        // 현재 난이도
        int currentDifficulty = PlayerPrefs.GetInt("Difficulty", 1);

        // 클리어한 내용을 플레이어프리팹스에 저장
        PlayerPrefs.SetInt("Round" + currentDifficulty + "Cleared", 1); // 현재 난이도의 클리어 상태를 true로 설정
        PlayerPrefs.SetInt("GameCleared", 1);

        // 스테이지 씬으로 이동
        SceneManager.LoadScene("StageScene");
    }

    public void RetryGame()
    {
        // 현재 난이도
        int currentDifficulty = PlayerPrefs.GetInt("Difficulty", 1);

        // 다시하기를 누르면 난이도를 감소시킴 왜냐면 게임매니저에서 짝 다맞추면 자동으로 난이도 +1시켜서 근데 이건 성공하면 다시하기 버튼을 빼서 필요없는 코드가 됐다. 
        int previousDifficulty = currentDifficulty - 1;
        if (previousDifficulty >= 1) 
        {
            PlayerPrefs.SetInt("Difficulty", previousDifficulty);
            PlayerPrefs.SetInt("Round" + currentDifficulty + "Cleared", 0); // 현재 난이도의 클리어 상태를 false로 설정
            PlayerPrefs.SetInt("GameCleared", 0);
            SceneManager.LoadScene("MainScene");
        }
    }

    public void NextRoundButtonClicked()
    {
        int currentDifficulty = PlayerPrefs.GetInt("Difficulty", 1);
        // 현재 클리어한 난이도의 다음 난이도를 확인
        int nextDifficulty = currentDifficulty + 1;
        SceneManager.LoadScene("MainScene");
    }
}
