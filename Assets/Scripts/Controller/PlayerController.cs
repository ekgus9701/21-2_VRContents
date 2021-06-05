using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    TimingManager theTimingManager;
    CameraController theCam;
    PlayerCollision playerCollision;

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
            if (playerCollision.collide == 0) //�ε����� �����ڸ��� ����(collision�� rigid body�� �����ߴµ� ���� �������� �ڲ� ���� ����ؼ� �ڵ�� �����ߴ�.)
            {
                destPos = destPos + new Vector3(dir.x, 0, -dir.z) * 5;
                playerCollision.collide = 1;
            }

        }
        else if (theTimingManager.CheckTiming() == 1) //cool�̸� ����
        {
            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 4;
            if (playerCollision.collide == 0) //�ε����� �����ڸ��� ����
            {
                destPos = destPos + new Vector3(dir.x, 0, -dir.z) * 4;
                playerCollision.collide = 1;
            }
        }
        else if (theTimingManager.CheckTiming() == 2) //good�̸� ���� 
        {

            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 3;
            if (playerCollision.collide == 0) //�ε����� �����ڸ��� ����
            {
                destPos = destPos + new Vector3(dir.x, 0, -dir.z) * 3;
                playerCollision.collide = 1;
            }
        }
        else if (theTimingManager.CheckTiming() == 3) //bad�� ���� ���� ������
        {

            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 2;
            if (playerCollision.collide == 0) //�ε����� �����ڸ��� ����
            {
                destPos = destPos + new Vector3(dir.x, 0, -dir.z) * 2;
                playerCollision.collide = 1;
            }
        }
        else //miss��
        {
            TimingManager.judgementRecord[4]++; //miss�� timingmanager�ּ� ī��Ʈ�Ǹ� �߸����� playerController���� ī��Ʈ�ߴ�.
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