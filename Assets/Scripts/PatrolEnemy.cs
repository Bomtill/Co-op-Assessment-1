using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    public static event Action GameOverEvent;
    NavMeshAgent agent;
    Transform targetPlayer;
    //EnemyFOV thisEnemyFOV;
    Vector3 lastKnowPosition;
    Animator anim;
    Time localTime;
    [SerializeField] GameObject exclamationPoint;
    [SerializeField] GameObject questionMark;
    [SerializeField] GameObject exclamationPointRed;

    [Tooltip("Tick if the character is a Scientist, instead of a Guard")]
    [SerializeField] bool isScientist;

    [SerializeField] float moveSpeed, alertTime, SearchTime, attackTime, patrolWaitTime, returnToPatrolTime;

    enum States {IDLE, PATROL, SEARCH, ATTACK, ALARMED, RUNAWAY, PAUSED, REWIND }
    [SerializeField] States currentState;

    float patrolWaitTimeCounter;
    bool attackTimerRunning = false;
    bool isIdle = false;
    bool isTimeStopped = false;
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
        //thisEnemyFOV = GetComponentInChildren<EnemyFOV>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        //viewCone = GetComponentInChildren<MeshCollider>();
        currentState = States.IDLE;
        StartCoroutine(SM());

        exclamationPoint.SetActive(false);
        exclamationPointRed.SetActive(false);
        questionMark.SetActive(false);
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
        isIdle = true;
        float timeDiff = patrolWaitTimeCounter;
        patrolWaitTimeCounter -= Time.deltaTime;
        while (currentState == States.IDLE)
        {
            yield return new WaitForSeconds(patrolWaitTime - timeDiff);
            if (!isTimeStopped)
            {
                if (shouldPatrol)
                {
                    currentState = States.PATROL;
                }
            }
        } 
        yield return null;
    }
    IEnumerator PATROL()
    {
        isIdle = false;
        patrolWaitTimeCounter = 0;
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
        agent.SetDestination(lastKnowPosition);
        yield return null;
    }
    IEnumerator ATTACK()
    {
        attackTimerRunning = false;
        while (currentState == States.ATTACK)
        {
            GameOverEvent?.Invoke();
            Debug.Log("Game Over");
            StopAllCoroutines();
            yield return endFrame;
        }
        yield return null;
    }
    IEnumerator ALARMED()
    {
        // anim.play caution or look animation
        Debug.Log("huh, what are you doing!");
        isIdle = false;
        isAlert = true;
        if (canSeePlayer)
        {
            while (currentState == States.ALARMED)
            {
                if (isScientist)
                {
                    currentState = States.RUNAWAY;
                    yield return endFrame;
                } else
                {
                    exclamationPoint.SetActive(false);
                    StartCoroutine("AttackTimeCountdown");
                    yield return endFrame;
                }
            }
        }
        
        yield return null;
    }
    IEnumerator PAUSED() 
    {
        // not getting out of idle state
        while(currentState == States.PAUSED)
        {
            anim.speed = 0;
            agent.speed = 0;
            yield return endFrame;
        }
        yield return null;
    }
    IEnumerator REWIND() // for when players rewind time
    {
        yield return null;
    }
    IEnumerator AlarmedCountdown()
    {
        exclamationPoint.SetActive(true);
        yield return new WaitForSeconds(alertTime);
        //exclamationPoint.SetActive(false);
        currentState = States.ALARMED;
        yield return null;
    }

    IEnumerator AttackTimeCountdown()
    {
        if (!attackTimerRunning)
        {
            attackTimerRunning = true;
            exclamationPointRed.SetActive(true);
            yield return new WaitForSeconds(attackTime);
            currentState = States.ATTACK;
            yield return null;
        } else yield return null;
        
    }
    IEnumerator ReturnToPatrol()
    {
        questionMark.SetActive(false);
        anim.SetBool("IsAlert", false);
        yield return new WaitForSeconds(returnToPatrolTime);
        currentState = States.PATROL;
        isSearching = false;
        yield return null;
    }

    public void PlayerSeen(Transform player)
    {
        lastKnowPosition = player.position;
        
        if (isAlert)
        {
            agent.isStopped = true;
            currentState = States.ALARMED;
            StartCoroutine("AttackTimeCountdown");
            
        } else
        {
            agent.isStopped = true;
            canSeePlayer = true;
            anim.SetBool("IsAlert", true);
            StartCoroutine("AlarmedCountdown");
        }
    }
    public void PlayerOutOfVeiw()
    {
        agent.isStopped = false;
        exclamationPoint.SetActive(false);
        exclamationPointRed.SetActive(false);
        questionMark.SetActive(true);
        canSeePlayer = false;
        StopCoroutine("AttackTimeCountdown");
        StopCoroutine("AlarmedCountdown");
        StartCoroutine(ReturnToPatrol());
        currentState = States.SEARCH;

    }
    private void OnEnable()
    {
        PlayerPowers.PauseTimeEvent += PauseTimeActive;
        PlayerPowers.RestartTimeEvent += PauseTimeInactive;
    }
    private void OnDisable()
    {
        PlayerPowers.PauseTimeEvent -= PauseTimeActive;
        PlayerPowers.RestartTimeEvent -= PauseTimeInactive;
    }
    void PauseTimeActive()
    {
        isTimeStopped = true;
        currentState = States.PAUSED;
        if (attackTimerRunning) StopCoroutine(AttackTimeCountdown());
        
    }
    void PauseTimeInactive()
    {
        isTimeStopped = false;
        anim.speed = 1;
        agent.speed = 1.5f;
        if (isAlert) { PlayerOutOfVeiw(); return; }
        if (isIdle) currentState = States.IDLE;
        else currentState = States.PATROL;
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
