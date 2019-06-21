using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;
    private Animator holyWaterAnim;

    public int damage = 1;
    public float yVelocity = 2.5f;
    public float xVelocity = 2f;

    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
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
            //enemy.gameObject.SendMessage("OnDamage", damage);
            holyWaterAnim.SetBool("HasHit",true);
            rigidbody.velocity = Vector2.zero;
            rigidbody.isKinematic = true;
        }
    }
    
    public void DestroyWater() {
        Destroy(gameObject);
    }
}
