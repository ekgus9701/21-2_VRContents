using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    GameManager gameManager;
    
    
    float timer;
    int waitingTime;
    public int collide=1;  //PlayerController에서 사용해야하는 변수이다.

    void Start()
    {
        timer = 0.0f;
        waitingTime = 1;
        gameManager = FindObjectOfType<GameManager>(); //timer 시간값 가져와야해서 선언했다.
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "danger") //빨간 벽에 닿으면 
        {
            collide = 0; //부딪히면 collide는 0
            timer += Time.deltaTime;
            Debug.Log("충돌");
            gameManager.GameTime -= 5; //시간에서 5초깎기
            gameManager.collisionText = "-5s"; //5초 깎였다는 텍스트 보여주기
            if (timer > waitingTime) //1초 지나면
            {
                gameManager.collisionText = ""; //텍스트 없애기
                timer = 0;
            }

        }
        if (other.gameObject.tag == "wall") // 벽에 닿으면 
        {
            collide = 0; //부딪히면 collide는 0
        }


            if (other.gameObject.tag == "clock")//클락 아이템 먹으면
         {
            timer += Time.deltaTime;
            Debug.Log("시계");
            gameManager.GameTime += 20; //시간에서 20초 더하기
            Destroy(other.gameObject); //클락아이템 삭제하기
            gameManager.collisionText = "+20s"; //20초 추가됐다는 텍스트 보여주기
            if (timer > waitingTime) //1초 지나면
            {
                gameManager.collisionText = ""; //텍스트 없애기
                timer = 0;
            }

        }
    }


}
