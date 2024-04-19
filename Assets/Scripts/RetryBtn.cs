using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryBtn : MonoBehaviour
{
    public void Retry()
    {
        // 현재 플레이어의 클리어 기록을 저장
        SavePlayerClearData();

        SceneManager.LoadScene("StageScene");
    }

    private void SavePlayerClearData()
    {
        // 현재 클리어 상태를 PlayerPrefs에 저장
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            int currentDifficulty = playerController.GetCurrentDifficulty();
            for (int i = 1; i <= 5; i++)
            {
                bool isCleared = playerController.IsLevelCleared(i);
                // 현재 클리어한 난이도가 최대 난이도인 경우 최대 난이도로 저장
                int clearedValue = (i == currentDifficulty && isCleared) ? 5 : (isCleared ? 1 : 0);
                PlayerPrefs.SetInt("Difficulty" + i + "Cleared", clearedValue);
            }
            PlayerPrefs.Save(); // 변경된 내용을 저장
        }
    }
}
