using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    
   public void BtnPlay()
    {
        GameManager.instance.GameStart();
        this.gameObject.SetActive(false);
        
    }

}
