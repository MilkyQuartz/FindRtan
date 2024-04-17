using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public GameObject card;
    public Text levelTxt;

    GameObject audioManager;

    int level=0; //게임 난이도 [0,1,2,3,4 로 예정][스코어에 따른]
    int nowStage; //스타트씬에서 받아올 변수

    List<int> intList; //배열크기가 확정되지않을때 사용

    void Start()
    {
        audioManager = GameObject.Find("AudioManager"); //스타트씬에서 넘어온 오디오매니저 찾기
        nowStage = audioManager.GetComponent<AudioManager>().stageNum; //오디오매니저 따라 들어온 난이도값
        //Debug.Log(nowStage);
        level=nowStage+1; //텍스트 표시용 [ level =난이도값0 +1]
        levelTxt.text= level.ToString(); //난이도 텍스트 연결 

        Level(nowStage);         
        //float size=1f-(level*0.1f); // 난이도에 따른 카드크기
        int[] arr=intList.ToArray(); //리스트를 배열로 변경
        
        //int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 }; 
        //arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        float flyInDuration = 0.03f; // 카드가 화면 안으로 날아오는 시간

        StartCoroutine(CardAnimation(arr, flyInDuration));
    }

    IEnumerator CardAnimation(int[] arr, float flyInDuration)
    {
        List<Vector2> targetPositions = new List<Vector2>();

        for (int i = 0; i < arr.Length; i++) //20 >>arr.Length 대체
        {
            // 여기서 5로 나눠서 4행 5열이 되는데 4로 나누면 5행 4열이 됩니당.
            float x = (i % 5) * 1.1f - 2.2f;
            float y = (i / 5) * 1.1f - 3.0f;
            targetPositions.Add(new Vector2(x, y));
        }

        Shuffle(targetPositions); // 목표 위치를 섞음

        for (int i = 0; i < arr.Length; i++) //20 >>arr.Length 대체
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

    public void Level(int num){    

        int arrLength=6+num; // 6+level 4 = 10
        
        int[] arr1= new int[arrLength];
        int[] arr2= new int[arrLength]; 
        for(int j=0;j<arrLength;j++){
            arr1[j]=j; // arr1[10]={0,1,2,3,4,5,6,7,8,9} 
            //Debug.Log(arr1[j]);           
            arr2[j]=j; // arr2[10]={0,1,2,3,4,5,6,7,8,9}          
        }
        
        int[] arrBack= arr1.Concat(arr2).ToArray();// arrBack = arr1 + arr2
        //arrBack[20]={0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9}
        arrBack = arrBack.OrderBy(x => Random.Range(0f, 1f)).ToArray();// //배열 오름차수 랜덤정렬 
       
        // for(int j=0;j<arrBack.Length;j++) Debug.Log(arrBack[j]);       
          
        intList=arrBack.ToList(); //배열을 리스트로 만듬
    }
}
