using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public enum GhostState {
    Patrol,
    Chase
}

public class Ghost : MonoBehaviour {
    private GhostState state;
    public GhostState CurrentState {
        get => state;
        set {
            if (value != state) {
                state = value;
            }
        }
    }

    public NavMeshAgent agent;
    public Player player;
    public VisionCone visionCone;

    private Transform nextPoint;

    void Start() {
        CurrentState = GhostState.Patrol;
    }

    // Update is called once per frame
    void Update() {
        if(GameManager.instance.CurrentState == GameState.Game) {
            switch (CurrentState) {
                case GhostState.Patrol :
                    PatrolState();
                    break;
                case GhostState.Chase :
                    ChaseState();
                    break;
                default : break;
            }
        }
    }
    void PatrolState () {
        // if(visionCone.GameObjectInView != null) {
        //     CurrentState = GhostState.Chase;
        // }
        if(WalkPoint.points.Any()) {
            //Follow points
            if(nextPoint == null) {
                int walkPointIndex = (int) Random.Range(0, WalkPoint.points.Count);
                nextPoint = WalkPoint.points.ElementAt(walkPointIndex);
            }
            MoveTo(nextPoint);
            if(IfDestinationReached()) {
                nextPoint = null;
            }
        }
    }
    void ChaseState () {
        if(visionCone.GameObjectInView == null) {
            CurrentState = GhostState.Patrol;
        } else {
            MoveTo(player.transform);
        }
    }
    bool IfDestinationReached () {
        if (!agent.pathPending) {
            if (agent.remainingDistance <= agent.stoppingDistance) {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
                    return true;
                }
            }
        }
        return false;
    }
    void MoveTo (Transform nextTransform) {
        if(nextTransform != null) {
            // The step size is equal to speed times frame time.
            var step = agent.speed * Time.deltaTime;
            // Rotate our transform a step closer to the target's.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, nextTransform.rotation, step);
            // Move to position
            agent.SetDestination(nextTransform.position);
        }
    }
}
