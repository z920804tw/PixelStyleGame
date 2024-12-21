using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoadManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static LevelLoadManager instance;
    public GameObject loadCanvas;
    public Image loadBar;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        // loadBar.fillAmount = Mathf.MoveTowards(loadBar.fillAmount, target, 3 * Time.deltaTime);
    }
    //讀取關卡
    public async void LoadScene(string sceneName)
    {
        loadBar.fillAmount = 0;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false; //自動跳轉

        loadCanvas.SetActive(true);
        do
        {
            await Task.Delay(100);
            loadBar.fillAmount = Mathf.MoveTowards(loadBar.fillAmount, scene.progress, 3 * Time.deltaTime);
        } while (scene.progress < 0.9f);
        loadBar.fillAmount = 1;

        scene.allowSceneActivation = true; //自動跳轉
        await Task.Delay(100);

        loadCanvas.SetActive(false);



    }
}
