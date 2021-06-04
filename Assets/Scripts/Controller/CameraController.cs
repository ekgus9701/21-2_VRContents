
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
        playerDistance = transform.position - thePlayer.position; //Ä«¸Þ¶ó À§Ä¡¿¡¼­ ÇÃ·¹ÀÌ¾îÀÇ À§Ä¡ »­
        t_destPos = thePlayer.position + playerDistance + (transform.right * hitDistance);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            t_destPos = thePlayer.position + playerDistance + (transform.forward * hitDistance);//transform.forward * hitDistance: Ä«¸Þ¶ó Á¤¸éÀ¸·Î ÁÜ ¶¯±è
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            t_destPos = thePlayer.position + playerDistance + (transform.right * hitDistance); //¿À¸¥ÂÊÀ¸·Î ÁÜ¶¯±è

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            t_destPos = thePlayer.position + playerDistance + (-transform.right * hitDistance); //¿ÞÂÊÀ¸·Î ÁÜ¶¯±è

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            t_destPos = thePlayer.position + playerDistance + (-transform.forward * hitDistance); //µÚÂÊÀ¸·Î ÁÜ¶¯±è
        }


        transform.position = Vector3.Lerp(transform.position, t_destPos, followSpeed * Time.deltaTime);
    }

    public IEnumerator ZoomCam()
    {
        hitDistance = zoomDistance;

        yield return new WaitForSeconds(0.15f); //0.15ÃÊ ±â´Ù¸²

        hitDistance = 0;
    }
}
