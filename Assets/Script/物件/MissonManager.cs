using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissonManager : MonoBehaviour
{
    // Start is called before the first frame updatepublic QuestInfoList EasyQuestInfoList;
    public GameObject questCanvas;
    public GameObject[] missionDifficulty;

    GameObject playerComponets;
    void Start()
    {
        playerComponets = GameObject.Find("PlayerComponets");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SelectDifficutly(int i)
    {   
        CloseAllList();
        missionDifficulty[i].SetActive(true);
    }

    public void OpenQuestCanvas()
    {
        questCanvas.SetActive(true);
        if (playerComponets != null)
        {
            playerComponets.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void CloseQuestCanvas()
    {
        questCanvas.SetActive(false);
        if (playerComponets != null)
        {
            playerComponets.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void CloseAllList()
    {
        foreach (GameObject i in missionDifficulty)
        {
            i.SetActive(false); 
        }
    }
}
