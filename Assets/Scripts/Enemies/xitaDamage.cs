using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xitaDamage : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private XitaVeia xitaVeia;

    void Start() {
        xitaVeia = GetComponentInParent<XitaVeia>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    

    private void OnTriggerEnter2D(Collider2D collider) {

        if (boxCollider.IsTouchingLayers(xitaVeia.groundLayer) && !xitaVeia.isOnPlatform && xitaVeia.jump) {
            xitaVeia.RunDirection();
        }

        if (boxCollider.IsTouchingLayers(xitaVeia.simonLayer)) {
            var damageable = collider.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.OnDamage(xitaVeia.damage, gameObject);
            }
        }

    }

    
}
