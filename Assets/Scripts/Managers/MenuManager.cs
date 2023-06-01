using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    void Awake () {
        DeletePoint.points.Clear();
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
