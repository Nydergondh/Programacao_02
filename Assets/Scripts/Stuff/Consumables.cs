﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Consumables : MonoBehaviour
{
    public int idConsumable;
    public LayerMask simonLayer;
    public LayerMask groundLayer;

    private AudioSource audioSource;
    public AudioClip thePowerOfCrist;
    public AudioClip GetIten;
    public AudioClip GetBigIten;
    public AudioClip GetWhip;

    public Throables throable;
    private new Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    public new BoxCollider2D collider;
    private new SpriteRenderer renderer;

    void Start() {
        print(idConsumable);
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        throable = SimonActions.simon.GetComponentInChildren<Throables>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (boxCollider.IsTouchingLayers(groundLayer)) {
            rigidbody.velocity = Vector2.zero;
            rigidbody.isKinematic = true;
        }

        if (boxCollider.IsTouchingLayers(simonLayer)) {
            ConsumePointIten();
            ConsumeWhipIten();
            ConsumeHearts();
            ConsumeThrowableIten();
            CoxinhaDeFrango();
            FuckYouAll();
            EndGameIten();
            audioSource.Play();
            if (idConsumable != 13) {
                renderer.enabled = false;
                boxCollider.enabled = false;
                StartCoroutine(WaitToDestory());
            }
            else {
                renderer.enabled = false;
                boxCollider.enabled = false;
            }
        }
    }

    public int GetId() {
        return idConsumable;
    }

    public void ConsumePointIten() {
        if (idConsumable >= 7 && idConsumable <= 10) {
            if (idConsumable == 7) {
                UI_Manager.ui_Manager.points += 100;
            }
            else if (idConsumable == 8) {
                UI_Manager.ui_Manager.points += 200;
            }
            else if (idConsumable == 9) {
                UI_Manager.ui_Manager.points += 500;
            }
            else if (idConsumable == 10) {
                UI_Manager.ui_Manager.points += 1000;
            }
            audioSource.clip = GetIten;
        }
    }

    public void ConsumeWhipIten() {
        if (idConsumable == 4) {
            if (SimonActions.simon.whipLv < 3) {
                SimonActions.simon.whipLv += 1;
                audioSource.clip = GetWhip;
            }
        }
    }

    public void ConsumeThrowableIten() {
        if (idConsumable >= 0 && idConsumable <= 3) {
            throable.currentId = idConsumable;
            SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
            UI_Manager.ui_Manager.subweapon_data.sprite = renderer.sprite;
            UI_Manager.ui_Manager.subweapon_data.color = new Color(255, 255, 255);
            audioSource.clip = GetBigIten;
        }

    }

    public void ConsumeHearts() {

        if (idConsumable == 5) {
            UI_Manager.ui_Manager.hearts += 1;
            audioSource.clip = GetIten;
        }
        else if(idConsumable == 6) {
            UI_Manager.ui_Manager.hearts += 5;
            audioSource.clip = GetIten;
        }

    }

    public void FuckYouAll() {
        if (idConsumable == 12) {
            GameManager.gameManager.DestroyEnemys();
            audioSource.clip = thePowerOfCrist;

        }
    }

    public void CoxinhaDeFrango() {
        if (idConsumable == 11) {
            int healthRestored;
            if (SimonActions.simon.health + (SimonActions.simon.maxHealth / 2) <= SimonActions.simon.maxHealth) {
                SimonActions.simon.health += (SimonActions.simon.maxHealth / 2);
                healthRestored = 8;
                audioSource.clip = GetBigIten;
            }
            else {
                SimonActions.simon.health = SimonActions.simon.maxHealth;
                healthRestored = SimonActions.simon.maxHealth - SimonActions.simon.health;
                audioSource.clip = GetBigIten;
            }
            UI_Manager.ui_Manager.currentWidthPlayer += healthRestored * 7.875f;
        }
    }

    public void EndGameIten() {
        if (idConsumable == 13) {
            StartCoroutine(GameManager.gameManager.EndGame());
        }
    }

    public IEnumerator WaitToDestory() {
        collider.enabled = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
