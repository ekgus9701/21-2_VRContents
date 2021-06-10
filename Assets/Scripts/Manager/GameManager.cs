using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject[] goGameUI = null;
    [SerializeField] GameObject goTitleUI = null;


    public float GameTime;
    public Text GameTimeText;
    public Text LoseText;
    public Text GameTimeTextInCollision;
    public string collisionText;

    public bool isStartGame = false;
    int check = 0;


    TimingManager theTiming;
    PlayerController thePlayer;

    void Start()
    {
        instance = this;
        GameTimeTextInCollision.text = ""; //처음에 '충돌시 텍스트' 안보이게 설정
        LoseText.text = ""; //처음에 졌을때 보이는 문구 안보이게 설정
        thePlayer = FindObjectOfType<PlayerController>();
        theTiming = FindObjectOfType<TimingManager>();

    }

    void newStart()
    {
        if (isStartGame && Result.win) //이기고 메인메뉴에 가서 다시 시작하는 경우
        {
            GameTimeTextInCollision.text = ""; //처음에 '충돌시 텍스트' 안보이게 설정
            LoseText.text = ""; //처음에 졌을때 보이는 문구 안보이게 설정
            GameTime = 151; //시간 초기화

        }
    }

    // Update is called once per frame
    void Update()
    {
        GameTime -= Time.deltaTime; //타이머 시간 계속 줄어든다
        if (isStartGame)
        {

            if (check == 0)
            { //전역변수로 시간을 설정해주면 메뉴 시작부터 시간이 간다. check를 해주지않으면 업데이트 될때마다 121초로 초기화되어 시간이 가지않는다.
                GameTime = 151;
                check++;
            }


            if ((int)GameTime > 0)
            {

                GameTimeText.text = "Time: " + (int)GameTime;  //화면에 출력한다
                GameTimeTextInCollision.text = collisionText; //충돌시 텍스트도 출력한다.
            }
        }
        if (Result.win)
        {
            GameTime += Time.deltaTime; //이겼으면 시간이 더이상 줄지않아야한다.
        }




        if ((int)GameTime == 0) //남은 시간이 0초면
        {
            Debug.Log("시간 다 지났을때");
            GameTimeText.text = "Time: " + (int)GameTime;  //화면에 출력한다
            isStartGame = false; //게임 끝남

            LoseText.text = "Fail\nThe Game will be out in 5s";//5초 후에 게임이 꺼질 것이라는 예고를 한다.

        }

        if ((int)GameTime < -5)
        { //5초후에 게임을 종료한다.

#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; 
                    
#else
            Application.Quit(); // 게임 종료
#endif
        }

        if (isStartGame && Result.win) //이기고 메인메뉴로 가서 다시 스타트 할때
        {
            GameTime -= Time.deltaTime; //타이머 시간 다시 계속 줄어든다 (이게 없으면 타이머가 멈춰있다)
        }

    }


    public void GameStart()
    {
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(true);
        }
        isStartGame = true;
        theTiming.Initialized();
        thePlayer.Initialized();
    }

    public void MainMenu()
    {
        GameTimeTextInCollision.text = "";
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(false);
        }
        goTitleUI.SetActive(true);
        //메인메뉴로 돌아갔을때 collision text가 보여서 삭제해줬다. 

    }
    public void GameQuit() //게임 종료하는 버튼을 만들었다.
    {
        {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; 
                    
#else
            Application.Quit(); // 게임 종료
#endif
        }
    }
}




