using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {
    public float knifeSpeed = 5f;
    public int damage = 1;

    public LayerMask enemyLayer;
    public LayerMask groundLayer;

    private new SpriteRenderer renderer;
    private new Collider2D collider;
    
    void Start() {
        collider = GetComponent<Collider2D>();    
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float xMovement = (transform.position.x + knifeSpeed) * Time.deltaTime;
        transform.position = new Vector3(xMovement, transform.position.y,0);
    }

    private void OnTriggerEnter2D(Collider2D enemy) {
        if (collider.IsTouchingLayers(enemyLayer)) {
            enemy.gameObject.SendMessage("OnDamage", damage);
        }
    }
}
