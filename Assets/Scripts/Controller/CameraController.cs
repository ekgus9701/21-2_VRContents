
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        transform.position = player.transform.position + new Vector3(35, 35, 0); //ī�޶� ĳ���͸� ����ٴϰ� �ߴ�.
    }

}
