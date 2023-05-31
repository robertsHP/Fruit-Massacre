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
    public MovementAI movementAI;

    private WalkPoint currentWalkPoint;

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
        if(visionCone.GameObjectInView != null) {
            CurrentState = GhostState.Chase;
        } else {
            currentWalkPoint = movementAI.Patrol(agent, currentWalkPoint);
        }
    }
    void ChaseState () {
        if(visionCone.GameObjectInView == null) {
            CurrentState = GhostState.Patrol;
        } else {
            movementAI.MoveTo(agent, player.transform);
        }
    }
}
