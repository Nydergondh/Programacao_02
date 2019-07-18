using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChadProjectile : MonoBehaviour, IDamageable
{
    
    public float speed = 3f;
    public int damage;

    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    public Transform parentOrientation;
    public LayerMask simonLayer;

    public delegate Transform ParentDirection();
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        if (parentOrientation.localScale.x == 1)
        {
            speed *= -1;
        }
        else {
            spriteRenderer.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
        OutOffScreen();
    }

    public void OutOffScreen()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (!renderer.isVisible)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collider) {

        if (boxCollider2D.IsTouchingLayers(simonLayer)) {
            var damageable = collider.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.OnDamage(damage, gameObject);
            }
        }

    }

    public void OnDamage(int damage, GameObject gameObject) {
        Destroy(gameObject);
    }
}
