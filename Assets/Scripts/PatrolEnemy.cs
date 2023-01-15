using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    public static event Action GameOverEvent;
    NavMeshAgent agent;
    Vector3 lastKnowPosition;
    Animator anim;
    ParticleSystem gunShot;
    EnemyAudio enemyAudio;
    // put these in a switch or enum so only one can be active
    [SerializeField] GameObject exclamationPoint;
    [SerializeField] GameObject questionMark;
    [SerializeField] GameObject exclamationPointRed;

    [Tooltip("Tick if the character is a Scientist, instead of a Guard")]
    [SerializeField] bool isScientist;

    [SerializeField] float moveSpeed, alertSpeed, alertTime, SearchTime, attackTime, patrolWaitTime, returnToPatrolTime;

    enum States {IDLE, PATROL, SEARCH, ATTACK, ALARMED, RUNAWAY, PAUSED, REWIND, DISTRACTED}
    [SerializeField] States currentState;

    float patrolWaitTimeCounter;
    bool attackTimerRunning = false;
    bool isIdle = false;
    bool isTimeStopped = false;
    bool isAlert = false;
    bool isSearching = false;
    bool isPatrolling = false;
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
        currentState = States.IDLE;
        StartCoroutine(SM());
        gunShot = GetComponentInChildren<ParticleSystem>();
        enemyAudio = GetComponentInChildren<EnemyAudio>();
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
        agent.speed = moveSpeed;
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
        agent.speed = moveSpeed;
        isPatrolling = true;
        patrolWaitTimeCounter = 0;
        //questionMark.SetActive(false);
        agent.SetDestination(patrolPoint[currentPatrolPoint]);
        while (currentState == States.PATROL)
        {            
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoint.Count;
                currentState = States.IDLE;
                isPatrolling = false;
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
        questionMark.SetActive(true);
        //isSearching = true;
        // get random spot in area and SetDestination
        agent.SetDestination(lastKnowPosition);
        yield return new WaitForSeconds(3);
        yield return null;
    }
    IEnumerator ATTACK()
    {
        attackTimerRunning = false;
        while (currentState == States.ATTACK)
        {
            gunShot.Play();
            enemyAudio.GunShotSFX();
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
        //Debug.Log("huh, what are you doing!");
        isIdle = false;
        isAlert = true;
        agent.speed = alertSpeed;
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
                    questionMark.SetActive(false);
                    StartCoroutine("AttackTimeCountdown");
                    yield return endFrame;
                }
            }
        } else currentState = States.SEARCH; isSearching = true;
        
        yield return null;
    }
    IEnumerator PAUSED() 
    {
        while(currentState == States.PAUSED)
        {
            anim.speed = 0;
            agent.speed = 0;
            // need to stop the timer coroutines.
            yield return endFrame;
        }
        yield return null;
    }
    IEnumerator REWIND() // for when players rewind time
    {
        yield return null;
    }
    IEnumerator DISTRACTED()
    {
        yield return null;
        // check for lightswitch off
        // move to distract point, either a lightswitch or a noise point after item has been thrown
        // return to patrol state
        
    }

    IEnumerator AlarmedCountdown()
    {
        exclamationPoint.SetActive(true);
        yield return new WaitForSeconds(alertTime);
        if (isTimeStopped) StopCoroutine(AlarmedCountdown());
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
        agent.speed = moveSpeed;
        questionMark.SetActive(false);
        anim.SetBool("IsAlert", false);
        yield return new WaitForSeconds(returnToPatrolTime);
        if (isTimeStopped) StopCoroutine(ReturnToPatrol());
        currentState = States.PATROL;
        isSearching = false;
        yield return null;
    }

    public void PlayerSeen(Transform player)
    {
        lastKnowPosition = player.position;
        ScoreManager.playerSeenCount++;
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
        agent.speed = moveSpeed;
        if (canSeePlayer) 
        { 
            currentState = States.ATTACK; 
            return; 
        }
        if (isAlert) 
        { 
            //PlayerOutOfVeiw(); 
            currentState = States.ALARMED; 
            return; 
        }
        if (isSearching) { currentState = States.SEARCH; return; }
        if (isIdle) { currentState = States.IDLE; return; }
        if (isPatrolling)  currentState = States.PATROL;
        return;
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
