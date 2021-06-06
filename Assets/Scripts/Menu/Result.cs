using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject goUI = null;
    [SerializeField] Text[] txtCount = null;

    TimingManager theTiming;
        
    void Start()
    {
        theTiming = FindObjectOfType<TimingManager>();
    }
    public void ShowResult()
    {
        
        goUI.SetActive(true);

        for (int i = 0; i < txtCount.Length; i++)
            txtCount[i].text = "0";

        int[] t_judgement = theTiming.GetJudgementRecord();

        for(int i = 0; i < txtCount.Length; i++)
        {
            txtCount[i].text = string.Format("{0:#,##0}", t_judgement[i]);
        }
    }

    public void BtnMainMenu()
    {
        goUI.SetActive(false);
        GameManager.instance.MainMenu();
        
    }

    public void QuitGame()
    {
        GameManager.instance.GameQuit();
    }
   
}
