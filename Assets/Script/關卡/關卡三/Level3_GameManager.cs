using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level3_GameManager : MonoBehaviour
{
    [Header("任務List")]
    public Quest[] questList;
    public TMP_Text questHintText;
    [Header("關卡檢查點設定")]
    [SerializeField] GameObject[] checkPoints;
    public TMP_Text checkPointText;
    public int checkPointCount;

    [Header("關卡UI設定")]
    public SceneCanvasManager sceneCM;
    GameObject playerComponet;
    int endCase;
    bool check;




    [Header("Debug")]
    public int currnetStatus;
    public bool isEnd;
    float carHp;
    GameObject car;

    void Start()
    {
        StartLevel();
        foreach (GameObject i in checkPoints)
        {
            i.SetActive(false);
        }
        checkPoints[checkPointCount].SetActive(true);
        endCase = -1;
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
                endCase = 1;
            }

        }
        else
        {
            // if (!check)
            // {
            //     check = true;
            //     EndGame(endCase);
            // }

        }

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
        if (checkPointCount < checkPoints.Length)
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
                questHintText.text = $"任務目標:{questList[currnetStatus].description}";
                break;

            case 1:
                questHintText.text = $"任務目標:{questList[currnetStatus].description}, 當前進度{checkPointCount}/{checkPoints.Length}";
                //checkPoints[checkPointCount].SetActive(true);
                if (checkPointCount >= checkPoints.Length)
                {
                    NextStatus(currnetStatus);
                }
                break;
            case 2:
                questHintText.text = $"任務目標{questList[currnetStatus].description}";
                isEnd = true;
                endCase = 0;
                EndGame(endCase);
                break;
        }
    }

    void CaculatePointDis()
    {

        float distance = Vector3.Distance(car.transform.position, checkPoints[checkPointCount].transform.position);
        checkPointText.text = $"目標距離:{(int)distance}m";

    }
    void EndGame(int i)
    {
        StopScene();
        sceneCM.EndLevel(i);
    }
    void StartLevel()
    {
        sceneCM.endUI.SetActive(false);
        sceneCM.startUI.SetActive(true);
        playerComponet = GameObject.Find("Player").GetComponent<PlayerComponets>().PlayerComponet;
        playerComponet.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void StopScene()
    {
        car.GetComponent<CarSetting>().enabled = false;
        car.GetComponent<CarController>().enabled = false;
        car.GetComponent<CarAudioController>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}
