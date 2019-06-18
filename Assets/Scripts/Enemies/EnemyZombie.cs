using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : MonoBehaviour {

    private new Collider2D collider;
    private int health;
    private int attackDamage;

    public float zombieSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = (transform.position.x + zombieSpeed) * Time.deltaTime;
        transform.position = new Vector3(xMovement, transform.position.y, 0);
    }

    void OnDamage(int damage) {
        health =- damage;
        if (health <= 0) {
            Destroy(transform.gameObject);
        }
    } 
}
