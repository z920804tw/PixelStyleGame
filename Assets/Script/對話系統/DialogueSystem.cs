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

    [Header("事件")]
    public UnityEvent TalkEvent;

    [Header("參數設定")]
    public TalkStatus talkStatus;
    public GameObject[] talkPages;

    public DialogueList dialogueList;
    public TextAsset textAsset;

    public GameObject talkCanvas;

    public int currentIndex = 0;

    GameObject playerComponet;


    void Start()
    {
        playerComponet = GameObject.Find("PlayerComponets").gameObject;
        GetJsonInfo(textAsset);
    }


    void GetJsonInfo(TextAsset textAsset)   //取得json角色對話
    {
        if (textAsset != null)
        {
            dialogueList = JsonUtility.FromJson<DialogueList>(textAsset.text); // 直接讀取 textAsset 的內容
            
            //Debug.Log("Dialogue loaded successfully.");
        }
        else { }
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
        currentIndex++;
    }
    public void EndTalk()
    {
        CloseAllPage();
        StartPlayerControl();
        talkStatus = TalkStatus.End;

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
                talkPages[currentIndex].SetActive(true);
                break;
            case TalkStatus.End:
                Debug.Log("end");
                talkPages[talkPages.Length - 1].SetActive(true);
                break;
        }
    }

    public void SetTalkStatusWait()  //有選擇時，如果選擇no的話會用到
    {
        CloseAllPage();
        talkCanvas.SetActive(false);
        talkStatus = TalkStatus.Wait;
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
