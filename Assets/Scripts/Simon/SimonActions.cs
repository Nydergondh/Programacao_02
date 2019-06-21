using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonActions : MonoBehaviour
{
    public static SimonActions simon;

    public float walkSpeed = 5f;
    public float jumpSpeed = 5f;
    public int whipLv = 1;
    public LayerMask isPlataform;

    public SpriteRenderer simonRenderer;
    public SpriteRenderer whipRenderer;

    public Animator simonAnim;
    public Animator whipAnim;

    private new Rigidbody2D  rigidbody;
    public Collider2D feetCollider;
    
    void Awake() {
        if (simon == null) {
            simon = this;
        }
        else {
            Destroy(simon);
            simon = this;
        }
    }
    
    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        whipRenderer.enabled = false;
    }

    void Update() {
        Movement();
        Attack();
    }

    private void Movement() {
        // Movements : Walk, Fall, Jump, Idle, Stair, Crouch

        Vector2 vel;
        float delta = Input.GetAxis("Horizontal") * walkSpeed;
        vel = new Vector2(delta, rigidbody.velocity.y);
        simonAnim.SetFloat("Speed" ,delta);
        if (!simonAnim.GetBool("IsAttacking") || (simonAnim.GetBool("IsJumping") && simonAnim.GetBool("IsAttacking"))) {
            if (delta > 0 || delta < 0) {
                //is walking
                simonAnim.SetBool("IsWalking", true);
                vel = new Vector2(delta, rigidbody.velocity.y);
            }
            else {
                //is not walking
                simonAnim.SetBool("IsWalking", false);
                vel = new Vector2(0, rigidbody.velocity.y);
            }

            if (Input.GetButton("Crouch")) {
                //is crouching
                simonAnim.SetBool("Crouch", true);
                simonAnim.SetBool("IsWalking", false);
                vel = Vector2.zero;
            }
            else {
                //is not crouching
                simonAnim.SetBool("Crouch", false);
                vel = new Vector2(delta, rigidbody.velocity.y);
            }

            if (CheckGround() && !simonAnim.GetBool("IsJumping") && Input.GetButtonDown("Jump") && !simonAnim.GetBool("Crouch")) {
                Jump(vel.x);
                simonAnim.SetBool("IsJumping", true);
                simonAnim.SetBool("Crouch", false);
                simonAnim.SetBool("IsWalking", false);
            }
            else if (!CheckGround() && simonAnim.GetBool("IsJumping")) {
                //rigidbody.velocity = vel;
            }
            else if (CheckGround() && simonAnim.GetBool("IsJumping") && rigidbody.velocity.y <= 0) {
                simonAnim.SetBool("IsJumping", false);
                rigidbody.velocity = vel;
            }
            else {
                rigidbody.velocity = vel;
            }
        }
        //check for other types of atacks
        //create an input with keys (down | s)
        CheckDirection(delta);
    }

    void Attack() {

        // Actions : Item, Whip
        if (!simonAnim.GetBool("IsAttacking")) {

            if (Input.GetKeyDown(KeyCode.Z) && !simon.simonAnim.GetBool("IsJumping") && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))) {
                simonAnim.SetBool("IsWalking", false);
                simonAnim.SetBool("IsAttacking", true);
                StartCoroutine(AttackWait());
            }

            else if (Input.GetKeyDown(KeyCode.Z)) {
                //play whipAnim
                if (simonAnim.GetBool("Crouch")) {
                    CrouchWhip();
                }
                else {
                    Whip();
                }

                if (simonAnim.GetBool("IsJumping")) {
                    simonAnim.SetBool("IsWalking", false);
                    simonAnim.SetBool("IsAttacking", true);
                }
                else {
                    simonAnim.SetBool("IsWalking", false);
                    simonAnim.SetBool("IsAttacking", true);
                    rigidbody.velocity = Vector2.zero;
                }
                StartCoroutine(AttackWait());
            }
        }
    }

    private void Jump(float vel) {
        rigidbody.velocity =  new Vector2(vel, jumpSpeed);
    }

    bool CheckGround() {
        if (!feetCollider.IsTouchingLayers(isPlataform)) {
            return false;
        }
        else {
            return true;
        }
    }

    private void Whip() {
        if (whipLv == 1) {
            whipAnim.SetBool("Whip", true);
            whipAnim.SetInteger("WhipLv", 1);
        }
        else if (whipLv == 2) {
            whipAnim.SetBool("Whip", true);
            whipAnim.SetInteger("WhipLv", 2);
        }
        else if (whipLv == 3) {
            whipAnim.SetBool("Whip", true);
            whipAnim.SetInteger("WhipLv", 3);
        }
    }

    private void CrouchWhip() {
        if (whipLv == 1) {
            whipAnim.SetBool("CrouchWhip", true);
            whipAnim.SetInteger("WhipLv", 1);
        }
        else if (whipLv == 2) {
            whipAnim.SetBool("CrouchWhip", true);
            whipAnim.SetInteger("WhipLv", 2);
        }
        else if (whipLv == 3) {
            whipAnim.SetBool("CrouchWhip", true);
            whipAnim.SetInteger("WhipLv", 3);
        }
    }

    public void CheckDirection(float movement) {
        //if was left but now is going rigth
        if (!simonAnim.GetBool("IsAttacking") && !simonAnim.GetBool("IsJumping")) {
            if (movement <= 1 * walkSpeed && movement > 0) {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            //if was rigth but now is going left
            else if (movement >= -1 * walkSpeed && movement < 0) {
                transform.localScale = new Vector3(1, 1, 1); 
            }
        }
    }
    //checks if simon is colliding with groundlayers (set in the layer stuff)

    IEnumerator AttackWait() {
        yield return new WaitForSeconds(0.518f);
        //set all the attack variables to false
        simonAnim.SetBool("IsAttacking", false);
        whipAnim.SetBool("Whip", false);
        whipAnim.SetBool("CrouchWhip", false);
        whipRenderer.enabled = false;
    }
    
}
