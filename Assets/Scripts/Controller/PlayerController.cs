using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    TimingManager theTimingManager;
    CameraController theCam;
    PlayerCollision playerCollision;
    GameManager gameManager;

    public static bool s_canPressKey = true;

    
    [SerializeField] float moveSpeed = 5;
    Vector3 dir = new Vector3();
    Vector3 destPos = new Vector3();

    [SerializeField] float spinSpeed = 270;
    Vector3 rotDir = new Vector3();
    Quaternion destRot = new Quaternion();

    [SerializeField] float recoilPosY = 0.25f;
    [SerializeField] float recoilSpeed = 0.25f;

    [SerializeField] Transform fakeCube = null;
    [SerializeField] Transform realCube = null;

   

    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        theCam = FindObjectOfType<CameraController>();
        playerCollision = FindObjectOfType<PlayerCollision> ();
        gameManager= FindObjectOfType<GameManager>();

    }

    public void Initialized()
    {
        transform.position = Vector3.zero;
        destPos = Vector3.zero;
        realCube.localPosition = new Vector3(39.46f, 0, -22.6f); //���� �� ���θ޴��� ���� �� ����۽� ť�갡 �ִ� ó�� ��ġ�� ���ư�����
        s_canPressKey = true;
        gameManager.GameTime = 151; //�ٽ� ������ �� ����Ÿ�ӵ� �ʱ�ȭ�Ǿ���ؼ� �ʱ�ȭ����
    }


    void Update()
    {

       if (GameManager.instance.isStartGame) { 
            if (s_canPressKey)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
                {

                    StartAction();
                }
            }
        }
    }


    void StartAction()
    {
        //����§�ڵ�
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal")); //�� ��Ȳ�� �°� W ������ ������, A������ ��������, D������ ������, S������ �ڷ� �����̵��� �ߴ�.

        if (theTimingManager.CheckTiming() == 0) //perfect�� ���� ������
        {
            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 5;
            if (playerCollision.collide == 0) //�ε����� �����ڸ����� ���� �� �ε������� �ݴ������� ���� (collision�� rigid body�� �����ߴµ� ���� �������� �ڲ� ���� ����ؼ� �ڵ�� �����ߴ�.)
            {// (�浹�Ȱ����� ��������� �����̸鼭 �� �浹�ؼ� �ᱹ ���������ʴ� ��찡 �߻�)
                Debug.Log("�浹");
                destPos = destPos + new Vector3(dir.x, 0, -dir.z) * 8;
                playerCollision.collide = 1;
            }

        }
        else if (theTimingManager.CheckTiming() == 1) //cool�̸� �� ���� ������
        {
            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 4;
            if (playerCollision.collide == 0) //�ε����� �����ڸ����� ���� �� �ε������� �ݴ������� ���� 
            {
                Debug.Log("�浹");
                destPos = destPos + new Vector3(dir.x, 0, -dir.z) * 7;
                playerCollision.collide = 1;
            }
        }
        else if (theTimingManager.CheckTiming() == 2) //good�̸� ���� ������
        {

            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 3;
            if (playerCollision.collide == 0) //�ε����� �����ڸ����� ���� �� �ε������� �ݴ������� ���� 
            {
                Debug.Log("�浹");
                destPos = destPos + new Vector3(dir.x, 0, -dir.z) * 6;
                playerCollision.collide = 1;
            }
        }
        else if (theTimingManager.CheckTiming() == 3) //bad�� ���� ���� ������
        {

            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 2;
            if (playerCollision.collide == 0) //�ε����� �����ڸ����� ���� �� �ε������� �ݴ������� ���� 
            {
                Debug.Log("�浹");
                destPos = destPos + new Vector3(dir.x, 0, -dir.z) * 5;
                playerCollision.collide = 1;
            }
        }
        else //miss�� �ȿ�����
        {
            TimingManager.judgementRecord[4]++; //miss�� timingmanager�ּ� ī��Ʈ�Ǹ� �߸����� playerController���� ī��Ʈ�ߴ�. (���������� ���â���� �ʿ�)
        }
        
        rotDir = new Vector3(-dir.z, 0f, -dir.x); //������� �ȿ������� ������
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);
        destRot = fakeCube.rotation;

        StartCoroutine(MoveCo());
        StartCoroutine(SpinCo());
       StartCoroutine(RecoilCo());


    }

        IEnumerator MoveCo()
        {

            while (Vector3.SqrMagnitude(transform.position - destPos) >= 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = destPos;


        }
        IEnumerator SpinCo()
        {
            while (Quaternion.Angle(realCube.rotation, destRot) > 0.5f)
            {
                realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime);
                yield return null;
            }

            realCube.rotation = destRot;
        }

        IEnumerator RecoilCo()
        {

            while (realCube.position.y < recoilPosY)
            {
                realCube.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
                yield return null;
            }


            while (realCube.position.y > 0)
            {
                realCube.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
                yield return null;
            }

        }
    

}