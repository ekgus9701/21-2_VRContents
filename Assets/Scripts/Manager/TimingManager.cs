using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();

    [SerializeField] Transform Center = null; 
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBoxs = null;
    public  static int[] judgementRecord = new int[5];
    EffectManager theEffect;

    // Start is called before the first frame update
    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }
    public int CheckTiming()
    {

        
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;
            
             
            for (int x = 0; x < timingBoxs.Length; x++)
            {
                
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                 
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i);

                    theEffect.JudgementEffect(x);
                    
                    if (x<timingBoxs.Length-1)
                        theEffect.NoteHitEffect();

                    //리듬 맞춘 정도에 따라 PlayerController에서 쓰는 속도가 달라져야해서 switch문을 써서 return 해줬다. 
                    switch (x)
                    {
                        case 0:
                            // Debug.Log("Perfect");
                            judgementRecord[x]++;
                            return x;
                            break;
                        case 1:
                            // Debug.Log("Cool");
                            judgementRecord[x]++;
                            return x;
                            break;
                        case 2:
                            // Debug.Log("Good");
                            judgementRecord[x]++;
                            return x;
                            break;
                        case 3:
                            //Debug.Log("Bad");
                            judgementRecord[x]++;
                            return x;
                            break;
                    }
                   
                   
                    
               
                }
            }
        }
        
       

        return -1;
       

    }

    public int[] GetJudgementRecord()
    {
        return judgementRecord;
    }

    public void MissRecord()
    {
        judgementRecord[4]++;
    }

 public void Initialized()
    {
        
        for(int i=0;i<5;i++) 
            judgementRecord[i] = 0;

    }
}