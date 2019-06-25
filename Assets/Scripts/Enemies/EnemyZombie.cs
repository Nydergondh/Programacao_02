﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : MonoBehaviour, IDamageable {
    
    private new Collider2D collider;
    private int health = 3;
    private int damage = 1;
    private int attackDamage;
    private Animator zombieAnim;

    public LayerMask simonLayer;
    public float zombieSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        zombieAnim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float xMovement = zombieSpeed * Time.deltaTime;
        // kinda a mind fuck, I spawn the zombie with a scale in X and he has to move accordingly to that(check ZombieSpawner)
        if (transform.localScale.x == 1) {
            xMovement *= -1;
        }
        if (zombieAnim.GetBool("Alive")) {
            transform.position = new Vector3(transform.position.x + xMovement, transform.position.y, 0);
        }
    }

    public void OnDamage(int damage, GameObject gameObject) {
        health -= damage;
        if (health <= 0) {
            StartCoroutine(DestroyZombie());
        }
    }

    public IEnumerator DestroyZombie() {
        zombieAnim.SetBool("Alive",false);
        yield return new WaitForSeconds(0.355f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D enemy) {

        if (collider.IsTouchingLayers(simonLayer)) {
            var damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.OnDamage(damage, gameObject);
            }
        }

    }

}
