using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject card;

    void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        float flyInDuration = 0.1f; // ī�尡 ȭ�� ������ ���ƿ��� �ð�

        StartCoroutine(CardAnimation(arr, flyInDuration));
    }

    IEnumerator CardAnimation(int[] arr, float flyInDuration)
    {
        List<Vector2> targetPositions = new List<Vector2>();

        for (int i = 0; i < 16; i++)
        {
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            targetPositions.Add(new Vector2(x, y));
        }

        Shuffle(targetPositions); // ��ǥ ��ġ�� ����

        for (int i = 0; i < 16; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            Vector2 targetPos = targetPositions[i]; // ������ ��ǥ ��ġ

            // ī�尡 ȭ�� �ۿ��� �����ϰ� ��ǥ ��ġ�� ���ƿ��� �ִϸ��̼�(�̰� �� ���� ���� �ʿ���)
            float elapsedTime = 0f;
            while (elapsedTime < flyInDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / flyInDuration);
                go.transform.position = Vector2.Lerp(targetPos + new Vector2(Random.Range(-200f, 200f), Random.Range(-300f, 300f)), targetPos, t);
                yield return null;
            }

            go.SetActive(true);
            go.GetComponent<Card>().Setting(arr[i]);
        }
    }

    // ���� �Լ�
    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            int randomIndex = Random.Range(i, n);

            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
