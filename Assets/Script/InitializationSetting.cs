using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationSetting : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        //初始化設定，只會在第一次開遊戲時執行
        if (!PlayerPrefs.HasKey("Initialization"))
        {
            PlayerPrefs.SetInt("Initialization", 1);
            PlayerPrefs.SetFloat("BgVolume", 0.5f);
            PlayerPrefs.SetFloat("EnemyVolume", 0.5f);
            PlayerPrefs.SetFloat("CarEngineVolume", 0.5f);
            PlayerPrefs.Save();
            Debug.Log("初始化完成");
            if (LevelLoadManager.instance != null)
            {
                LevelLoadManager.instance.LoadScene("Menu");
            }
        }
        else
        {
            if (LevelLoadManager.instance != null)
            {
                LevelLoadManager.instance.LoadScene("Menu");
            }
        }

    }

}
