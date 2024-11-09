using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveQuest : MonoBehaviour
{
    // Start is called before the first frame update
    public int activeStatus; //下一個要觸發的任務index
    QuestManager questManager;

    [SerializeField]bool hasActived;
    void Start()
    {
        questManager = GameObject.Find("GameManager").GetComponent<QuestManager>();
    }

    public void ActiveQuestStatus()
    {
        if (!hasActived)
        {
           
            hasActived = true;
            questManager.UpdateQuestStatus(activeStatus);
        }

    }

}
