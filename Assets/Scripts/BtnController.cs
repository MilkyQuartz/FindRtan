using UnityEngine;
using UnityEngine.UI;

public class BtnController : MonoBehaviour
{
    public int difficulty; // ���̵� ���� ���� ����
    private Image buttonImage;
    private Button button;
    private PlayerController playerController;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();

        // Player �����տ��� ��ũ��Ʈ ��������
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }

        bool isCleared = playerController.IsLevelCleared(difficulty);
        bool isFirstDifficulty = difficulty == 1; // ù��° ���������� Ǯ������ؼ�

        // 1�ܰ�ų� Ŭ���� �ưų��ϴ� ��ư Ȱ��ȭ
        if (isCleared || isFirstDifficulty)
        {
            button.interactable = true;
            buttonImage.color = Color.white;
        }
        else
        {
            // ��ư ��Ȱ��ȭ
            button.interactable = false;
        }
    }

    public void OnClick()
    {
        // �÷��̾��� Ŭ���� ���� ����
        playerController.SetLevelCleared(difficulty, true);

        // ���� ���̵��� �����ϰ� Ŭ������� �ʾҴٸ� Ŭ���� ���� ����
        int nextDifficulty = difficulty + 1;
        if (nextDifficulty <= 5) // ������ 5���̶� �� 5�ܰ���� ����
        {
            if (!playerController.IsLevelCleared(nextDifficulty))
            {
                playerController.SetLevelCleared(nextDifficulty, false); // ���� ���̵��� Ŭ���� ���¸� false�� ����
            }
        }

        // ���̵� �� ���� ���尡 �迭�� ���� �� �ְ� �ؾ���
        PlayerPrefs.SetInt("Difficulty", difficulty);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
