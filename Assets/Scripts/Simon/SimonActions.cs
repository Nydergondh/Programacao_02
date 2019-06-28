using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonActions : MonoBehaviour, IDamageable {

    public static SimonActions simon;

    public int health = 5;
    public int damage = 3;
    public float walkSpeed = 5f;
    public float jumpSpeed = 5f;
    public int whipLv = 1;
    private bool invulnerable;
    public LayerMask isPlataform;
    public LayerMask enemyLayer;

    public SpriteRenderer simonRenderer;

    public Animator simonAnim;
    public Animator whipAnim;

    private GameObject enemyObj;
    private new Rigidbody2D  rigidbody;
    public BoxCollider2D feetCollider;
    public BoxCollider2D Collider;
    private BoxCollider2D simonCollider;

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
        invulnerable = false;
        simonCollider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
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
        if (!invulnerable && simonAnim.GetBool("Alive")) {
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
                    vel = new Vector2(0,rigidbody.velocity.y);
                }
                else {
                    //is not crouching
                    simonAnim.SetBool("Crouch", false);
                    vel = new Vector2(delta, rigidbody.velocity.y);
                }
                //jump
                if (CheckGround() && !simonAnim.GetBool("IsJumping") && Input.GetButtonDown("Jump") && !simonAnim.GetBool("Crouch")) {
                    Jump(vel.x);
                    simonAnim.SetBool("IsJumping", true);
                    simonAnim.SetBool("Crouch", false);
                    simonAnim.SetBool("IsWalking", false);
                }
                //is on the middle of the air
                else if (!CheckGround() && simonAnim.GetBool("IsJumping")) {

                }
                //was jumping and now touched the ground
                else if (CheckGround() && simonAnim.GetBool("IsJumping") && rigidbody.velocity.y <= 0) {
                    simonAnim.SetBool("IsJumping", false);
                    rigidbody.velocity = vel;
                }
                else {
                    rigidbody.velocity = vel;
                }
                AttColliderSize();
            }
            //check for other types of atacks
            //create an input with keys (down | s)
            CheckDirection(delta);
        }
        
    }

    void Attack() {

        // Actions : Item, Whip
        if (!simonAnim.GetBool("IsAttacking") && !invulnerable && simonAnim.GetBool("Alive")) {

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

                AttColliderSize();

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

    private void SetDamage(int dam) {
        damage = dam;
    }

    IEnumerator AttackWait() {

        yield return new WaitForSeconds(0.518f);
        //set all the attack variables to 
        simonAnim.SetBool("IsAttacking", false);
        whipAnim.SetBool("Whip", false);
        whipAnim.SetBool("CrouchWhip", false);
        //checks if GotHit to throw simon in one direction
        if (invulnerable)
        {
            ThrowSimon(enemyObj);
        }
    }

    IEnumerator ApplyDamage() {
        invulnerable = true;
        simonAnim.SetBool("GetHit", true);
        simonRenderer.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(0.518f);
        simonRenderer.color = new Color(1, 1, 1, 1);
        simonAnim.SetBool("GetHit", false);
        invulnerable = false;
    }

    public IEnumerator Die() {
        invulnerable = true;
        simonAnim.SetBool("Alive", false);
        yield return new WaitForSeconds(1.52f);
        invulnerable = false;      
    }

    public void OnDamage(int damage, GameObject enemyObject) {
        if (!invulnerable && simonAnim.GetBool("Alive"))
        {
            enemyObj = enemyObject;
            health -= damage;
            print("Damage Taken " + damage + " Current Health: " + health);
            ThrowSimon(enemyObj);
            StartCoroutine(ApplyDamage());
            if (health <= 0) {
                simonAnim.SetBool("Alive", false);
            }
        }
    }

    private void ThrowSimon(GameObject enemyObj)
    {
        if (enemyObj.transform.localScale.x == 1 && !simonAnim.GetBool("IsAttacking"))
        {
            rigidbody.velocity = new Vector2(-1, 2);
        }
        else if (enemyObj.transform.localScale.x == -1 && !simonAnim.GetBool("IsAttacking"))
        {
            rigidbody.velocity = new Vector2(1, 2);
        }
    }
    //gambiarra para atualizar o tamanho do collider
    private void AttColliderSize() {
        simonCollider.size = Collider.size;
        simonCollider.offset = Collider.offset;
    }
}
