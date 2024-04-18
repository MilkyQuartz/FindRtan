using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject card;

    void Start()
    {
        // PlayerPrefs에서 저장된 난이도 값을 가져옴
        int difficulty = PlayerPrefs.GetInt("Difficulty", 1);
        GenerateCards(difficulty);
    }

    public void GenerateCards(int difficulty)
    {
        int pairCount = 0;

        // 난이도에 따라 숫자 쌍의 개수를 결정
        switch (difficulty)
        {
            case 1:
                pairCount = 3;
                break;
            case 2:
                pairCount = 5;
                break;
            case 3:
                pairCount = 7;
                break;
            case 4:
                pairCount = 8;
                break;
            case 5:
                pairCount = 10;
                break;
            default:
                pairCount = 3;
                break;
        }

        int[] arr = new int[pairCount * 2];

        // 숫자 쌍 생성
        for (int i = 0; i < pairCount; i++)
        {
            arr[i * 2] = i;
            arr[i * 2 + 1] = i;
        }

        arr = arr.OrderBy(x => Random.Range(0f, 1f)).ToArray(); // 배열 섞기

        float flyInDuration = 0.1f; // 카드가 화면 안으로 날아오는 시간

        StartCoroutine(CardAnimation(arr, flyInDuration));

        // 보드가 생성될 때 게임 매니저에 카드 카운트 값을 전달
        GameManager.instance.cardCount = pairCount * 2;
    }

    IEnumerator CardAnimation(int[] arr, float flyInDuration)
    {
        List<Vector2> targetPositions = new List<Vector2>();

        for (int i = 0; i < arr.Length; i++)
        {
            float x = (i % 4) * 1.1f - 2.1f;
            float y = (i / 4) * 1.1f - 3.0f;
            targetPositions.Add(new Vector2(x, y));
        }

        Shuffle(targetPositions); // 목표 위치를 섞음

        for (int i = 0; i < arr.Length; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            Vector2 targetPos = targetPositions[i]; // 무작위 목표 위치

            // 카드가 화면 밖에서 시작하고 목표 위치로 날아오는 애니메이션(이건 잘 몰라서 공부 필요함)
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

    // 섞는 함수
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
