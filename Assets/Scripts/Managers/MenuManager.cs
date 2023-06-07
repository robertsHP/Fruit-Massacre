using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MainMenuState {
    Menu,
    CreditsPanel,
    TutorialPanel
}

public class MenuManager : MonoBehaviour {
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject creditsPanel;

    private MainMenuState state;
    public MainMenuState CurrentState {
        get => state;
        set {
            if (value != state) {
                SetState(value);
            }
        }
    }

    void SetState (MainMenuState value) {
        state = value;
        menu.SetActive(state == MainMenuState.Menu);
        creditsPanel.SetActive(state == MainMenuState.CreditsPanel);
        tutorialPanel.SetActive(state == MainMenuState.TutorialPanel);
    }

    void Awake () {
        MenuDeletePoint.points.Clear();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SetState(MainMenuState.Menu);
    }

    public void TutorialPanel () {
        SetState(MainMenuState.TutorialPanel);
    }
    public void Credits () {
        SetState(MainMenuState.CreditsPanel);
    }
    public void Quit () {
        Application.Quit();
    }
    public void Play () {
        SceneManager.LoadScene(1);
    }
    public void Back () {
        SetState(MainMenuState.Menu);
    }
}
