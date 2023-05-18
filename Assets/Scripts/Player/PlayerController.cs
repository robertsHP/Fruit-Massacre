using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("References")]
    public Rigidbody rigidbody;
    public Transform camHolder;
    public Camera camera;

    [Header("Configurations")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpSpeed = 6f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float speed;
    Vector3 direction;
    bool isGrounded;
    bool isJumping;

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
        
    }
    void OnGameLose () {
        // controller.enabled = false;
        // rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    void Update() {
        speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        if(Input.GetButtonDown("Jump") && isGrounded) {
            isJumping = false;
            rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    void FixedUpdate () {
        Vector3 walkVelocity = (transform.right * direction.x + transform.forward * direction.z) * speed;
        rigidbody.velocity = new Vector3(walkVelocity.x, rigidbody.velocity.y, walkVelocity.z);
    }
    void OnCollisionEnter (Collision collision) {
        if(collision.collider.CompareTag("Enemy")) {
            GameManager.instance.SetState(GameState.Lose);
        }
        Debug.Log("on");
        isGrounded = true;
    }
    void OnCollisionExit (Collision collision) {
        Debug.Log("off");
        isGrounded = false;
    }
}
