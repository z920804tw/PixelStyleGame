using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level1_GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("任務List")]
    public Quest[] questList;
    public TMP_Text questHintText;
    [Header("關卡檢查點設定")]
    [SerializeField] GameObject[] checkPoints;
    public TMP_Text checkPointText;
    public int checkPointCount;
    public float checkPonitDistance;


    [Header("汽車相關設定")]
    [SerializeField] float carHp;
    GameObject car;

    [Header("Debug")]
    public int currnetStatus;
    public bool isEnd;

    void Start()
    {
        foreach (GameObject i in checkPoints)
        {
            i.SetActive(false);
        }
        checkPoints[checkPointCount].SetActive(true);

        car = GameObject.Find("貨車");
        UpdateQuest(currnetStatus); //預設0
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnd)
        {

            if (checkPointCount < checkPoints.Length)  //如果還有檢查點，就會一直更新檢查點距離
            {
                CaculatePointDis();
            }

            carHp = car.GetComponent<CarSetting>().carHp;
            if (carHp <= 0)  //如果車輛血量=0就結束
            {
                isEnd = true;
            }

        }
        else { }

    }

    public void NextStatus(int i)   //切換下一個任務
    {
        if (questList[i].isActived == false)
        {
            questList[i].isActived = true;
            currnetStatus++;
            UpdateQuest(currnetStatus);
        }
    }
    public void AddCount()  //增加檢查點
    {
        checkPointCount++;
        UpdateQuest(currnetStatus);  //每當有一個checkPoint完成，就會去更新一次
        if (checkPointCount<checkPoints.Length)
        {
            checkPoints[checkPointCount].SetActive(true);
        }
    }

    public void UpdateQuest(int i)
    {
        currnetStatus = i;
        switch (i)
        {
            case 0:
                questHintText.text = questList[currnetStatus].description;
                break;

            case 1:
                questHintText.text = questList[currnetStatus].description + $",當前進度{checkPointCount}/{checkPoints.Length}";
                if (checkPointCount >= checkPoints.Length)
                {
                    NextStatus(currnetStatus);
                }
                break;
            case 2:
                questHintText.text = questList[currnetStatus].description;
                isEnd = true;
                break;
        }
    }

    void CaculatePointDis()
    {

        float distance = Vector3.Distance(car.transform.position, checkPoints[checkPointCount].transform.position);
        checkPointText.text = $"目標距離:{(int)distance}m";

    }



}
