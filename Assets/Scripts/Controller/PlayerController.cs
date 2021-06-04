using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    TimingManager theTimingManager;
    CameraController theCam;

    //�̵�
    [SerializeField] float moveSpeed = 50;
    Vector3 dir = new Vector3();
    Vector3 destPos = new Vector3();

    //ȸ��
    [SerializeField] float spinSpeed = 270;
    Vector3 rotDir = new Vector3();
    Quaternion destRot = new Quaternion();

   

    //�ݵ�
    [SerializeField] float recoilPosY = 0.25f;
    [SerializeField] float recoilSpeed = 0.25f;
    

    //��Ÿ
    [SerializeField] Transform fakeCube = null;
    [SerializeField] Transform realCube = null;


    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        theCam = FindObjectOfType<CameraController>();
    }

    void Update()
    {
        float moveZ = 0f;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {

            StartAction();

        }
    }

    void StartAction()
    {
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        if (theTimingManager.CheckTiming() == 0) //perfect�� ���� ������
        {

            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 5;

        }
        else if (theTimingManager.CheckTiming() == 1) //cool�̸� ����
        {

            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 4;
        }
        else if (theTimingManager.CheckTiming() == 2) //good�̸� ���� 
        {

            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 3;
        }
        else if (theTimingManager.CheckTiming() == 3) //bad�� ���� ���� ������
        {

            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 2;
        }
        
        rotDir = new Vector3(-dir.z, 0f, -dir.x);
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
        // �ö�
        while (realCube.position.y < recoilPosY)
        {
            realCube.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        // ������
        while (realCube.position.y > 0)
        {
            realCube.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

       // realCube.localPosition = new Vector3(0, 0, 0);
    }

}