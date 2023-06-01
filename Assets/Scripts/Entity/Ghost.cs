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

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Player player;
    [SerializeField] private VisionCone visionCone;
    [SerializeField] private MovementAI movementAI;
    [SerializeField] private Animator animator;

    [SerializeField] private float walkSpeed = 3;
    [SerializeField] private float runSpeed = 5;

    private WalkPoint currentWalkPoint;

    void Start() {
        CurrentState = GhostState.Patrol;
        agent.speed = walkSpeed;
        animator.SetBool("SeePlayer", false);
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
        } else {
            movementAI.Idle(agent, currentWalkPoint);
        }
    }
    void PatrolState () {
        if(visionCone.GameObjectInView != null) {
            animator.SetBool("SeePlayer", true);
            agent.speed = runSpeed;
            CurrentState = GhostState.Chase;
        } else {
            currentWalkPoint = movementAI.Patrol(agent, currentWalkPoint);
        }
    }
    void ChaseState () {
        if(visionCone.GameObjectInView == null) {
            animator.SetBool("SeePlayer", false);
            agent.speed = walkSpeed;
            CurrentState = GhostState.Patrol;
        } else {
            movementAI.MoveTo(agent, player.transform);
        }
    }
}
