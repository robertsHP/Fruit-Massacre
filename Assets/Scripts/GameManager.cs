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

    public event Action OnGameLoad;
    public event Action OnGameWon;
    public event Action OnGameLose;

    private GameState state;
    public uint fruitCount;

    void Awake () {
        if(instance == null) instance = this;
    }
    void Start() {
        SetState(GameState.Game);
    }
    public void SetState (GameState newState) {
        state = newState;
        UpdateState();
    }
    void UpdateState() {
        switch (state) {
            case GameState.Game :
                OnGameLoad?.Invoke();
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

    public void FruitCollected () {
        fruitCount++;
        UIManager.instance.UpdateFruitCount();
    }
}