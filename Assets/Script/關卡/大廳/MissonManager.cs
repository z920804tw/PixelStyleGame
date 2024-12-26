using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissonManager : MonoBehaviour
{
    // Start is called before the first frame updatepublic QuestInfoList EasyQuestInfoList;
    public GameObject questCanvas;
    public GameObject[] missionDifficulty;
    Animator anim;

    GameObject playerComponets;
    void Start()
    {
        playerComponets = GameObject.Find("PlayerComponets");
        anim = GetComponent<Animator>();
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
        // anim.SetBool("Open", true);
        if (playerComponets != null)
        {
            playerComponets.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void CloseQuestCanvas()
    {

        anim.SetBool("Open", false);
        Invoke("CloseCanvas", 1.1f);

    }
    void CloseCanvas()
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
