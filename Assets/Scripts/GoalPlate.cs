using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlate : MonoBehaviour
{
    AudioSource theAudio;
    NoteManager theNote;
    Result theResult;
    int check = 0;
    
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        theNote = FindObjectOfType<NoteManager>();
        theResult = FindObjectOfType<Result>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (check == 0) //������ �Ҹ��� ������ ����� check ������ �Ἥ  �ѹ��� �︮�� ����
            {
                theAudio.Play();
                PlayerController.s_canPressKey = false;
                theNote.RemoveNote();
                check++;
                theResult.ShowResult();
            }
        }
    }
}
