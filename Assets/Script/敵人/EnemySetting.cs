using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EnemySetting : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator anim;
    public Transform Target;
    PlayerController playerController;
    public int enemyHp;
    float distance;
    [SerializeField] bool isDead, check, isCol;

    Rigidbody rb;
    CapsuleCollider col;

    NavMeshAgent navMeshAgent;

    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerController = GameObject.Find("PlayerControl").GetComponent<PlayerController>();


    }

    // Update is called once per frame
    void Update()
    {
        CheckTarget();   //檢查目標狀態是不是在車上
        if (!isDead)
        {
            if (!isCol)
            {
                TrackTarget();
            }
        }
        else
        {
            if (!check)
            {
                navMeshAgent.enabled = false;
                rb.isKinematic = true;
                anim.SetTrigger("Dying");
                Invoke("DestroyEnemy", 10f);

                col.enabled = false;
                check = true;
            }
        }

    }
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Car") && other.gameObject.GetComponent<CarSetting>().inTheCar) //判斷有沒有碰到汽車，並且汽車上要有人
        {
            navMeshAgent.enabled = false;
            isCol = true;
            Rigidbody carRb = other.gameObject.GetComponent<Rigidbody>();
            if (carRb.velocity.magnitude >= 1.5f)
            {
                enemyHp -= 100;
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
            Debug.Log("Exit");
            isCol = false;
            navMeshAgent.enabled = true;
        }
    }

    void CheckTarget()
    {
        if (!playerController.isIncar)
        {
            Target = GameObject.Find("Player").transform;
            navMeshAgent.stoppingDistance = 2f;
        }
        else
        {
            Target = GameObject.Find("Player").transform.root;
            navMeshAgent.stoppingDistance = 3f;
        }
    }
    void TrackTarget() //跟蹤目標
    {
        navMeshAgent.destination = Target.position;

        distance = Vector3.Distance(this.transform.position, Target.position);
        distance = Mathf.FloorToInt(distance);
        if (distance <= navMeshAgent.stoppingDistance)
        {
            anim.SetBool("Run", false);
            anim.SetBool("Attack",true);
            navMeshAgent.isStopped = true;
            
        }
        else
        {
            anim.SetBool("Attack",false);
            anim.SetBool("Run", true);
            navMeshAgent.isStopped = false;
        }
    }
    void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

}
