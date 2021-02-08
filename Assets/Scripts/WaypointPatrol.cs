using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    int m_CurrentWaypointIndex;

    void Start ()
    {
        if(waypoints.Length > 0)
            navMeshAgent.SetDestination (waypoints[0].position);
    }

    void Update ()
    {
        if(waypoints.Length > 0)
        {
            if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination (waypoints[m_CurrentWaypointIndex].position);
            }
        }
        
    }
}
