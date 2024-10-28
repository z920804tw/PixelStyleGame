using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum TalkStatus
{
    Start,
    Wait,
    End
}
public class DialogueSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent TalkEvent;
    public TalkStatus talkStatus;
    public GameObject[] talkPages;

    public GameObject playerComponet;
    public GameObject talkCanvas;

    int waitIndex=0;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CloseAllPage()
    {
        foreach (GameObject i in talkPages)
        {
            i.SetActive(false);
        }
    }

    public void NextPage(int i)
    {
        CloseAllPage();
        talkPages[i].SetActive(true);
    }
    public void EndTalk()
    {
        CloseAllPage();
        StartPlayerControl();
        talkStatus= TalkStatus.End;

    }

    public void CheckTalkStatus()   //檢查現在對話進度
    {
        switch (talkStatus)
        {
            case TalkStatus.Start:
                Debug.Log("start");
                talkPages[0].SetActive(true);
                break;
            case TalkStatus.Wait:
                Debug.Log("wait");
                talkPages[waitIndex].SetActive(true);
                break;
            case TalkStatus.End:
                Debug.Log("end");
                talkPages[talkPages.Length - 1].SetActive(true);
                break;
        }
    }

    public void SetTalkStatusWait(int i)  //有選擇時，如果選擇no的話會用到
    {
        CloseAllPage();
        talkCanvas.SetActive(false);

        waitIndex=i;
        talkStatus= TalkStatus.Wait;
        StartPlayerControl();
    }


    void StartPlayerControl()       //對話結束開啟玩家控制
    {
        playerComponet.SetActive(true);
        GameObject.Find("TalkInteraction").GetComponent<DialogueInteractionController>().isTalking = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



}
