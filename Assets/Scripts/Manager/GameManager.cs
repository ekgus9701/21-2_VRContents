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

    void Start()
    {
        instance = this;
        GameTimeTextInCollision.text = ""; //ó���� '�浹�� �ؽ�Ʈ' �Ⱥ��̰� ����
        LoseText.text = ""; //ó���� ������ ���̴� ���� �Ⱥ��̰� ����
       
    }

    // Update is called once per frame
    void Update()
    {
        GameTime -= Time.deltaTime; //Ÿ�̸� �ð� ��� �پ���
        if (isStartGame)
        {

            if (check == 0)
            { //���������� �ð��� �������ָ� �޴� ���ۺ��� �ð��� ����. check�� ������������ ������Ʈ �ɶ����� 121�ʷ� �ʱ�ȭ�Ǿ� �ð��� �����ʴ´�.
                GameTime = 121;
                check++;
            }


            if ((int)GameTime > 0)
            {

                GameTimeText.text = "Time: " + (int)GameTime;  //ȭ�鿡 ����Ѵ�
                GameTimeTextInCollision.text = collisionText; //�浹�� �ؽ�Ʈ�� ����Ѵ�.
            }
        }


        if ((int)GameTime == 0) //���� �ð��� 0�ʸ�
        {
            Debug.Log("�ð� �� ��������");
            GameTimeText.text = "Time: " + (int)GameTime;  //ȭ�鿡 ����Ѵ�
            isStartGame = false; //���� ����

            LoseText.text = "Fail\nThe Game will be out in 5s";//5���Ŀ� ������ ���� ���̶�� ���� �Ѵ�.

        }

        if ((int)GameTime < -5)
        { //5���Ŀ� ������ �����Ѵ�.

#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; 
                    
#else
            Application.Quit(); // ���� ����
#endif
        }

    }


    public void GameStart()
    {
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(true);
        }
        isStartGame = true;
       
    }

    public void MainMenu()
    {
        GameTimeTextInCollision.text = "";
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(false);
        }
        goTitleUI.SetActive(true);
        //���θ޴��� ���ư����� collision text�� ������ ���������. 

    }
    public void GameQuit() //���� �����ϴ� ��ư�� �������.
    {
        {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; 
                    
#else
            Application.Quit(); // ���� ����
#endif
        }
    }
}
   



