using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject[] goGameUI = null;
    public float GameTime = 126;
    public Text GameTimeText;
    public Text GameTimeTextInCollision;
    public string collisionText;

    public bool isStartGame = false;
    int check = 0;

    void Start()
    {

        instance = this;
        GameTimeTextInCollision.text = ""; //처음에 '충돌시 텍스트' 안보이게
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartGame)
        {

            if (check == 0)
            { //위에서 시간을 설정해주면 메뉴 시작부터 시간이 간다. check를 해주지않으면 업데이트 될때마다 121초로 초기화되어 시간이 가지않는다.
                GameTime = 121;
                check++;
            }
            if ((int)GameTime > 0)
            {
                GameTime -= Time.deltaTime; //타이머 시간 계속 줄어든다
                GameTimeText.text = "Time: " + (int)GameTime;  //화면에 출력한다
                GameTimeTextInCollision.text = collisionText; //충돌시 텍스트도 출력한다.
            }
            if ((int)GameTime == 0) //시간이 다 지나면
            {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; 
                    
#else
                Application.Quit(); // 게임 종료
#endif
            }
        }
    }



    

    public void GameStart()
    {
        for(int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(true);
        }
        isStartGame = true;
    }

   


}
