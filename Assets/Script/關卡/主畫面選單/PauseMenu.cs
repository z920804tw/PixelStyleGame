using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mainCanvas;
    public GameObject settingCanvas;
    public SceneCanvasManager sceneCanvasManager;
    bool isIncar;

    void Start()
    {

    }
    void Update()
    {

    }

    // Update is called once per frame

    public void OpenCanvas()
    {
        sceneCanvasManager.pauseUI.SetActive(true);
        BackMain();
        isIncar = GameObject.Find("Player").GetComponent<PlayerComponets>().isInCar();
        if (isIncar)
        {
            GameObject.Find("貨車").GetComponent<CarSetting>().enabled = false;
            GameObject.Find("貨車").GetComponent<CarController>().isPause=true;
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerComponets>().PlayerComponet.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ClosePanel()
    {
        isIncar = GameObject.Find("Player").GetComponent<PlayerComponets>().isInCar();
        if (isIncar)
        {
            GameObject.Find("貨車").GetComponent<CarSetting>().enabled = true;
            GameObject.Find("貨車").GetComponent<CarController>().isPause=false;
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerComponets>().PlayerComponet.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //判斷是不是在大廳
        if (!sceneCanvasManager.isLobby)
        {
            sceneCanvasManager.gameUI.SetActive(true);
        }
        sceneCanvasManager.pauseUI.SetActive(false);
    }

    public void Back(string i)
    {
        if (LevelLoadManager.instance != null)
        {
            LevelLoadManager.instance.LoadScene(i);
        }
    }

    public void OpenSetting()
    {
        mainCanvas.SetActive(false);
        settingCanvas.SetActive(true);
    }

    public void BackMain()
    {
        mainCanvas.SetActive(true);
        settingCanvas.SetActive(false);
        
    }
}
