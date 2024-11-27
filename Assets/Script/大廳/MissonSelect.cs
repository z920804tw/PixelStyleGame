using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissonSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] missionList;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void SelectMission(int i)
    {
        CloseAllList();
        missionList[i].SetActive(true);
    }
    public void StartMission(int i)
    {
        SceneManager.LoadScene(i);
    }

    void CloseAllList()
    {
        foreach (GameObject i in missionList)
        {
            i.SetActive(false);
        }
    }
}
