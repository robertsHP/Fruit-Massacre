using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public static UIManager instance;

    [SerializeField] private RectTransform loseConditionPanel;
    [SerializeField] private RectTransform winConditionPanel;
    [SerializeField] private TMPro.TextMeshProUGUI fruitCount;

    void Awake () {
        if(instance == null) instance = this;
    }
    void Start () {
        GameManager.instance.OnGameWon += OnGameWon;
        GameManager.instance.OnGameLose += OnGameLose;

        winConditionPanel.gameObject.SetActive(false);
        loseConditionPanel.gameObject.SetActive(false);
    }
    void OnDestroy () {
        GameManager.instance.OnGameWon -= OnGameWon;
        GameManager.instance.OnGameLose -= OnGameLose;
    }

    void OnGameWon() {
        winConditionPanel.gameObject.SetActive(true);
    }
    void OnGameLose() {
        loseConditionPanel.gameObject.SetActive(true);
    }

    public void UpdateFruitCount () {
        uint killCount = WalkingFruit.fruitKillCount;
        uint totalCount = WalkingFruit.totalFruitCount;

        fruitCount.text = "Fruit murdered: "+killCount+" out of "+totalCount;
    }

    void Update() {
        // if (Keyboard.current.escapeKey.wasReleasedThisFrame) {
        //     LoadMenu();
        // }
    }
    // void LoadMenu() {
    //     if (isLoadingMenu) return;
        
    //     isLoadingMenu = true;
    //     SceneManager.LoadScene(0);
    // }
}
