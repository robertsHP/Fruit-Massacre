using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Game,
    Win,
    Lose
}

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    // public event Action OnGameStart;
    public event Action OnGameWon;
    public event Action OnGameLose;

    private GameState state;
    public GameState CurrentState {
        get => state;
        set {
            if (value != state) {
                SetState(value);
            }
        }
    }

    ///////INSPECTOR VARIABLES

    [SerializeField] public uint totalFruitAmount = 6;
    [SerializeField] public bool debugOn = false;
    [SerializeField] public Player player;

    ///////RUNTIME VARIABLES

    [HideInInspector] public uint fruitKillCount = 0;

    [HideInInspector] public List<WalkPoint> walkPoints = new List<WalkPoint>();
    [HideInInspector] public List<StalkerPoint> stalkerPoints = new List<StalkerPoint>();

    [HideInInspector] public List<WalkingFruit> walkingFruit = new List<WalkingFruit>();
    [HideInInspector] public List<Enemy> enemies = new List<Enemy>();

    [HideInInspector] public List<Enemy> enemiesChasingPlayer = new List<Enemy>();

    ////////

    private bool gameOn = false;

    void Awake () => instance = this;

    void Start() {
        // SetState(GameState.Game);
    }
    private void SetState (GameState newState) {
        state = newState;
        switch (state) {
            case GameState.Game :
                // OnGameStart?.Invoke();
                OnGameStart();
                break;
            case GameState.Win :
                OnGameWon?.Invoke();
                break;
            case GameState.Lose :
                OnGameLose?.Invoke();
                break;
            default :
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    void OnGameStart () {
        UIManager.instance.UpdateFruitCount();
        SpawnManager.instance.SpawnWalkingFruit();
    }

    void Update () {
        if(!gameOn) {
            gameOn = true;
            SetState(GameState.Game);
        }
        // Debug.Log(walkingFruit.Count);
    }
}