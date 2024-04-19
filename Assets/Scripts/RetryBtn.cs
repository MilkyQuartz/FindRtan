using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryBtn : MonoBehaviour
{
    public void Retry()
    {
        // ���� �÷��̾��� Ŭ���� ����� ����
        SavePlayerClearData();

        SceneManager.LoadScene("StageScene");
    }

    private void SavePlayerClearData()
    {
        // ���� Ŭ���� ���¸� PlayerPrefs�� ����
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            int currentDifficulty = playerController.GetCurrentDifficulty();
            for (int i = 1; i <= 5; i++)
            {
                bool isCleared = playerController.IsLevelCleared(i);
                // ���� Ŭ������ ���̵��� �ִ� ���̵��� ��� �ִ� ���̵��� ����
                int clearedValue = (i == currentDifficulty && isCleared) ? 5 : (isCleared ? 1 : 0);
                PlayerPrefs.SetInt("Difficulty" + i + "Cleared", clearedValue);
            }
            PlayerPrefs.Save(); // ����� ������ ����
        }
    }
}
