using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level1_GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Quest[] questList;
    public TMP_Text questHintText;

    public CheckPonit[] checkPoints;
    public int checkPointCount;
    public int currnetStatus;
    public bool isEnd;

    void Start()
    {
        checkPoints = FindObjectsOfType<CheckPonit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnd)
        {
            switch (currnetStatus)
            {
                case 0:
                    questHintText.text = questList[currnetStatus].description;
                    break;

                case 1:
                    questHintText.text = questList[currnetStatus].description+ $",當前進度{checkPointCount}/{checkPoints.Length}";
                    if (checkPoints[0].isFinish && checkPoints[1].isFinish)
                    {
                        NextStatus(currnetStatus);
                    }
                    break;
                case 2:
                    questHintText.text = questList[currnetStatus].description ;
                    isEnd = true;
                    break;

            }

        }
        else
        {

        }

    }

    public void NextStatus(int i)
    {
        if (questList[i].isActived == false)
        {
            questList[i].isActived = true;
            currnetStatus++;
        }


    }
    public void AddCount()
    {
        checkPointCount++;
    }



}
