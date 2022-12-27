using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    public Transform[] wayPoints;

    int m_CurrentWayPointIndex;

    private void Start()
    {
        navMeshAgent.SetDestination(wayPoints[0].position);
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWayPointIndex = (m_CurrentWayPointIndex + 1) % wayPoints.Length;
            navMeshAgent.SetDestination(wayPoints[m_CurrentWayPointIndex].position);
        }
    }
}
