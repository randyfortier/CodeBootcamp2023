using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private Transform target;

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int nextWaypointIndex = 0;
    [SerializeField] private float closeEnoughDistance = 1f;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if(agent != null) {
            if (Vector3.Distance(waypoints[nextWaypointIndex].position, transform.position) < closeEnoughDistance) {
                nextWaypointIndex++;

                if (nextWaypointIndex >= waypoints.Length) {
                    nextWaypointIndex = 0;
                }

                agent.SetDestination(waypoints[nextWaypointIndex].position);
            }

            Animator animator = GetComponent<Animator>();
            if (animator != null) {
                animator.SetFloat("Speed", agent.velocity.magnitude);
            }

        }
    }
}
