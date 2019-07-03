using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {
    public float knifeSpeed = 5f;
    public int damage = 3;

    public LayerMask enemyLayer;
    public LayerMask groundLayer;

    private new SpriteRenderer renderer;
    private new Collider2D collider;
    private CameraMovement cameraMovement;

    void Start() {
        GameManager.gameManager.canThrowIten = false;
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
        collider = GetComponent<Collider2D>();    
        renderer = GetComponent<SpriteRenderer>();
        if (SimonActions.simon.transform.localScale.x == 1) {
            renderer.flipX = true;
            knifeSpeed *= -1f;
        }
    }

    void Update()
    {
        if(transform.position.x > cameraMovement.rigthLimit.position.x || transform.position.x < cameraMovement.leftLimit.position.x) {
            Destroy(gameObject);
        }
        float xMovement = knifeSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + xMovement, transform.position.y,0);
    }

    private void OnTriggerEnter2D(Collider2D enemy) {
        if (collider.IsTouchingLayers(enemyLayer)) {
            var damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.OnDamage(damage, gameObject);
            }
        }

        if (collider.IsTouchingLayers(groundLayer)) {
            GameManager.gameManager.canThrowIten = true;
            Destroy(gameObject);
        }

    }

}
