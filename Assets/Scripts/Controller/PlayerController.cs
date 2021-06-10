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
        realCube.localPosition = new Vector3(39.46f, 0, -22.6f); //성공 후 메인메뉴로 갔을 때 재시작시 큐브가 있던 처음 위치로 돌아가게함
        s_canPressKey = true;
        gameManager.GameTime = 151; //다시 시작할 때 게임타임도 초기화되어야해서 초기화해줌
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
        //내가짠코드
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal")); //내 상황에 맞게 W 누르면 앞으로, A누르면 왼쪽으로, D누르면 오른쪽, S누르면 뒤로 움직이도록 했다.

        if (theTimingManager.CheckTiming() == 0) //perfect면 많이 움직임
        {
            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 5;
            if (playerCollision.collide == 0) //부딪히면 원래자리보다 조금 더 부딪힌곳과 반대쪽으로 복귀 (collision과 rigid body를 설정했는데 무슨 문제인지 자꾸 벽을 통과해서 코드로 구현했다.)
            {// (충돌된곳에서 벗어나기위해 움직이면서 또 충돌해서 결국 움직이지않는 경우가 발생)
                Debug.Log("충돌");
                destPos = destPos + new Vector3(dir.x, 0, -dir.z) * 8;
                playerCollision.collide = 1;
            }

        }
        else if (theTimingManager.CheckTiming() == 1) //cool이면 덜 많이 움직임
        {
            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 4;
            if (playerCollision.collide == 0) //부딪히면 원래자리보다 조금 더 부딪힌곳과 반대쪽으로 복귀 
            {
                Debug.Log("충돌");
                destPos = destPos + new Vector3(dir.x, 0, -dir.z) * 7;
                playerCollision.collide = 1;
            }
        }
        else if (theTimingManager.CheckTiming() == 2) //good이면 조금 움직임
        {

            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 3;
            if (playerCollision.collide == 0) //부딪히면 원래자리보다 조금 더 부딪힌곳과 반대쪽으로 복귀 
            {
                Debug.Log("충돌");
                destPos = destPos + new Vector3(dir.x, 0, -dir.z) * 6;
                playerCollision.collide = 1;
            }
        }
        else if (theTimingManager.CheckTiming() == 3) //bad면 아주 조금 움직임
        {

            destPos = transform.position + new Vector3(-dir.x, 0, dir.z) * 2;
            if (playerCollision.collide == 0) //부딪히면 원래자리보다 조금 더 부딪힌곳과 반대쪽으로 복귀 
            {
                Debug.Log("충돌");
                destPos = destPos + new Vector3(dir.x, 0, -dir.z) * 5;
                playerCollision.collide = 1;
            }
        }
        else //miss시 안움직임
        {
            TimingManager.judgementRecord[4]++; //miss가 timingmanager애서 카운트되면 잘못나와 playerController에서 카운트했다. (성공했을시 결과창에서 필요)
        }
        
        rotDir = new Vector3(-dir.z, 0f, -dir.x); //생각대로 안움직여서 수정함
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