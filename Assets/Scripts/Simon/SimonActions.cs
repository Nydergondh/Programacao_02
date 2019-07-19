using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonActions : MonoBehaviour, IDamageable {

    public static SimonActions simon;

    public int maxHealth = 16;
    public int health;
    public int damage = 1;
    public float walkSpeed = 1f;
    public float jumpSpeed = 5f;
    public int whipLv = 1;

    public bool freeJump;
    public bool transitioning;
    private bool gotHit;
    private bool invincible;
    private bool usingStairs;

    public LayerMask isPlataform;
    public LayerMask enemyLayer;

    public SpriteRenderer simonRenderer;

    public Animator simonAnim;
    public Animator whipAnim;

    private GameObject enemyObj;
    public Throables throwable { get; private set; }
    public new Rigidbody2D rigidbody { get; set; }
    public BoxCollider2D feetCollider;
    public new BoxCollider2D collider;
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
        health = maxHealth;
        usingStairs = false;
        transitioning = false;
        invincible = false;
        gotHit = false;       

        simonCollider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        throwable = GetComponentInChildren<Throables>();
        throwable.currentId = -1;
    }

    void Update() {
        if (!transitioning) {
            Movement();
            Attack();
        }
        
        else {
            Transit();
        }
        
    }

    private void Movement() {
        // Movements : Walk, Fall, Jump, Idle, Stair, Crouch

        Vector2 vel;
        float delta = Input.GetAxis("Horizontal") * walkSpeed;
        vel = new Vector2(delta, rigidbody.velocity.y);
        simonAnim.SetFloat("Speed" ,delta);
        if (!gotHit && simonAnim.GetBool("Alive")) {
            if (!simonAnim.GetBool("IsAttacking") || (simonAnim.GetBool("IsJumping") && simonAnim.GetBool("IsAttacking"))) {
                /*
                if (usingStairs) {
                    UseStairs();
                }
                */

                if (delta > 0 || delta < 0) {
                    //is walking
                    if (!simonAnim.GetBool("IsJumping")) {
                        simonAnim.SetBool("IsWalking", true);
                    }
                    else {
                        simonAnim.SetBool("IsWalking", false);
                    }
                    if (rigidbody.velocity.y == 0) {
                        vel = new Vector2(delta, rigidbody.velocity.y);
                    }
                    else {
                        vel = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y);
                    }
                }
                else {
                    //is not walking
                    simonAnim.SetBool("IsWalking", false);
                    vel = new Vector2(0, rigidbody.velocity.y);
                }

                if (Input.GetButton("Crouch") && !simonAnim.GetBool("IsJumping") && rigidbody.velocity.y == 0) {
                    //is crouching
                    simonAnim.SetBool("Crouch", true);
                    simonAnim.SetBool("IsWalking", false);
                    vel = new Vector2(0, rigidbody.velocity.y);
                }
                else {
                    //is not crouching
                    simonAnim.SetBool("Crouch", false);
                    vel = new Vector2(vel.x, rigidbody.velocity.y);
                }
                //jump and attack at the same time
                //jump
                if ((CheckGround() && !simonAnim.GetBool("IsJumping") && Input.GetButtonDown("Jump") && !simonAnim.GetBool("Crouch") && (Input.GetKeyDown(KeyCode.Z)))) {
                    Jump(vel.x);
                    simonAnim.SetBool("IsJumping", true);
                    simonAnim.SetBool("Crouch", false);
                    simonAnim.SetBool("IsWalking", false);
                    Attack();
                }
                else if (CheckGround() && !simonAnim.GetBool("IsJumping") && Input.GetButtonDown("Jump") && !simonAnim.GetBool("Crouch")) {
                    Jump(vel.x);
                    simonAnim.SetBool("IsJumping", true);
                    simonAnim.SetBool("Crouch", false);
                    simonAnim.SetBool("IsWalking", false);
                }
                //is on the middle of the air
                else if (!CheckGround() && simonAnim.GetBool("IsJumping")) {
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x,rigidbody.velocity.y);
                }
                //was jumping and now touched the ground
                else if (CheckGround() && simonAnim.GetBool("IsJumping") && rigidbody.velocity.y <= 0) {
                    simonAnim.SetBool("IsJumping", false);
                    rigidbody.velocity = vel;
                }
                else {
                    rigidbody.velocity = vel;
                }
                //is falling from plataform
                if (!simonAnim.GetBool("IsJumping") && rigidbody.velocity.y < 0) {
                    simonAnim.SetBool("IsWalking", false);
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
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
        if (!simonAnim.GetBool("IsAttacking") && !gotHit && simonAnim.GetBool("Alive")) {
            //throw stuff
            if (Input.GetKeyDown(KeyCode.Z) && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) 
                && GameManager.gameManager.canThrowIten && throwable.currentId != -1 && UI_Manager.ui_Manager.hearts > 0) {
                simonAnim.SetBool("Throw", true);
                simonAnim.SetBool("IsWalking", false);
                simonAnim.SetBool("IsAttacking", true);
                UI_Manager.ui_Manager.hearts -= 1;
                
                StartCoroutine(AttackWait());                
            } 


            else if (Input.GetKeyDown(KeyCode.Z) && (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.W))) {
                    //play whipAnim
                    if (simonAnim.GetBool("Crouch")) {
                        CrouchWhip();
                    }
                    else { 
                        Whip();
                    }

                    AttColliderSize();

                    if (!simonAnim.GetBool("IsJumping")) {
                        rigidbody.velocity = Vector2.zero;
                    }

                    simonAnim.SetBool("IsWalking", false);
                    simonAnim.SetBool("IsAttacking", true);

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
        whipAnim.speed = 1.25f;
        if (whipLv == 1) {
            damage = 1;
            whipAnim.SetBool("Whip", true);
            whipAnim.SetInteger("WhipLv", 1);
        }
        else if (whipLv == 2) {
            damage = 2;
            whipAnim.SetBool("Whip", true);
            whipAnim.SetInteger("WhipLv", 2);
        }
        else if (whipLv == 3) {
            damage = 2;
            whipAnim.SetBool("Whip", true);
            whipAnim.SetInteger("WhipLv", 3);
        }
    }

    private void CrouchWhip() {
        whipAnim.speed = 1.25f;
        if (whipLv == 1) {
            damage = 1;
            whipAnim.SetBool("CrouchWhip", true);
            whipAnim.SetInteger("WhipLv", 1);
        }
        else if (whipLv == 2) {
            damage = 2;
            whipAnim.SetBool("CrouchWhip", true);
            whipAnim.SetInteger("WhipLv", 2);
        }
        else if (whipLv == 3) {
            damage = 2;
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
        if (!simonAnim.GetBool("Throw")) {
            simonAnim.speed = 1.25f;
            yield return new WaitForSeconds(0.518f / 1.5f);
            //set all the attack variables to 
            simonAnim.speed = 1.0f;
            simonAnim.SetBool("IsAttacking", false);
            whipAnim.SetBool("Whip", false);
            whipAnim.SetBool("CrouchWhip", false);
            //checks if GotHit to throw simon in one direction
            if (gotHit) {
                DamageSimon(enemyObj);
            }
        }
        else {
            if (!simonAnim.GetBool("IsJumping")) {
                rigidbody.velocity = Vector2.zero;
            }
            simonAnim.SetBool("Throw",true);
            yield return new WaitForSeconds(0.518f / 2);
            //set all the attack variables to 
            simonAnim.SetBool("Throw", false);
            simonAnim.SetBool("IsAttacking", false);
            whipAnim.SetBool("Whip", false);
            whipAnim.SetBool("CrouchWhip", false);
            //checks if GotHit to throw simon in one direction
            if (gotHit) {
                DamageSimon(enemyObj);
            }
        }
    }

    IEnumerator ApplyDamage() {
        gotHit = true;
        simonAnim.SetBool("GetHit", true);
        StartCoroutine(Invincibility());
        yield return new WaitForSeconds(0.518f);
        simonAnim.SetBool("GetHit", false);
        gotHit = false;
    }

    IEnumerator Invincibility() {
        invincible = true;
        simonRenderer.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(3);
        simonRenderer.color = new Color(1, 1, 1, 1);
        invincible = false;
    }

    public IEnumerator Die() {
        gotHit = true;
        simonAnim.SetBool("Alive", false);
        rigidbody.velocity = Vector2.zero;

        yield return new WaitForSeconds(1.52f);
        gotHit = false;      
    }

    public void OnDamage(int damage, GameObject enemyObject) {
        if (!invincible && simonAnim.GetBool("Alive"))
        {
            enemyObj = enemyObject;
            health -= damage;
            DamageSimon(enemyObj);
            StartCoroutine(ApplyDamage());

            UI_Manager.ui_Manager.currentWidthPlayer -= damage * 7.875f;
            
            if (health <= 0) {
                simonAnim.SetBool("Alive", false);
            }
        }
    }

    private void DamageSimon(GameObject enemyObj)
    {
        if (enemyObj.transform.localScale.x == 1 && !simonAnim.GetBool("IsAttacking"))
        {
            rigidbody.velocity = new Vector2(-1, 2.5f);
        }
        else if (enemyObj.transform.localScale.x == -1 && !simonAnim.GetBool("IsAttacking"))
        {
            rigidbody.velocity = new Vector2(1, 2.5f);
        }
    }
    //gambiarra para atualizar o tamanho do collider
    private void AttColliderSize() {
        simonCollider.size = collider.size;
        simonCollider.offset = collider.offset;
    }
    //Sets Transition Stage (Called in GameManager)
    public void SetTransition() {
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        transitioning = true;       
    }

    public void Transit() {
        //castle transit
        if(!GameManager.gameManager.currentScenario.holeTransit && !GameManager.gameManager.currentScenario.doorTransit) {
            rigidbody.velocity = new Vector2(1f, rigidbody.velocity.y);
        }
        //hole transit
        else if (GameManager.gameManager.currentScenario.holeTransit && !GameManager.gameManager.currentScenario.doorTransit) {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y);
        }
        //door transit
        else if (!GameManager.gameManager.currentScenario.holeTransit && GameManager.gameManager.currentScenario.doorTransit) {
            CameraMovement camera = Camera.main.GetComponent<CameraMovement>();
            if (camera.transitCamera) {
                rigidbody.velocity = new Vector2(1f, rigidbody.velocity.y);
            }
            else {
                simonAnim.SetBool("IsWalking", false);
                rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
            }
        }
    }

    public bool CanTrasint() {
        if (simonAnim.GetBool("Alive") && !simonAnim.GetBool("IsJumping") && !simonAnim.GetBool("Crouch") && !simonAnim.GetBool("IsAttacking") && simonAnim.GetBool("IsWalking")) {
            return true;
        }
        else if (simonAnim.GetBool("Alive") && !simonAnim.GetBool("IsJumping") && !simonAnim.GetBool("Crouch") && !simonAnim.GetBool("IsAttacking") && 
                !simonAnim.GetBool("IsWalking") && (rigidbody.velocity.y > 0 || rigidbody.velocity.y < 0)) {

            return true;
        }

        else if (simonAnim.GetBool("Alive") && simonAnim.GetBool("IsJumping") && !simonAnim.GetBool("Crouch") && !simonAnim.GetBool("IsAttacking") &&
        !simonAnim.GetBool("IsWalking") && (rigidbody.velocity.y > 0 || rigidbody.velocity.y < 0) && GameManager.gameManager.currentScenario.holeTransit) {

            return true;
        }
        return false;
    }
    /*
    public void UseStairs() {

        if (Input.GetKey(KeyCode.UpArrow) && stair.currentStair < stair.stairs.Count) {
            stair.currentStair += 1;
            rigidbody.MovePosition(stair.stairs[stair.currentStair].position);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && stair.currentStair > 0) {
            stair.currentStair -= 1;
            rigidbody.MovePosition(stair.stairs[stair.currentStair].position);
        }
        else {
            rigidbody.gravityScale = 1;
            canUseStairs = false;
        }

    }
    */
    //TODO Create Metodh to consume consumable and do what they do (ALOT OF WORK)
}
