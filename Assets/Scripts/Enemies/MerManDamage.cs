using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerManDamage : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private MermaidMan mermaid;

    void Start() {
        mermaid = GetComponentInParent<MermaidMan>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collider) {

        if (boxCollider.IsTouchingLayers(mermaid.simonLayer)) {
            var damageable = collider.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.OnDamage(mermaid.damage, gameObject);
            }
        }

    }
}
