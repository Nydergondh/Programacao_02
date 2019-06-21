using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;
    private Animator axeAnim;

    public int damage = 1;
    public float yVelocity = 4f;
    public float xVelocity = 3f;

    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        axeAnim = GetComponent<Animator>();
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
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D enemy) {
        if (collider.IsTouchingLayers(enemyLayer)) {
            enemy.gameObject.SendMessage("OnDamage", damage);
        }

        if (collider.IsTouchingLayers(groundLayer)) {
            //enemy.gameObject.SendMessage("OnDamage", damage);
            Destroy(gameObject);
        }

    }
}
