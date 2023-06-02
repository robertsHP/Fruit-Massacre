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
    [SerializeField] private TMPro.TextMeshProUGUI staminaAmount;

    public void TryAgain () {
        Debug.Log(nameof(TryAgain));
        SceneManager.LoadScene(1);
    }
    public void QuitLoseScreen () {
        Debug.Log(nameof(QuitLoseScreen));
        SceneManager.LoadScene(0);
    }


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
        uint fruitKillCount = GameManager.instance.fruitKillCount;
        uint totalFruitAmount = GameManager.instance.totalFruitAmount;

        fruitCount.text = "Fruit murdered: "+fruitKillCount+" out of "+totalFruitAmount;
    }
    // public void UpdateStamina () {
    //     uint stamina = GameManager.instance.player.stamina;

    //     staminaAmount.text = "Stamina: "+stamina;
    // }
}
