﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour
{
    public LayerMask enemyLayer;
    new Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D enemy) {
        if (collider.IsTouchingLayers(enemyLayer)) {
            var damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null) {
                print("damage = " + SimonActions.simon.damage);
                damageable.OnDamage(SimonActions.simon.damage, gameObject);
            }
        }
    }
}