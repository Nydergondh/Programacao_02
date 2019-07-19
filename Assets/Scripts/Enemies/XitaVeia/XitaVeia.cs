using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XitaVeia : MonoBehaviour, IDamageable, IDestroyOffScreen
{
    private float xitaSpeed = 2.5f;
    private int health = 1;

    public float xitaRange = 2f;
    public int xitaHealth { get; set; } = 2;
    public int damage = 2;

    private AudioSource audioSource;
    public AudioClip deathSound;

    public bool jump;
    public bool isOnPlatform;
    public bool run;

    public LayerMask enemyCollider;
    public LayerMask groundLayer;
    public LayerMask simonLayer;

    public Animator xitaAnim { get; set; }
    public new Rigidbody2D rigidbody { get; set; }
    private BoxCollider2D xitaCollider;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        run = false;
        isOnPlatform = true;
        jump = false;
        xitaAnim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        xitaCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        if (!xitaAnim.GetBool("Dead")) {
            if (run) {
                rigidbody.velocity = new Vector2(xitaSpeed, rigidbody.velocity.y);
                OutOffScreen();
            }

            else if ((Vector3.Distance(transform.position, SimonActions.simon.transform.position) < xitaRange) && isOnPlatform) {
                if (transform.localScale.x == 1 && !xitaAnim.GetBool("Run")) {
                    xitaSpeed *= -1;
                    xitaAnim.SetBool("Run", true);
                }
                rigidbody.velocity = new Vector2(xitaSpeed, rigidbody.velocity.y);
            }
        }
        else {
            rigidbody.velocity = Vector2.zero;
        }
    }

    public void RunDirection() {

        if (jump && !isOnPlatform) {
            if (SimonActions.simon.transform.position.x > transform.position.x) {
                if (transform.localScale.x == 1) {
                    transform.localScale = new Vector3(-1, 1, 1);
                    xitaSpeed *= -1;
                }
            }
            else if (SimonActions.simon.transform.position.x < transform.position.x) {
                transform.localScale = new Vector3(1, 1, 1);
                xitaSpeed *= -1;
            }
            run = true;
            jump = false;
            xitaAnim.SetBool("Run", run);
            xitaAnim.SetBool("Jump", jump);
        }
    }

    public void OnDamage(int damage, GameObject gameObject) {
        health -= damage;
        if (health <= 0) {
            xitaAnim.SetBool("Run", false);
            xitaAnim.SetBool("Jump", false);
            xitaAnim.SetBool("Dead", true);
            StartCoroutine(DestroyXita());
        }
    }


    public IEnumerator DestroyXita() { //sad boys
        xitaAnim.SetBool("Dead", true);
        rigidbody.velocity = Vector2.zero;
        audioSource.clip = deathSound;
        audioSource.Play();
        yield return new WaitForSeconds(0.355f);
        Destroy(gameObject);
    }

    public void OutOffScreen() {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (!renderer.isVisible) {
            Destroy(gameObject);
        }
    }

}
