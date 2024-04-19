using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int currentDifficulty; // 현재 진행 중인 난이도
    private int highDifficulty; // 가장 높은  난이도
    private bool[] clearArr; // 클리어한 난이도를 저장하는 배열

    void Start()
    {
        clearArr = new bool[5]; // 난이도는 1부터 5까지이므로 크기가 5인 배열 사용

        // 현재 난이도를 설정
        currentDifficulty = PlayerPrefs.GetInt("Difficulty", 1);

        // 가장 높은 클리어된 난이도를 설정
        highDifficulty = PlayerPrefs.GetInt("HighDifficulty", 1);

        // 클리어한 난이도를 설정
        for (int i = 0; i < currentDifficulty; i++)
        {
            SetLevelCleared(i + 1, true); // 이전 난이도까지 클리어 상태로 설정
        }
    }

    // 클리어 상태를 설정하는 함수
    public void SetLevelCleared(int difficulty, bool cleared)
    {
        clearArr[difficulty - 1] = cleared; // 배열 인덱스는 0부터 시작하므로 difficulty에서 1을 뺀다. 버튼 난이도 1부터 시작

        // 최고값 갱신
        if (cleared && difficulty > highDifficulty)
        {
            highDifficulty = difficulty;
            PlayerPrefs.SetInt("HighDifficulty", highDifficulty);
        }
    }

    // 클리어 상태를 반환하는 함수
    public bool IsLevelCleared(int difficulty)
    {
        return clearArr[difficulty - 1]; 
    }

    // 현재 진행 중인 난이도 반환
    public int GetCurrentDifficulty()
    {
        return currentDifficulty;
    }

    // 가장 높은 난이도
    public int GetHigDifficulty()
    {
        return highDifficulty;
    }
}
