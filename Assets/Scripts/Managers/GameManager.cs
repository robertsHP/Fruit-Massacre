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

    public bool debugOn = false;

    private GameState state;
    public GameState CurrentState {
        get => state;
        set {
            if (value != state) {
                SetState(value);
            }
        }
    }

    private List<WalkPoint> _walkPoints = new List<WalkPoint>();
    private List<StalkerPoint> _stalkerPoints = new List<StalkerPoint>();

    public uint TotalFruitCount {get; set;}
    public uint FruitKillCount {get; set;}
    public List<WalkPoint> WalkPoints {get {return _walkPoints;}}
    public List<StalkerPoint> StalkerPoints {get {return _stalkerPoints;}}

    void Awake () {
        if(instance == null) instance = this;
        SetState(GameState.Game);
    }
    void Start() {

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
        TotalFruitCount = 0;
        FruitKillCount = 0;
        WalkPoints.Clear();
        StalkerPoints.Clear();
    }

    void Update () {

    }
}