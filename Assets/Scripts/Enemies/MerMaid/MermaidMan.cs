using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermaidMan : MonoBehaviour , IDamageable
{

    private int health = 1;
    public int damage = 2;
    private float walkSpeed = 1f;
    public float jumpSpeed = 7.25f;

    public bool walk;
    public bool jump;
    public bool attack;
    public bool waitWalk;
    private Vector2 currentDestination;

    public LayerMask isPlataform;
    public LayerMask simonLayer;

    public GameObject projectile;
    public Transform projectileSpawn;

    public Animator mermaidAnim { get; private set; }

    private new Rigidbody2D rigidbody;
    public new BoxCollider2D collider;
    public BoxCollider2D triggerCollider;
    // Start is called before the first frame update
    void Awake() { 
        walk = false;
        waitWalk = false;
        jump = true;
        attack = false;
    }

    void Start() {
        collider = GetComponent<BoxCollider2D>();
        mermaidAnim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(0, jumpSpeed);
        //StartCoroutine(WaitBegin());
    }

    // Update is called once per frame
    void Update()
    {
        if (walk && rigidbody.velocity.y == 0 && !mermaidAnim.GetBool("Dead"))
        {

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
                    mermaidAnim.SetBool("Walk", true);
                    mermaidAnim.SetBool("Jump", false);
                }
            }
        }
        OutOffScreen();
    }

    public void OnDamage(int damage, GameObject gameObject) {
        health -= damage;
        print("OnDamage");
        if (health <= 0) {
            mermaidAnim.SetBool("Walk", false);
            mermaidAnim.SetBool("Attack", false);
            mermaidAnim.SetBool("Jump", false);
            mermaidAnim.SetBool("Dead", true);
            StartCoroutine(DestroyMermaid());
        }
    }

    public IEnumerator DestroyMermaid() { //sad boys
        rigidbody.velocity = Vector2.zero;
        rigidbody.isKinematic = true;
        yield return new WaitForSeconds(0.355f);
        Destroy(gameObject);
    }

    public void OutOffScreen() {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (!renderer.isVisible) {
            Destroy(gameObject);
        }
    }

    private Vector2 DecidePosition() {
        //if distance from simon is far and negative
        if (transform.position.x < SimonActions.simon.transform.position.x - 1.5f) {
            print("GotHere");
            walkSpeed = 1;
            transform.localScale = new Vector3(-1,1,1);
            return new Vector2(transform.position.x + 1.5f, transform.position.y);
        }
        //if distance from simon is far and positive
        else if (transform.position.x > SimonActions.simon.transform.position.x + 1.5f) {
            print("GotHere3");
            walkSpeed = -1;
            transform.localScale = new Vector3(1, 1, 1);
            return new Vector2(transform.position.x - 1.5f, transform.position.y);
        }
        //if distance from simon is negative
        else if (transform.position.x < SimonActions.simon.transform.position.x) {
            print("GotHere2");
            walkSpeed = 1;
            transform.localScale = new Vector3(-1, 1, 1);
            return new Vector2(SimonActions.simon.transform.position.x + 1.5f, transform.position.y);
        }
        //if distance from simon is positive
        else if (transform.position.x > SimonActions.simon.transform.position.x) {
            print("GotHere3");
            walkSpeed = -1;
            transform.localScale = new Vector3(1, 1, 1);
            return new Vector2(SimonActions.simon.transform.position.x - 1.5f, transform.position.y);
        }
        //if distance = 0
        else {
            walkSpeed = 1;
            transform.localScale = new Vector3(-1, 1, 1);
            return new Vector2(transform.position.x + -1.5f, transform.position.y);
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
        GameObject proj = Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
        ChadProjectile chad = proj.GetComponent<ChadProjectile>();
        chad.parentOrientation = transform;
        chad.damage = damage;
    }

    IEnumerator WaitBegin() {
        yield return new WaitForSeconds(2f);

    }
}
