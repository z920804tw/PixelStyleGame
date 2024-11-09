using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerZone : MonoBehaviour
{
    // Start is called before the first frame update
    ActiveQuest activeQuest;
    void Start()
    {
        activeQuest = GetComponent<ActiveQuest>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car") && activeQuest != null)
        {
            activeQuest.ActiveQuestStatus();
        }
    }
}
