using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int currentDifficulty; // ���� ���� ���� ���̵�
    private int highDifficulty; // ���� ����  ���̵�
    private bool[] clearArr; // Ŭ������ ���̵��� �����ϴ� �迭

    void Start()
    {
        clearArr = new bool[5]; // ���̵��� 1���� 5�����̹Ƿ� ũ�Ⱑ 5�� �迭 ���

        // ���� ���̵��� ����
        currentDifficulty = PlayerPrefs.GetInt("Difficulty", 1);

        // ���� ���� Ŭ����� ���̵��� ����
        highDifficulty = PlayerPrefs.GetInt("HighDifficulty", 1);

        // Ŭ������ ���̵��� ����
        for (int i = 0; i < currentDifficulty; i++)
        {
            SetLevelCleared(i + 1, true); // ���� ���̵����� Ŭ���� ���·� ����
        }
    }

    // Ŭ���� ���¸� �����ϴ� �Լ�
    public void SetLevelCleared(int difficulty, bool cleared)
    {
        clearArr[difficulty - 1] = cleared; // �迭 �ε����� 0���� �����ϹǷ� difficulty���� 1�� ����. ��ư ���̵� 1���� ����

        // �ְ� ����
        if (cleared && difficulty > highDifficulty)
        {
            highDifficulty = difficulty;
            PlayerPrefs.SetInt("HighDifficulty", highDifficulty);
        }
    }

    // Ŭ���� ���¸� ��ȯ�ϴ� �Լ�
    public bool IsLevelCleared(int difficulty)
    {
        return clearArr[difficulty - 1]; 
    }

    // ���� ���� ���� ���̵� ��ȯ
    public int GetCurrentDifficulty()
    {
        return currentDifficulty;
    }

    // ���� ���� ���̵�
    public int GetHigDifficulty()
    {
        return highDifficulty;
    }
}
