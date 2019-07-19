using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : MonoBehaviour, IDamageable, IDestroyOffScreen {
    
    private new Collider2D collider;
    private Animator zombieAnim;

    private AudioSource audioSource;
    public AudioClip deathSound;

    private int health = 1;
    private int damage = 2;
    private int attackDamage;
    private bool canDestroy = false; //bad name (consider changing)

    public LayerMask simonLayer;
    public float zombieSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        zombieAnim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float xMovement = zombieSpeed * Time.deltaTime;
        // kinda a mind fuck, I spawn the zombie with a scale in X and he has to move accordingly to that(check ZombieSpawner)
        if (transform.localScale.x == 1) {
            xMovement *= -1;
        }
        if (zombieAnim.GetBool("Alive")) {
            CheckDestruction();
            transform.position = new Vector3(transform.position.x + xMovement, transform.position.y, 0);
        }

    }

    private void CheckDestruction() {
        if (Vector3.Distance(transform.position, SimonActions.simon.transform.position) <= 2) {
            canDestroy = true;
        }
        if (canDestroy) {
            OutOffScreen();
        }
    }

    public void OnDamage(int damage, GameObject gameObject) {
        health -= damage;
        if (health <= 0) {
            StartCoroutine(DestroyZombie());
        }
    }

    public IEnumerator DestroyZombie() {
        collider.enabled = false;
        zombieAnim.SetBool("Alive",false);
        audioSource.clip = deathSound;
        audioSource.Play();
        yield return new WaitForSeconds(0.355f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D enemy) {

        if (collider.IsTouchingLayers(simonLayer)) {
            var damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.OnDamage(damage, gameObject);
            }
        }

    }

    public void OutOffScreen() {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        if (!renderer.isVisible) {
            Destroy(gameObject);
        }
        
    }



}
