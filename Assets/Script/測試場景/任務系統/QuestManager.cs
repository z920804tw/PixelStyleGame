using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentStatus;
    public GameObject questHint;

    public Quest quest;
    TMP_Text questHintText;


    void Start()
    {
        questHintText = questHint.GetComponentInChildren<TextMeshProUGUI>();
        questHintText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void UpdateQuestStatus(int status)
    {
        currentStatus = status;
        questHint.SetActive(true);

        switch (currentStatus)
        {
            case 1:
                Task1Setting();
                break;
            case 2:
                Task2Setting();
                break;
            case 3:
                Task3Setting();
                break;
            case 4:
                Task4Setting();
                break;
            case 5:
                Task5Setting();
                break;
            case 6:
                Task6Setting();
                break;
            case 7:
                Task7Setting();
                break;
            case 8:
                Task8Setting();
                break;
            case 9:
                Task9Setting();
                break;
            default:

                break;
        }
    }

    public void Task1Setting()
    {
        questHintText.text = "跟人員A對話";
        quest.npcList[0].SetActive(true);
    }
    public void Task2Setting()
    {
        questHintText.text = "跟人員B對話";
        quest.npcList[1].SetActive(true);
    }
    public void Task3Setting()
    {
        questHintText.text = "請將貨物搬上貨車";
    }
    public void Task4Setting()
    {
        quest.npcList[1].SetActive(false);
        quest.npcList[2].SetActive(true);
        questHintText.text = "再去跟人員B對話";
    }
    public void Task5Setting()
    {
        questHintText.text = "將貨車開到指定的地點";
        quest.TriggerZone[0].SetActive(true);
        quest.npcList[3].SetActive(true);
    }
    public void Task6Setting()
    {
        questHintText.text = "跟人員C對話";
    }
    public void Task7Setting()
    {
        questHintText.text = "將貨物放置到指定的區域內";
    }
    public void Task8Setting()
    {
        quest.npcList[3].SetActive(false);
        quest.npcList[4].SetActive(true);
        questHintText.text = "再去跟人員C對話";
    }
    public void Task9Setting()
    {
        questHintText.text = "任務結束";
        Invoke("closeHint", 2f);

    }





    void closeHint()
    {
        questHintText.text = string.Empty;
        questHint.SetActive(false);
    }
}
