using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public CharacterController controller;
    public Renderer rend;
    public PlayerViewBounds viewBounds;
    public PlayerCamHolder camHolder;

    public float speed = 4f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public GameObject bloodParticle;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start() {
        GameManager.instance.OnGameWon += OnGameWon;
        GameManager.instance.OnGameLose += OnGameLose;
    }
    void OnDestroy () {
        GameManager.instance.OnGameWon -= OnGameWon;
        GameManager.instance.OnGameLose -= OnGameLose;
    }
    void OnGameWon() {
        speed = 0f;
        gravity = 0f;
        jumpHeight = 0f;
    }
    void OnGameLose () {
        Instantiate(bloodParticle, transform.position + new Vector3(0, 2, 0), transform.rotation);
        speed = 0f;
        gravity = 0f;
        jumpHeight = 0f;
    }

    // Update is called once per frame
    void Update() {
        rend.enabled = GameManager.instance.debugOn;

        if(GameManager.instance.CurrentState == GameState.Game) {
            // EscapeGameInput();
            CheckGrounded();
            MovementInput();
            JumpInput();
        }
    }
    private void EscapeGameInput () {
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }
    }
    private void CheckGrounded () {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
    }
    private void MovementInput () {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }
    private void JumpInput () {
        if(Input.GetKeyDown("space") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void OnTriggerEnter (Collider other) {
        if(other.CompareTag("Enemy")) {
            GameManager.instance.CurrentState = GameState.Lose;
            Instantiate(bloodParticle, transform.position + new Vector3(0, 2, 0), transform.rotation);
        }
    }
}