using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   private float GameTime = 121;
    public Text GameTimeText;

    // Update is called once per frame
    void Update()
    {
       
        if ((int)GameTime > 0)
        {
            GameTime -= Time.deltaTime;
            //Debug.Log((int)GameTime);
            GameTimeText.text = "Time: " + (int)GameTime;
        }
        
    }
}
