using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0; // ������� ��Ʈ ����. 1�д� �� ��Ʈ����.
    double currentTime = 0d; // ���� ������ ���� ������ �߿��ؼ� float���� double

    [SerializeField] Transform tfNoteAppear = null; // ��Ʈ ���� ��ġ ������Ʈ

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

        if (currentTime >= 60d / bpm) // 60/bpm = �� ��Ʈ�� �ð� (120bpm�̶�� �� ��Ʈ�� �ҿ� �ð��� 0.5��)
        {
            GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
           
            t_note.transform.position = tfNoteAppear.position;
            t_note.transform.localScale = new Vector3(1f, 1f, 0f);
            t_note.SetActive(true);

            theTimingManager.boxNoteList.Add(t_note);
            Debug.Log("��");
            currentTime -= 60d / bpm;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("�ߵ�");
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())
                theEffectManager.JudgementEffect(4);
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            //Debug.Log("�ߵ�");
            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
           // Debug.Log("�ߵ�1");
            collision.gameObject.SetActive(false);
        }
    }
}
