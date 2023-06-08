using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {
    [SerializeField] public AudioSource ambience;
    [SerializeField] public AudioSource chase;
    [SerializeField] public AudioSource killSound;
    [SerializeField] public AudioSource winSound;
    [SerializeField] public AudioSource footStep;
    [SerializeField] public AudioSource runFootStep;
    [SerializeField] public AudioSource jumpSound;

    private float chaseAudioIncreaseAmount = 0.005f;
    private float chaseMaxVolume = 0.7f;
    private float chaseMinVolume = 0f;

    void Start() {
        GameManager.instance.OnGameWon += OnGameWon;
        GameManager.instance.OnGameLose += OnGameLose;
    }
    void OnDestroy () {
        GameManager.instance.OnGameWon -= OnGameWon;
        GameManager.instance.OnGameLose -= OnGameLose;
    }
    void OnGameWon() {
        winSound.Play();
    }
    void OnGameLose () {
        killSound.Play();
    }

    void Update () {
        if(GameManager.instance.CurrentState == GameState.Game) {
            if(GameManager.instance.enemiesChasingPlayer.Count != 0) {
                if(!chase.isPlaying) 
                    chase.Play();
                chase.volume += chaseAudioIncreaseAmount;
                chase.volume = Mathf.Clamp(chase.volume, chaseMinVolume, chaseMaxVolume);
            } else if (chase.volume != 0) {
                chase.volume -= chaseAudioIncreaseAmount;
                chase.volume = Mathf.Clamp(chase.volume, chaseMinVolume, chaseMaxVolume);
            }
        } else if (GameManager.instance.CurrentState == GameState.Win) {
            if(ambience.isPlaying || chase.isPlaying) {
                ambience.Stop();
                chase.Stop();
            }
        }
    }
}
