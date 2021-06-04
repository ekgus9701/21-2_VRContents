using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0; // 리듬게임 비트 단위. 1분당 몇 비트인지.
    double currentTime = 0d; // 리듬 게임은 오차 적은게 중요해서 float보단 double

    [SerializeField] Transform tfNoteAppear = null; // 노트 생성 위치 오브젝트

    TimingManager theTimingManager;
    EffectManager theEffectManager;

    void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();

    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm) // 60/bpm = 한 비트당 시간 (120bpm이라면 한 비트당 소요 시간은 0.5초)
        {
            GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
           
            t_note.transform.position = tfNoteAppear.position;
            t_note.transform.localScale = new Vector3(1f, 1f, 0f);
            t_note.SetActive(true);

            theTimingManager.boxNoteList.Add(t_note);
            Debug.Log("잘");
            currentTime -= 60d / bpm;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("잘됨");
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())
                theEffectManager.JudgementEffect(4);
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            //Debug.Log("잘됨");
            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
           // Debug.Log("잘됨1");
            collision.gameObject.SetActive(false);
        }
    }
}
