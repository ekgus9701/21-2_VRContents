using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    GameManager gameManager;
    
    
    public int collide=1;  //PlayerController���� ����ؾ��ϴ� �����̴�.

    void Start()
    {
     
        gameManager = FindObjectOfType<GameManager>(); //timer �ð��� �����;��ؼ� �����ߴ�.
    }


    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "danger") //���� ���� ������ 
        {
            collide = 0; //�ε����� collide�� 0
            Debug.Log("�浹");
            gameManager.GameTime -= 5; //�ð����� 5�ʱ��
            gameManager.collisionText = "-5s"; //5�� �𿴴ٴ� �ؽ�Ʈ �����ֱ�
           

        }
        if (other.gameObject.tag == "wall") // ���� ������ 
        {
            collide = 0; //�ε����� collide�� 0
        }


        if (other.gameObject.tag == "clock")//Ŭ�� ������ ������
        {
            
            Debug.Log("�ð�");
            gameManager.GameTime += 20; //�ð����� 20�� ���ϱ�
            Destroy(other.gameObject); //Ŭ�������� �����ϱ�
            gameManager.collisionText = "+20s"; //20�� �߰��ƴٴ� �ؽ�Ʈ �����ֱ�
           

        }
    }


}
