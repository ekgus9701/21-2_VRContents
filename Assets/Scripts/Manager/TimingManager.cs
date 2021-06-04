using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();

    [SerializeField] Transform Center = null; // 판정 범위의 중심
    [SerializeField] RectTransform[] timingRect = null; // 다양한 판정 범위
    Vector2[] timingBoxs = null; // 판정 범위 최소값 x, 최대값 y
    
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
            
             //판정 순서 : Perfect -> Cool -> Good -> Bad
            for (int x = 0; x < timingBoxs.Length; x++)
            {
                
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    //노트제거
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i);

                    theEffect.JudgementEffect(x);
                    //이펙트연출
                    if (x<timingBoxs.Length-1)
                        theEffect.NoteHitEffect();

                    switch (x)
                    {
                        case 0:
                           // Debug.Log("Perfect");
                            return x;
                            break;
                        case 1:
                           // Debug.Log("Cool");
                            return x;
                            break;
                        case 2:
                           // Debug.Log("Good");
                            return x;
                            break;
                        case 3:
                            //Debug.Log("Bad");
                            return x;
                            break;
                    }
                   // return x;
                    
               
                }
            }
        }

        //theEffect.JudgementEffect(timingBoxs.Length);
        return -1;
      // Debug.Log("Miss");

    }
}