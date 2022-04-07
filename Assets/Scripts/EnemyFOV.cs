using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    //public GameObject[] players;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    PatrolEnemy patrolEnemyClass;
    public bool pauseTimeIsActive = false;

    bool canSeePlayer = false;
    private float delay = 0.2f;
    

    // Start is called before the first frame update
    void Start()
    {
        //players = GameObject.FindGameObjectsWithTag("Player");
        patrolEnemyClass = GetComponentInParent<PatrolEnemy>();
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            yield return wait;
            FOVCheck();
        }
    }

    private void FOVCheck()
    {
        // check range of things with the layers set in the sphere view range
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        //Debug.Log("EnemyFOV.Check");
        // if things in range is greater than 0
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            //Debug.Log("EnemyFOV. something in range");
            // if direction to target is within the view angle
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                //Debug.Log("EnemyFOV. Something in view");
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                // if a raycast from enemy to the player and doesn't hit an obstuction
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    if (!canSeePlayer)
                    {
                        patrolEnemyClass.PlayerSeen(target);
                        canSeePlayer = true;
                        ScoreManager.playerSeenCount++;
                        //Debug.Log("EnemyFOV. Player seen");
                    } else { Debug.Log("EnemyFOV.Player in view"); } return;
                   
                } else if (canSeePlayer) { patrolEnemyClass.PlayerOutOfVeiw(); canSeePlayer = false; } else return;

            } else if (canSeePlayer) { patrolEnemyClass.PlayerOutOfVeiw(); canSeePlayer = false; } else return;
        } else if (canSeePlayer) { patrolEnemyClass.PlayerOutOfVeiw(); canSeePlayer = false; } else return;
    }
}
