using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("生成參數設定")]
    public LayerMask TargetLayer;


    [SerializeField] float spawnRange;
    [SerializeField] bool isEnter;
    [Header("敵人參數設定")]
    public GameObject enemys;

    [SerializeField] int maxSpawn;
    [SerializeField] float spawnRadius;
    [SerializeField] float spawnTime;
    public int spawnCount;
    float timer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isEnter = Physics.CheckSphere(transform.position, spawnRange, TargetLayer);
        if (isEnter)
        {
            if (spawnCount < maxSpawn)
            {
                timer += Time.deltaTime;
                if (timer >= spawnTime)
                {
                    timer = 0;
                    Vector3 rndPos = RandomPos();
                    GameObject zb = Instantiate(enemys, rndPos, Quaternion.identity);
                    zb.GetComponent<EnemyController>().enemySpawn = this.GetComponent<EnemySpawn>();

                    spawnCount++;

                }
            }
        }

    }

    Vector3 RandomPos()
    {
        Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPoint = new Vector3(transform.position.x + randomPoint.x, transform.position.y + 0.2f, transform.position.z + randomPoint.y);
        return spawnPoint;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }
}
