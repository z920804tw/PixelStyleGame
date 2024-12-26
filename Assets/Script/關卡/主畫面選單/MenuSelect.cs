using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Menus;
    

    void Start()
    {
        SelectMenu(0);
    }
    private void Update()
    {

    }

    public void SelectMenu(int i)
    {
        CloseAllMenu();
        switch (i)
        {
            //開始選單
            case 0:
                Menus[0].SetActive(true);
                break;

            //設定選單
            case 1:
                Menus[1].SetActive(true);
                break;
            case 2:
                Menus[2].SetActive(true);
            break;

        }
    }

    public void CloseAllMenu()
    {
        foreach (GameObject i in Menus)
        {
            i.SetActive(false);
        }
    }

    public void StartGame(string i)
    {
        if (LevelLoadManager.instance != null)
        {
            LevelLoadManager.instance.LoadScene(i);
        }

    }
    public void QuitGame()
    {
        Application.Quit();
    }


}
