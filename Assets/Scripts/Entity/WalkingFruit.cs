using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingFruit : MonoBehaviour {
    [SerializeField] public GameObject bloodParticle;
    [SerializeField] public Animator animator;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public MovementAI movementAI;
    [SerializeField] public Renderer rend;

    [SerializeField] public AudioSource killSound;
    [SerializeField] public AudioSource matingCall;
    [SerializeField] public uint matingCallPauseSeconds = 5;

    private WalkPoint currentWalkPoint;
    private bool matingCallCoroutineOn = false;

    void Start() {
        GameManager.instance.walkingFruit.Add(this);

        animator.SetBool("PlayerDead", false);

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

    void Update() {
        if(GameManager.instance.CurrentState == GameState.Game) {
            if(!matingCallCoroutineOn)
                StartCoroutine(MatingCallCoroutine());
            currentWalkPoint = movementAI.Patrol(agent, currentWalkPoint);
        } else {
            movementAI.Idle(agent, currentWalkPoint);
        }
    }

    IEnumerator MatingCallCoroutine () {
        matingCallCoroutineOn = true;

        matingCall.Play();
        yield return new WaitForSeconds(matingCallPauseSeconds);

        matingCallCoroutineOn = false;
    }

    void OnTriggerEnter (Collider other) {
        if(other.CompareTag("Player")) {
            StartCoroutine(KillCoroutine());
        }
    }
    IEnumerator KillCoroutine () {
        OnFruitKilled();
        yield return new WaitUntil(KillSoundDone);
        Destroy(gameObject);
    }
    bool KillSoundDone () {
        return !killSound.isPlaying;
    }
    void OnFruitKilled () {
        GameManager.instance.fruitKillCount++;
        UIManager.instance.UpdateFruitCount();

        rend.enabled = false;

        Instantiate(bloodParticle, transform.position + new Vector3(0, 2, 0), transform.rotation);
        killSound.Play();

        uint fruitKillCount = GameManager.instance.fruitKillCount;
        uint totalFruitAmount = GameManager.instance.totalFruitAmount;

        if(fruitKillCount >= totalFruitAmount) {
            GameManager.instance.CurrentState = GameState.Win;
        }

        SpawnManager.instance.SpawnWalkingFruit();
        SpawnManager.instance.SpawnEnemy();
    }
}
