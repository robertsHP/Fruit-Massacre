using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MovementAI : MonoBehaviour {
    public WalkPoint Patrol (NavMeshAgent agent, WalkPoint currentPoint) {
        if(WalkPoint.points.Any()) {
            //Follow points
            if(currentPoint == null) {
                int walkPointAmount = WalkPoint.points.Count;
                int walkPointIndex = (int) Random.Range(0, walkPointAmount);
                currentPoint = WalkPoint.points.ElementAt(walkPointIndex);
                MoveTo(agent, currentPoint.transform);
            }
            if(IfDestinationReached(agent)) {
                currentPoint = null;
            }
        }
        return currentPoint;
    }
    public WalkPoint Idle (NavMeshAgent agent, WalkPoint currentWalkPoint) {
        if (!agent.isStopped) {
            agent.isStopped = true;
            currentWalkPoint = null;
        }
        return currentWalkPoint;
    }

    bool IfDestinationReached (NavMeshAgent agent) {
        if (!agent.pathPending) {
            if (agent.remainingDistance <= agent.stoppingDistance) {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
                    return true;
                }
            }
        }
        return false;
    }
    public void MoveTo (NavMeshAgent agent, Transform tr) {
        if(tr != null) {
            // The step size is equal to speed times frame time.
            var step = agent.speed * Time.deltaTime;
            // Rotate our transform a step closer to the target's.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, tr.rotation, step);
            // Move to position
            agent.SetDestination(tr.position);
        }
    }
}
