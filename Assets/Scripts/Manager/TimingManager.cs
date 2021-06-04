using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();

    [SerializeField] Transform Center = null; // ���� ������ �߽�
    [SerializeField] RectTransform[] timingRect = null; // �پ��� ���� ����
    Vector2[] timingBoxs = null; // ���� ���� �ּҰ� x, �ִ밪 y
    
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
            
             //���� ���� : Perfect -> Cool -> Good -> Bad
            for (int x = 0; x < timingBoxs.Length; x++)
            {
                
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    //��Ʈ����
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i);

                    theEffect.JudgementEffect(x);
                    //����Ʈ����
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