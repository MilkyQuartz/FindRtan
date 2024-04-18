using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour
{
    public int difficulty; // ���̵� ���� ���� ����
    public Color initialColor = Color.white; // �̹��� ��ư �� ������ ����
    private Image buttonImage; 
    private Button button;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();

        // Ŭ���� ���¸� Ȯ���Ͽ� ��ư Ȱ��ȭ ���� ����
        bool isCleared = PlayerPrefs.GetInt("Round" + difficulty + "Cleared", 0) == 1;
        bool isFirstDifficulty = difficulty == 1; // ù��° ���������� Ǯ������ؼ�

        button.interactable = isCleared || isFirstDifficulty;
        buttonImage.color = isCleared ? Color.white : initialColor;
    }

    public void OnClick()
    {
        // PlayerPrefs�� ���̵� ���� ����
        PlayerPrefs.SetInt("Difficulty", difficulty);

        // Ŭ����� ���¸� ����
        PlayerPrefs.SetInt("Round" + difficulty + "Cleared", 1);

        // ���� ���̵��� Ŭ���� ���·� ����
        int nextDifficulty = difficulty + 1;
        if (nextDifficulty <= 5) // ������ 5���̶� �� 5�ܰ���� ����
        {
            PlayerPrefs.SetInt("Round" + nextDifficulty + "Cleared", 1);
        }

        SceneManager.LoadScene("MainScene");
    }
}
