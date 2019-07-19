using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;
    private Animator holyWaterAnim;

    private AudioSource audioSource;
    public AudioClip waterHit;

    public int damage = 3;
    public float yVelocity = 2.5f;
    public float xVelocity = 2f;

    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = waterHit;
        audioSource.Play();
        GameManager.gameManager.canThrowIten = false;
        holyWaterAnim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        if (SimonActions.simon.transform.localScale.x == -1) {
            rigidbody.velocity = new Vector2(xVelocity, yVelocity);
        }
        else {
            rigidbody.velocity = new Vector2(-xVelocity, yVelocity);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D enemy) {
        if (collider.IsTouchingLayers(enemyLayer) || collider.IsTouchingLayers(groundLayer)) {
            holyWaterAnim.SetBool("HasHit", true);
            rigidbody.velocity = Vector2.zero;
            rigidbody.isKinematic = true;

            if (collider.IsTouchingLayers(enemyLayer)) {
                var damageable = enemy.GetComponent<IDamageable>();
                if (damageable != null) {
                    damageable.OnDamage(damage, gameObject);
                    StartCoroutine(DestroyWater());
                }
            }

            if (collider.IsTouchingLayers(groundLayer)) {
                StartCoroutine(DestroyWater());
            }
        }

    }
    
    public IEnumerator DestroyWater() {
        yield return new WaitForSeconds(1.1f);
        GameManager.gameManager.canThrowIten = true;
        Destroy(gameObject);
    }
}
