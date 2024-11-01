using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerData playerData;
    public int currentMoney, currentLife;
    public string PlayerName;
    string filePath;
    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "savefile1.json");
        playerData = LoadData();
        ApplyInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetInfo();
            SaveDate(playerData);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (playerData != null)
            {
                playerData = LoadData();
                ApplyInfo();

            }
        }
    }
    void ApplyInfo()
    {
        currentLife = playerData.Life;
        currentMoney = playerData.money;
        PlayerName = playerData.Name;

       GameObject.Find("Player").transform.position= playerData.playerTransform;
    }

    void SetInfo()
    {
        playerData.playerTransform = GameObject.Find("Player").transform.position;
        playerData.money = currentMoney;
        playerData.Life = currentLife;
        playerData.Name = PlayerName;
    }
    public void SaveDate(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, json);
        Debug.Log(filePath);
        Debug.Log("存檔成功");
    }

    public PlayerData LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

            Debug.Log("讀取成功");
            return playerData;
        }
        else
        {
            Debug.Log("讀取失敗");
            return null;
        }


    }
}
