using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    bool check;
    [Header("敵人組件設定")]
    public NavMeshAgent agent;
    public Transform Target;
    public LayerMask TargetLayer;
    public Animator anim;
    public EnemySpawn enemySpawn;
    Rigidbody rb;
    Collider col;
    EnemyAudioController enemyAudioController;
    PlayerComponets playerComponets;
    SceneCanvasManager sceneCM;

    [Header("敵人參數設定")]
    public int enemyHp;

    //巡邏
    Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //狀態
    public float sightRange, attackRange;
    public bool inSightRange, inAttackRange;
    bool isDead;

    public bool isEnd;


    private void Awake()
    {

        playerComponets = GameObject.Find("Player").GetComponent<PlayerComponets>();
        sceneCM = GameObject.Find("SceneCanvas").GetComponent<SceneCanvasManager>();

        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        enemyAudioController = GetComponent<EnemyAudioController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!isEnd)
        {
            if (sceneCM.endUI.activeSelf)
            {
                isEnd=true;
            }
            if (!isDead)
            {
                inSightRange = Physics.CheckSphere(transform.position, sightRange, TargetLayer);
                inAttackRange = Physics.CheckSphere(transform.position, attackRange, TargetLayer);
                TargetSet();

                if (!inSightRange && !inAttackRange)  //如果沒有目標近來 就巡邏
                {

                    enemyAudioController.AudioChange(0);
                    Patroling();

                }
                else if (inSightRange && !inAttackRange)//如果有目標近來，但還沒到攻擊範圍 就追擊
                {
                    enemyAudioController.AudioChange(1);
                    ChaseTarget();
                    walkPointSet = false;

                }
                else if (inSightRange && inAttackRange)//如果有目標近來並且盡到攻擊範圍， 就攻擊
                {
                    enemyAudioController.StopAllSound();
                    AttackTarget();
                }

                //距離超過80會消失
                float distance = Vector3.Distance(this.transform.position, Target.transform.position);
                if (distance > 80f)
                {
                    Destroy(this.gameObject);
                    if (enemySpawn != null)
                    {
                        enemySpawn.spawnCount--;

                    }
                }
            }
            else
            {
                if (!check)
                {
                    check = true;
                    anim.SetTrigger("Dying");
                    Destroy(this.gameObject, 10f);

                    if (enemySpawn != null)
                    {
                        enemySpawn.spawnCount--;
                    }

                }
            }
        }
        else //遊戲結束後，敵人就停止動作
        {
            agent.isStopped = true;
            enemyAudioController.enemySound.volume = 0;
        }



    }

    void TargetSet()   //目標狀態設定
    {
        bool isIncar = playerComponets.isInCar();
        if (!isIncar)
        {
            Target = GameObject.Find("Player").transform;
            TargetLayer = 1 << 8;
            sightRange = 15f;
            attackRange = 1f;
        }
        else
        {
            Target = GameObject.Find("貨車").transform;
            TargetLayer = 1 << 9;
            sightRange = 40f;
            attackRange = 2f;
        }
    }
    void Patroling()  //巡邏模式
    {
        anim.SetBool("Run", false);
        anim.SetBool("Attack", false);
        if (!walkPointSet)
        {
            float rndX = Random.Range(-walkPointRange, walkPointRange);
            float rndZ = Random.Range(-walkPointRange, walkPointRange);
            walkPoint = new Vector3(transform.position.x + rndX, transform.position.y, transform.position.z + rndZ);
            walkPointSet = true;
        }
        else
        {
            agent.SetDestination(walkPoint);
            agent.speed = 1f;
            Vector3 distance = transform.position - walkPoint;
            if (distance.magnitude < 1f)
            {
                walkPointSet = false;
            }
        }

    }
    void ChaseTarget()  //追擊模式
    {
        agent.isStopped = false;
        anim.SetBool("Attack", false);
        anim.SetBool("Run", true);
        agent.SetDestination(Target.position);
        agent.speed = 3f;
    }
    void AttackTarget() //攻擊模式
    {
        anim.SetBool("Run", false);
        anim.SetBool("Attack", true);
        agent.isStopped = true;

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            Rigidbody CarRb = other.gameObject.GetComponent<Rigidbody>();
            if (CarRb.velocity.magnitude >= 2.5f)
            {
                enemyHp -= 100;
                agent.updatePosition = false;
                agent.updateRotation = false;
                if (enemyHp <= 0)
                {
                    agent.isStopped = true;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    col.isTrigger = true;
                    isDead = true;
                }
            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            agent.updatePosition = true;
            agent.updateRotation = true;
        }
    }
    public void Attack()
    {
        enemyAudioController.PlaySound(2);
        if (Target.gameObject.CompareTag("Car"))
        {
            Target.GetComponent<CarSetting>().TakeDmg(2);
            Debug.Log("打的是汽車");
        }
        else
        {
            Debug.Log("打的是玩家");
        }
    }


    private void OnDrawGizmosSelected() //顯示範圍
    {
        // 設定 Gizmo 的顏色為藍色，表示視野範圍
        Gizmos.color = Color.blue;
        // 繪製視野範圍的球體
        Gizmos.DrawWireSphere(transform.position, sightRange);

        // 設定 Gizmo 的顏色為紅色，表示攻擊範圍
        Gizmos.color = Color.red;
        // 繪製攻擊範圍的球體
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
