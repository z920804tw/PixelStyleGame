using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SceneAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider bgVolSlider;
    [SerializeField] Slider enemyVolSlider;
    [SerializeField] Slider carVolSlider;

    [SerializeField] TMP_Text bgVolText;
    [SerializeField] TMP_Text enemyVolText;
    [SerializeField] TMP_Text carVolText;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Initialization")) //判斷有沒有初始化過
        {
            LoadVolume();
            Debug.Log("讀取");
        }
        else
        {
            Debug.Log("讀取失敗");
        }

    }


    public void SetBgVolume()
    {
        float volume = bgVolSlider.value;
        audioMixer.SetFloat("BgVolume", Mathf.Log10(volume) * 20);
 
        bgVolText.text = $"{Mathf.RoundToInt(volume * 100)}";
        PlayerPrefs.SetFloat("BgVolume", volume);
        Debug.Log("設定好背景音量");
    }
    public void SetEnemyVolume()
    {
        float volume = enemyVolSlider.value;
        audioMixer.SetFloat("EnemyVolume", Mathf.Log10(volume) * 20);

        enemyVolText.text = $"{Mathf.RoundToInt(volume * 100)}";
        PlayerPrefs.SetFloat("EnemyVolume", volume);
    }
    public void SetCarVolume()
    {
        float volume = carVolSlider.value;
        audioMixer.SetFloat("CarEngineVolume", Mathf.Log10(volume) * 20);

        carVolText.text = $"{Mathf.RoundToInt(volume * 100)}";
        PlayerPrefs.SetFloat("CarEngineVolume", volume);
    }

    void LoadVolume()
    {
        bgVolSlider.value = PlayerPrefs.GetFloat("BgVolume");
        enemyVolSlider.value = PlayerPrefs.GetFloat("EnemyVolume");
        carVolSlider.value = PlayerPrefs.GetFloat("CarEngineVolume");

        SetBgVolume();
        SetEnemyVolume();
        SetCarVolume();
    }
}
