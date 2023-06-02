using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    [SerializeField] public PlayerViewBounds viewBounds;
    [SerializeField] public PlayerCamHolder camHolder;
    [SerializeField] public PlayerAudio charAudio;
    [SerializeField] public Renderer rend;
    [SerializeField] public GameObject bloodParticle;

    [SerializeField] public float speed = 4f;
    [SerializeField] public float gravity = -9.81f;
    [SerializeField] public float jumpHeight = 1f;

    [SerializeField] public Transform groundCheck;

    private float groundDistance = 0.4f;
    private LayerMask groundMask;
    private CharacterController controller;

    private Vector3 velocity;
    private bool isGrounded;

    void Awake () {
        controller = GetComponent<CharacterController>();
        groundDistance = 0.4f;
        groundMask = LayerMask.GetMask("Ground");
    }
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