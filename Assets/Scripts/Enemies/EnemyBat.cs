using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour, IDamageable, IDestroyOffScreen
{
    private new Collider2D collider;
    private int health = 1;
    private int damage = 1;
    private int attackDamage;
    private Animator batAnim;

    public LayerMask simonLayer;
    public float batSpeed = 3f;

    // Start is called before the first frame update
    void Start() {
        batAnim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        if(transform.position.x < SimonActions.simon.transform.position.x) {
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    // Update is called once per frame
    void Update() {
        if (Vector3.Distance(transform.position, SimonActions.simon.transform.position) <= 3f || batAnim.GetBool("Active")) {
            MoveBat();
            OutOffScreen();
        }
    }

    private void MoveBat() {
        batAnim.SetBool("Active", true);
        float xMovement = batSpeed * Time.deltaTime;
        if (transform.localScale.x == 1) {
            xMovement *= -1;
        }
        if (batAnim.GetBool("Active") && batAnim.GetBool("Alive")) {
            transform.position = new Vector3(transform.position.x + xMovement, transform.position.y, 0);
        }
    }

    public void OnDamage(int damage, GameObject gameObject) {
        health -= damage;
        if (health <= 0) {
            StartCoroutine(DestroyBat());
        }
    }

    public IEnumerator DestroyBat() {
        batAnim.SetBool("Alive", false);
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
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        if (!renderer.isVisible) {
            Destroy(gameObject);
        }        
    }
}
