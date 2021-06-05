using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{

    public float Friction = 0f; //ĳ���Ͱ� �ʹ� �̲������� �������� �߰��ߴ�. 
  
    void Start()
    {
        PhysicMaterial m = new PhysicMaterial();

        m.staticFriction = Friction;
        m.dynamicFriction = Friction;
        GetComponent<Collider>().material = m;
    }

    
}
