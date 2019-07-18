using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diluvio : MonoBehaviour
{
    private int damage;
    public LayerMask simonLayer;
    private BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        damage = 5000;
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collider) {
        if (boxCollider.IsTouchingLayers(simonLayer)) {
            var damageable = collider.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.OnDamage(damage, gameObject);
            }
        }
    }
}
