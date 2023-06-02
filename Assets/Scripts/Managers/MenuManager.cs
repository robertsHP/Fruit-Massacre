using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    void Awake () {
        MenuDeletePoint.points.Clear();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play () {
        Debug.Log(nameof(Play));
        SceneManager.LoadScene(1);
    }
    public void Quit () {
        Debug.Log(nameof(Quit));
        Application.Quit();
    }
}
