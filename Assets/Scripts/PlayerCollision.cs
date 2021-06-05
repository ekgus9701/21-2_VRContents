using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    GameManager gameManager;
    
    
    float timer;
    int waitingTime;
    public int collide=1;  //PlayerController���� ����ؾ��ϴ� �����̴�.

    void Start()
    {
        timer = 0.0f;
        waitingTime = 1;
        gameManager = FindObjectOfType<GameManager>(); //timer �ð��� �����;��ؼ� �����ߴ�.
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "danger") //���� ���� ������ 
        {
            collide = 0; //�ε����� collide�� 0
            timer += Time.deltaTime;
            Debug.Log("�浹");
            gameManager.GameTime -= 5; //�ð����� 5�ʱ��
            gameManager.collisionText = "-5s"; //5�� �𿴴ٴ� �ؽ�Ʈ �����ֱ�
            if (timer > waitingTime) //1�� ������
            {
                gameManager.collisionText = ""; //�ؽ�Ʈ ���ֱ�
                timer = 0;
            }

        }
        if (other.gameObject.tag == "wall") // ���� ������ 
        {
            collide = 0; //�ε����� collide�� 0
        }


            if (other.gameObject.tag == "clock")//Ŭ�� ������ ������
         {
            timer += Time.deltaTime;
            Debug.Log("�ð�");
            gameManager.GameTime += 20; //�ð����� 20�� ���ϱ�
            Destroy(other.gameObject); //Ŭ�������� �����ϱ�
            gameManager.collisionText = "+20s"; //20�� �߰��ƴٴ� �ؽ�Ʈ �����ֱ�
            if (timer > waitingTime) //1�� ������
            {
                gameManager.collisionText = ""; //�ؽ�Ʈ ���ֱ�
                timer = 0;
            }

        }
    }


}
