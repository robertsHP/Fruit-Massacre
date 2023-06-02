using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingFruit : MonoBehaviour {
    [SerializeField] public GameObject bloodParticle;
    [SerializeField] public Animator animator;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public MovementAI movementAI;

    private WalkPoint currentWalkPoint;

    void Start() {
        GameManager.instance.walkingFruit.Add(this);
        animator.SetBool("isWalking", true);
    }
    void Update() {
        if(GameManager.instance.CurrentState == GameState.Game) {
            currentWalkPoint = movementAI.Patrol(agent, currentWalkPoint);
        } else {
            movementAI.Idle(agent, currentWalkPoint);
        }
    }
    void OnTriggerEnter (Collider other) {
        if(other.CompareTag("Player")) {
            OnFruitKilled();
            Instantiate(bloodParticle, transform.position + new Vector3(0, 2, 0), transform.rotation);
            SpawnManager.instance.SpawnWalkingFruit();
            SpawnManager.instance.SpawnEnemy();

            Destroy(gameObject);
        }
    }
    void OnFruitKilled () {
        GameManager.instance.fruitKillCount++;
        UIManager.instance.UpdateFruitCount();

        uint fruitKillCount = GameManager.instance.fruitKillCount;
        uint totalFruitAmount = GameManager.instance.totalFruitAmount;

        if(fruitKillCount >= totalFruitAmount) {
            GameManager.instance.CurrentState = GameState.Win;
        }
    }
}
