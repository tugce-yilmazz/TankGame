using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    Transform player;
    NavMeshAgent agent;
    public Transform p1, p2, p3;
    public Transform rayorigin;
    Vector3[] waypoints;
    int currentTarget;
    Animator fsm;
    float shootFreq = 5f;
    // Start is called before the first frame update
    void Start()
    {

        fsm = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    
        currentTarget = 0;

        waypoints = new Vector3[] { p1.position, p2.position, p3.position };
        agent.SetDestination(waypoints[currentTarget]);


        StartCoroutine("CheckPlayer");
    }

    IEnumerator CheckPlayer()
    {
        CheckVisibility();
        CheckDistance();

        yield return new WaitForSeconds(0.1f);
        yield return CheckPlayer();
    }

    private void CheckVisibility()
    {
        float maxDistance = 20f;
        Vector3 direction = (player.position - transform.position).normalized;
        //Vector 3 direction = (player.position - transform.position) / (player.position - transform.position).magnitude;

        Debug.DrawRay(rayorigin.position, direction * maxDistance, Color.red);

        if (Physics.Raycast(rayorigin.position, direction, out RaycastHit info, maxDistance))
        {
            if (info.transform.CompareTag("Player"))
            {
                fsm.SetBool("isvisible", true);

            }
            else
            {
                fsm.SetBool("isvisible", false);
            }
        }
        else
        {

            fsm.SetBool("isvisible", false);
        }

    }
    private void CheckDistance()
    {
        float distance = (player.position - transform.position).magnitude;
        //float distance = Vector3.distance(player.position - transform.position);

        fsm.SetFloat("distance", distance);

        float distanceFromWayPoint = Vector3.Distance(transform.position, waypoints[currentTarget]);
        fsm.SetFloat("distanceFromWaypoint", distanceFromWayPoint);
    }


    public void SetLookRotation()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.2f);
    }


    public void Shoot()
    {
        GetComponent<ShootBehaviour>().Shoot(shootFreq);
    }

    public void Patrol()
    {
        // Debug.Log("Patrolling...");
    }

    public void Chase()
    {
        agent.SetDestination(player.position);
    }

    public void SetNextWayPoint()
    {
        switch (currentTarget)
        {
            case 0:
                currentTarget = 1;
                break;
            case 1:
                currentTarget = 2;
                break;
            case 2:
                currentTarget = 0;
                break;

        }
        agent.SetDestination(waypoints[currentTarget]);


    }
  
}
