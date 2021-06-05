using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{

    public float Friction = 0f; //캐릭터가 너무 미끄러져서 마찰력을 추가했다. 
  
    void Start()
    {
        PhysicMaterial m = new PhysicMaterial();

        m.staticFriction = Friction;
        m.dynamicFriction = Friction;
        GetComponent<Collider>().material = m;
    }

    
}
