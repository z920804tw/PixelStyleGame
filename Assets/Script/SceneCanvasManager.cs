using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text timerText;
    bool isEnd;
    public float time;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnd)
        {
            if (timerText != null)
            {
                time += Time.deltaTime;
                timerText.text = $"時間:{(int)time}秒";
            }
        }

    }
    public void End()
    {
        isEnd = true;
    }

    public void backToLobby()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
