using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public void Play () {
        Debug.Log(nameof(Play));
        SceneManager.LoadScene(1);
    }
    public void Settings () {
        Debug.Log(nameof(Settings));
    }
    public void Quit () {
        Debug.Log(nameof(Quit));
        Application.Quit();
    }
    public void TryAgain () {
        Debug.Log(nameof(TryAgain));
        SceneManager.LoadScene(1);
    }
    public void QuitLoseScreen () {
        Debug.Log(nameof(Quit));
        SceneManager.LoadScene(0);
    }
}
