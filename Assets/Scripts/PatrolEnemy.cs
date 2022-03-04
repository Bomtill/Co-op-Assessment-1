using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    Transform targetPlayer;
    Transform lastKnowPosition;
    Animator anim;

    [Tooltip("Tick if the character is a Scientist, instead of a Guard")]
    [SerializeField] bool isScientist;

    [SerializeField] float moveSpeed, alertTime, SearchTime, attackTime, patrolWaitTime, returnToPatrolTime;

    enum States {IDLE, PATROL, SEARCH, ATTACK, ALARMED, RUNAWAY, PAUSED, REWIND }
    [SerializeField] States currentState;

    bool isAlert = false;
    bool isSearching = false;
    public bool canSeePlayer;



    [Header("PatrolSettings")]
    [SerializeField] bool shouldPatrol = true;
    public List<Vector3> patrolPoint = new List<Vector3>();
    int currentPatrolPoint = 0;

    // Return set here as more performant
    WaitForEndOfFrame endFrame = new WaitForEndOfFrame();

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        //viewCone = GetComponentInChildren<MeshCollider>();
        currentState = States.IDLE;
        StartCoroutine(SM());
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("ForwardSpeed", agent.velocity.magnitude);
    }

    IEnumerator SM()
    {
        while (true)
        {
            yield return StartCoroutine(currentState.ToString());
        }
    }
     
    IEnumerator IDLE()
    {
        while (currentState == States.IDLE)
        {
            yield return new WaitForSeconds(patrolWaitTime);
            if (shouldPatrol)
            {
                currentState = States.PATROL;
            }

        } 
        yield return null;
    }
    IEnumerator PATROL()
    {
        agent.SetDestination(patrolPoint[currentPatrolPoint]);
        while (currentState == States.PATROL)
        {            
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoint.Count;
                currentState = States.IDLE;
            }
            yield return endFrame;
        }
        yield return null;
    }
    IEnumerator RUNAWAY()
    {
        // runawy to alert some guards or find an alarm or hide
        yield return null;
    }

    IEnumerator SEARCH()
    {
        isSearching = true;
        yield return null;
    }
    IEnumerator ATTACK()
    {
        Debug.Log("Game Over");
        yield return endFrame;
    }
    IEnumerator ALARMED()
    {
        // Exlamation point ! like MGS
        // anim.play caution or look animation
        Debug.Log("huh, what are you doing!");
        isAlert = true;
        while (currentState == States.ALARMED)
        {
            if (isScientist)
            {
                currentState = States.RUNAWAY;
            } else
            {
                StartCoroutine(AttackTimeCountdown());
            }
        }
        yield return endFrame;
    }
    IEnumerator PAUSED() // for when players pause time
    {
        
        yield return null;
    }
    IEnumerator REWIND() // for when players rewind time
    {
        yield return null;
    }
    IEnumerator AlarmedCountdown()
    {

        yield return new WaitForSeconds(alertTime);
        currentState = States.ALARMED;
        yield return endFrame;
    }

    IEnumerator AttackTimeCountdown()
    {
        yield return new WaitForSeconds(attackTime);
        currentState = States.ATTACK;
        yield return endFrame;
    }
    IEnumerator ReturnToPatrol()
    {
        yield return new WaitForSeconds(returnToPatrolTime);
        currentState = States.PATROL;
        yield return endFrame;
    }

    public void PlayerSeen()
    {
        if (isAlert)
        {
            agent.isStopped = true;
            currentState = States.ALARMED;
            StartCoroutine(AttackTimeCountdown());
            
        } else
        {
            agent.isStopped = true;
            canSeePlayer = true;
            anim.SetBool("IsAlert", true);
            StartCoroutine(AlarmedCountdown());
        }
    }
    public void PlayerOutOfVeiw()
    {
        canSeePlayer = false;
        StopCoroutine(AttackTimeCountdown());
        StopCoroutine(AlarmedCountdown());
        StartCoroutine(ReturnToPatrol());

    }

    private void OnDrawGizmosSelected()
    {
        if(patrolPoint.Count > 0)
        {
            foreach (Vector3 child in patrolPoint)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(child, .5f);
            }
        }
    }

}
