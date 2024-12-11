using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("場景UI物件設定")]
    public GameObject startUI;
    public GameObject gameUI;
    public GameObject endUI;
    [Header("物件UI參數設定")]
    public TMP_Text endStatusText;
    public TMP_Text timerText;
    public TMP_Text timeCostText;
    public TMP_Text kiilCountText;

    public GameObject backBtn;
    public GameObject restartBtn;

    GameObject playerComponet;

    bool isEnd;
    float time;
    void Start()
    {
        endUI.SetActive(false);
        startUI.SetActive(true);
        playerComponet = GameObject.Find("Player").GetComponent<PlayerComponets>().PlayerComponet;
        playerComponet.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnd && gameUI.activeSelf)
        {
            if (timerText != null)
            {
                time += Time.deltaTime;
                timerText.text = $"時間:{(int)time}秒";
            }
        }

    }
    public void EndLevel(int i)
    {
        isEnd = true;
        gameUI.SetActive(false);
        endUI.SetActive(true);


        timeCostText.text = $"花費時間{(int)time}秒";
        switch (i)
        {
            case 0:
                endStatusText.text = $"任務完成";
                backBtn.SetActive(true);
                break;
            case 1:
                endStatusText.text = "任務失敗";
                restartBtn.SetActive(true);
                break;
        }

    }

    public void backToLobby()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartLevel()
    {
        startUI.SetActive(false);
        gameUI.SetActive(true);

        playerComponet.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
