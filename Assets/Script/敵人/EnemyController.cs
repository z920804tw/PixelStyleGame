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
    Rigidbody rb;
    Collider col;
    EnemyAudioController enemyAudioController;
    PlayerController playerController;

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


    private void Awake()
    {

        playerController = GameObject.Find("PlayerControl").GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        enemyAudioController = GetComponent<EnemyAudioController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            inSightRange = Physics.CheckSphere(transform.position, sightRange, TargetLayer);
            inAttackRange = Physics.CheckSphere(transform.position, attackRange, TargetLayer);
            TargetSet();

            if (!inSightRange && !inAttackRange)  //如果沒有目標近來 就巡邏
            {

                AudioChange(0);
                Patroling();

            }
            else if (inSightRange && !inAttackRange)//如果有目標近來，但還沒到攻擊範圍 就追擊
            {
                AudioChange(1);
                ChaseTarget();
                walkPointSet = false;

            }
            else if (inSightRange && inAttackRange)//如果有目標近來並且盡到攻擊範圍， 就攻擊
            {
                AudioChange(2);
                AttackTarget();
            }
        }
        else
        {
            if (!check)
            {
                check = true;
                agent.enabled = false;
                rb.isKinematic = true;
                col.isTrigger = true;

                anim.SetTrigger("Dying");
                Destroy(this.gameObject, 10f);

            }
        }


    }
    void AudioChange(int i)
    {
        if (enemyAudioController.enemyAudio[i].audioSource.clip != enemyAudioController.enemyAudio[i].audioClip)
        {
            enemyAudioController.StopAllSound();
            enemyAudioController.enemyAudio[i].audioSource.clip = enemyAudioController.enemyAudio[i].audioClip;
            enemyAudioController.enemyAudio[i].audioSource.Play();
        }
    }
    void TargetSet()   //目標狀態設定
    {
        if (!playerController.isIncar)
        {
            Target = GameObject.Find("Player").transform;
            TargetLayer = 1 << 8;
            sightRange = 10f;
            attackRange = 1f;
        }
        else
        {
            Target = playerController.transform.root;
            TargetLayer = 1 << 9;
            sightRange = 20f;
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
            agent.destination = walkPoint;
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
        agent.destination = Target.position;
        agent.speed = 2.5f;
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
    public void AttackSound()
    {
        enemyAudioController.enemyAudio[2].audioSource.PlayOneShot(enemyAudioController.enemyAudio[2].audioClip);
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
