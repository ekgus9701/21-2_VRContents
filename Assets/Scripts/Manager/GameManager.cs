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
        GameTimeTextInCollision.text = ""; //ó���� '�浹�� �ؽ�Ʈ' �Ⱥ��̰�
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartGame)
        {

            if (check == 0)
            { //������ �ð��� �������ָ� �޴� ���ۺ��� �ð��� ����. check�� ������������ ������Ʈ �ɶ����� 121�ʷ� �ʱ�ȭ�Ǿ� �ð��� �����ʴ´�.
                GameTime = 121;
                check++;
            }
            if ((int)GameTime > 0)
            {
                GameTime -= Time.deltaTime; //Ÿ�̸� �ð� ��� �پ���
                GameTimeText.text = "Time: " + (int)GameTime;  //ȭ�鿡 ����Ѵ�
                GameTimeTextInCollision.text = collisionText; //�浹�� �ؽ�Ʈ�� ����Ѵ�.
            }
            if ((int)GameTime == 0) //�ð��� �� ������
            {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; 
                    
#else
                Application.Quit(); // ���� ����
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
