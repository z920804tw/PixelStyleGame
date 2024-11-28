using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyController enemyController;
    void Start()
    {
        enemyController=transform.root.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        enemyController.Attack();
    }
}
