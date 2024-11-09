using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> loadedItems = new List<GameObject>(); // 存放貨箱內的物品
    public int maxCapacity;// 設定貨箱的最大容量

    ActiveQuest activeQuest;

    private void Start()
    {
        activeQuest = GetComponent<ActiveQuest>();
    }
    private void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && loadedItems.Count < maxCapacity)
        {
            other.transform.SetParent(this.gameObject.transform); // 將物品設為貨箱的子物件
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            loadedItems.Add(other.gameObject);
            if (activeQuest != null)
            {
                CheckCargoCount();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            loadedItems.Remove(other.gameObject);
        }
    }

    void CheckCargoCount()
    {
        if (loadedItems.Count >= maxCapacity)
        {
            activeQuest.ActiveQuestStatus();
        }
    }

}
