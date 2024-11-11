using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]bool isDead;

    CapsuleCollider col;
    NavMeshAgent navMeshAgent;
    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerController = GameObject.Find("PlayerControl").GetComponent<PlayerController>();


    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.isIncar)
        {
            Target = GameObject.Find("Player").transform;
            navMeshAgent.stoppingDistance=1.5f;
        }
        else
        {
            Target = GameObject.Find("Player").transform.root;
            navMeshAgent.stoppingDistance=4f;
        }
        TrackTarget();
        if (isDead)
        {
            isDead=false;
            anim.SetTrigger("Dying");
            Invoke("DestroyEnemy", 10f);
            navMeshAgent.isStopped = true;
            col.enabled = false;
            Debug.Log("dead");
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            enemyHp -= 100;
            if (enemyHp <= 0)
            {
                isDead=true;
            }
        }
    }
    void TrackTarget()
    {
        navMeshAgent.destination = Target.position;

        distance = Vector3.Distance(this.transform.position, Target.position);
        if (distance < navMeshAgent.stoppingDistance)
        {
            Debug.Log("Stop");
            anim.SetBool("Run", false);
        }
        else
        {
            anim.SetBool("Run", true);
        }
    }
    void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}
