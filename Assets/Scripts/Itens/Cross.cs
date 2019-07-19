using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{
    public float crossSpeed = 5f;
    public int damage = 2;
    private bool comeBack; // :( I miss u Sophia, come back Please.
    private bool comeRigth;
    public LayerMask enemyLayer;
    public LayerMask groundLayer;

    private AudioSource audioSource;
    public AudioClip crossShoot;

    private Transform cameraLeft;
    private Transform cameraRigth;
    private CameraMovement cameraMovement;

    private new SpriteRenderer renderer;
    private new Collider2D collider;

    void Start() {

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = crossShoot;
        audioSource.Play();
        GameManager.gameManager.canThrowIten = false;
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
        cameraRigth = cameraMovement.rigthLimit;
        cameraLeft = cameraMovement.leftLimit;
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();

        if (SimonActions.simon.transform.localScale.x == 1) {
            renderer.flipX = true;
            comeRigth = true;
            comeBack = false;
            crossSpeed *= -1f;
        }
        else {
            comeRigth = false;
        }
    }

    void Update() {

        if (transform.position.x >= cameraRigth.position.x || transform.position.x <= cameraLeft.position.x) {
            comeBack = true;
            crossSpeed *= -1f;
        }
        if (comeBack && ((comeRigth && transform.position.x > SimonActions.simon.transform.position.x) || (!comeRigth && transform.position.x < SimonActions.simon.transform.position.x))) {
            GameManager.gameManager.canThrowIten = true;
            Destroy(gameObject);
        }
        float xMovement = crossSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + xMovement, transform.position.y, 0);
        //print("Cross: "+ transform.position.x + " Player: " + SimonActions.simon.transform.position.x);
    }

    private void OnTriggerEnter2D(Collider2D enemy) {
        if (collider.IsTouchingLayers(enemyLayer)) {
            var damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.OnDamage(damage, gameObject);
            }
        }

        if (collider.IsTouchingLayers(groundLayer)) {
            GameManager.gameManager.canThrowIten = true;
            Destroy(gameObject);
        }

    }
}
