using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public enum GhostState {
    Patrol,
    Chase
}

public class Ghost : Enemy {
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
    [SerializeField] private VisionCone visionCone;
    [SerializeField] private MovementAI movementAI;

    [SerializeField] private AudioSource patrolSound;

    [SerializeField] private float walkSpeed = 3;
    [SerializeField] private float runSpeed = 5;

    private WalkPoint currentWalkPoint;

    void Start() {
        GameManager.instance.enemies.Add(this);

        CurrentState = GhostState.Patrol;
        agent.speed = walkSpeed;
        
        animator.SetBool("PlayerDead", false);
        animator.SetBool("SeePlayer", false);

        GameManager.instance.OnGameWon += OnGameOver;
        GameManager.instance.OnGameLose += OnGameOver;
    }
    void OnDestroy () {
        GameManager.instance.OnGameWon -= OnGameOver;
        GameManager.instance.OnGameLose -= OnGameOver;
    }
    void OnGameOver () {
        animator.SetBool("PlayerDead", true);
        movementAI.Idle(agent, currentWalkPoint);
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
        if(visionCone.GameObjectInView != null) {
            animator.SetBool("SeePlayer", true);
            agent.speed = runSpeed;
            CurrentState = GhostState.Chase;
            GameManager.instance.enemiesChasingPlayer.Add(this);
        } else {
            currentWalkPoint = movementAI.Patrol(agent, currentWalkPoint);
            if(!patrolSound.isPlaying)
                patrolSound.Play();
        }
    }
    void ChaseState () {
        if(visionCone.GameObjectInView == null) {
            animator.SetBool("SeePlayer", false);
            agent.speed = walkSpeed;
            CurrentState = GhostState.Patrol;
            GameManager.instance.enemiesChasingPlayer.Remove(this);
        } else {
            movementAI.MoveTo(agent, GameManager.instance.player.transform);
        }
    }
}
