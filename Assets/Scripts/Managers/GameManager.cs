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

    public event Action OnGameStart;
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

    void Awake () {
        if(instance == null) instance = this;
    }
    void Start() {
        SetState(GameState.Game);
    }
    private void SetState (GameState newState) {
        state = newState;
        switch (state) {
            case GameState.Game :
                OnGameStart?.Invoke();
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

    void Update () {

    }
}