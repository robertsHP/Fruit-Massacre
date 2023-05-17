using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public static UIManager instance;

    [SerializeField] public RectTransform endConditionPanel;
    [SerializeField] public TMPro.TextMeshProUGUI endMessage;

    [SerializeField] public TMPro.TextMeshProUGUI fruitCount;

    void Awake () {
        if(instance == null) instance = this;
    }
    void Start () {
        GameManager.instance.OnGameWon += OnGameWon;
        GameManager.instance.OnGameLose += OnGameLose;

        endConditionPanel.gameObject.SetActive(false);
        UpdateFruitCount();
    }
    void OnDestroy () {
        GameManager.instance.OnGameWon -= OnGameWon;
        GameManager.instance.OnGameLose -= OnGameLose;
    }

    void OnGameWon() {
        endConditionPanel.gameObject.SetActive(true);
        endMessage.text = "You win!";
    }
    void OnGameLose() {
        endConditionPanel.gameObject.SetActive(true);
        endMessage.text = "You lose!";
    }

    public void UpdateFruitCount () {
        uint count = GameManager.instance.fruitCount;
        fruitCount.text = count+"/"+Fruit.totalGameObjectAmount;
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
