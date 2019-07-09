using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermaidMan : MonoBehaviour , IDamageable
{

    private int health = 3;
    public int damage = 3;
    private float walkSpeed = 2f;

    public bool walk;
    public bool jump;
    public bool attack;
    public bool waitWalk;
    private Vector2 currentDestination;

    public LayerMask isPlataform;
    public LayerMask simonLayer;

    public GameObject projectile;

    private Animator mermaidAnim;

    private new Rigidbody2D rigidbody;
    public BoxCollider2D Collider;
    public BoxCollider2D triggerCollider;

    // Start is called before the first frame update
    void Awake() { 
        walk = false;
        waitWalk = false;
        jump = true;
        attack = false;
    }

    void Start() {
        mermaidAnim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        if(jump){
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (walk) {
            if (!attack) {
                if (!waitWalk) {
                    currentDestination = DecidePosition();
                    waitWalk = true;
                }
                if (Vector2.Distance(transform.position, currentDestination) <= 0.3) {
                    attack = true;
                    mermaidAnim.SetBool("Attack", true);
                    mermaidAnim.SetBool("Walk", false);
                    mermaidAnim.SetBool("Jump", false);
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                }
                else {
                    rigidbody.velocity = new Vector2(walkSpeed, rigidbody.velocity.y);
                }
            }
        }
        
    }

    public void OnDamage(int damage, GameObject gameObject) {
        health -= damage;
        if (health <= 0) {
            mermaidAnim.SetBool("Walk", false);
            mermaidAnim.SetBool("Attack", false);
            mermaidAnim.SetBool("Jump", false);
            mermaidAnim.SetBool("Dead", true);
            StartCoroutine(DestroyMermaid());
        }
    }

    public IEnumerator DestroyMermaid() { //sad boys
        Collider.enabled = false;
        triggerCollider.enabled = false;
        mermaidAnim.SetBool("Dead", true);
        yield return new WaitForSeconds(0.355f);
        Destroy(gameObject);
    }

    public void OutOffScreen() {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (!renderer.isVisible) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (triggerCollider.IsTouchingLayers(isPlataform) && rigidbody.velocity.y < 0) {
            walk = true;
            jump = false;
            mermaidAnim.SetBool("Walk", true);
            mermaidAnim.SetBool("Attack", false);
            mermaidAnim.SetBool("Jump", false);
            Collider.enabled = true;
        }

        if (triggerCollider.IsTouchingLayers(simonLayer)) {
            var damageable = collision.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.OnDamage(damage, gameObject);
            }
        }
    }

    private Vector2 DecidePosition() {
        //if distance from simon is far and negative
        if (transform.position.x < SimonActions.simon.transform.position.x - 2) {
            print("Gothere");
            walkSpeed = 2;
            return new Vector2(transform.position.x + 2, transform.position.y);
        }
        //if distance from simon is far and positive
        else if (transform.position.x > SimonActions.simon.transform.position.x + 2) {
            print("Gothere1");
            walkSpeed = -2;
            return new Vector2(transform.position.x - 2, transform.position.y);
        }
        //if distance from simon is negative
        else if (transform.position.x < SimonActions.simon.transform.position.x) {
            print("Gothere2");
            walkSpeed = 2;
            return new Vector2(SimonActions.simon.transform.position.x + 2, SimonActions.simon.transform.position.y);
        }
        //if distance from simon is positive
        else if (transform.position.x > SimonActions.simon.transform.position.x) {
            print("Gothere3");
            walkSpeed = -2;
            return new Vector2(SimonActions.simon.transform.position.x - 2, SimonActions.simon.transform.position.y);
        }
        //if distance = 0
        else {
            print("Gothere4");
            return new Vector2(transform.position.x + 2, transform.position.y);
        }
    }
    //event used in the end of an attack
    public void ChangeAttack() {
        attack = false;
        waitWalk = false;
        mermaidAnim.SetBool("Attack",false);
        mermaidAnim.SetBool("Walk", true);
        mermaidAnim.SetBool("Jump", false);
    }
    //event called in the Attack animation
    public void ShotProjectile() {
        //Instantiate(projectile, transform.position, Quaternion.identity, transform);
    }
}
