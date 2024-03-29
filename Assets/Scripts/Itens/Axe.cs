﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;
    private Animator axeAnim;

    private AudioSource audioSource;
    public AudioClip axeShoot;

    public int damage = 2;
    public float yVelocity = 4f;
    public float xVelocity = 3f;

    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = axeShoot;
        audioSource.Play();
        GameManager.gameManager.canThrowIten = false;
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

    private void OnTriggerEnter2D(Collider2D enemy) {
        if (collider.IsTouchingLayers(enemyLayer)) {
            var damageable = enemy.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.OnDamage(damage, gameObject);
            }
        }

        if (collider.IsTouchingLayers(groundLayer)) {
            GameManager.gameManager.canThrowIten = true;
            Destroy(gameObject);
        }

    }
}
