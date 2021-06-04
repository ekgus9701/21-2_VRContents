
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Transform thePlayer = null;
    [SerializeField] float followSpeed = 5;

    Vector3 playerDistance = new Vector3();
    Vector3 t_destPos = new Vector3();

    float hitDistance = 0;
    [SerializeField] float zoomDistance = -1.25f;
    // Start is called before the first frame update
    void Start()
    {
        playerDistance = transform.position - thePlayer.position; //ī�޶� ��ġ���� �÷��̾��� ��ġ ��
        t_destPos = thePlayer.position + playerDistance + (transform.right * hitDistance);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            t_destPos = thePlayer.position + playerDistance + (transform.forward * hitDistance);//transform.forward * hitDistance: ī�޶� �������� �� ����
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            t_destPos = thePlayer.position + playerDistance + (transform.right * hitDistance); //���������� �ܶ���

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            t_destPos = thePlayer.position + playerDistance + (-transform.right * hitDistance); //�������� �ܶ���

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            t_destPos = thePlayer.position + playerDistance + (-transform.forward * hitDistance); //�������� �ܶ���
        }


        transform.position = Vector3.Lerp(transform.position, t_destPos, followSpeed * Time.deltaTime);
    }

    public IEnumerator ZoomCam()
    {
        hitDistance = zoomDistance;

        yield return new WaitForSeconds(0.15f); //0.15�� ��ٸ�

        hitDistance = 0;
    }
}
