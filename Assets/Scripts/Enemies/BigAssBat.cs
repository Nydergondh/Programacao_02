using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAssBat : MonoBehaviour
{
    private new Collider2D collider;
    private int health = 1;
    private int damage = 1;
    private int attackDamage;

    private bool wait = true; // Used to wait some time after a attack
    private bool hitWall = false; //Used to check if the bat hit wall (redirect to alt Position)
    private bool gotHit = false; //Used to Check if the bot Got hitted by something
    private bool attack = false; //Used to check if the bat is in attack motion
    private bool goUp = false; // Used to check if the target get to the position when going up

    private Animator batAssAnim;
    private Vector3 currentTargetPosition;
    private Vector3 altTargetPosition;
    private Vector3 startPosition;
    private float xDistance;

    public LayerMask simonLayer;

    public float batSpeed = 3f;
    public float attackTime = 4f;
    private float currentTime = 0f;
    private float timerAttack = 0f;

    // Start is called before the first frame update
    void Start() {
        batAssAnim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        StartCoroutine(WaitTime());
    }

    // Update is called once per frame
    void Update() {
        if (!wait && !attack) {
            attack = true;
            altTargetPosition = new Vector3(transform.position.x, transform.position.y + 1, 0);
            SetTargetPosition();
            MoveTargetPosition();
        }
        if (attack) {
            MoveTargetPosition();
        }
    }

    public void OnDamage(int damage, GameObject gameObject) {
        health -= damage;
        if (health <= 0) {
            StartCoroutine(DestroyBat());
        }
    }

    public IEnumerator DestroyBat() {
        batAssAnim.SetBool("Alive", false);
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
    /*
    public void SetDestination() {
        timerAttack = 0f;       
        startPosition = transform.position;
        SetCurrentPosition();
    }

    private void SetCurrentPosition() {
        //TODO move this somewhere else
        //put this on update with an if statement

        float xDistance = Mathf.Abs(transform.position.x - SimonActions.simon.transform.position.x);

        if (transform.position.x < SimonActions.simon.transform.position.x) {
            currentTargetPosition = new Vector3(SimonActions.simon.transform.position.x, transform.position.y, 0);
        }
        else {
            currentTargetPosition = new Vector3(SimonActions.simon.transform.position.x + xDistance, transform.position.y, 0);
        }
        
    }


    private void MoveTargetPosition() {
        float timer = attackTime / 2;

        currentTime += Time.deltaTime / attackTime;
        timerAttack += Time.deltaTime / attackTime;
        print("Current Time " + currentTime);
        print("timeAttck " + timerAttack);
        if (timerAttack < timer) {
            transform.position = Vector3.Lerp(startPosition, currentTargetPosition, timerAttack);
            print("Got Here");
            if (Vector3.Distance( currentTargetPosition, transform.position) >= 0.1f && goUp) {
                goUp = false;
                attack = false;
                wait = true;
                StartCoroutine(WaitTime());
            }
        }
        else {
            print("Got Here1");
            goUp = true;
            SetDestination();
            transform.position = Vector3.Lerp(startPosition, currentTargetPosition, timerAttack);
        }
    }
    */


    private void MoveTargetPosition() {
        if (!goUp && Vector3.Distance(transform.position, currentTargetPosition) < 0.1f) {

            goUp = true;
            SetTargetPosition();
        }
        else if (Vector3.Distance(transform.position, currentTargetPosition) > 0.1f) {

            transform.position = Vector3.MoveTowards(transform.position, currentTargetPosition, batSpeed * Time.deltaTime);
        }
        else if(goUp && Vector3.Distance(transform.position, currentTargetPosition) < 0.1f) {
            goUp = false;
            attack = false;
            wait = true;
            StartCoroutine(WaitTime());
        }
    }

    private void SetTargetPosition() {

        if (!goUp) {
            currentTargetPosition = new Vector3(SimonActions.simon.transform.position.x, SimonActions.simon.transform.position.y, 0);
            xDistance = Mathf.Abs(transform.position.x - SimonActions.simon.transform.position.x);
            if (SimonActions.simon.transform.position.x < transform.position.x) {
                xDistance *= -1f;
            }
        }
        else {
            currentTargetPosition = new Vector3(SimonActions.simon.transform.position.x + xDistance, altTargetPosition.y -1, 0);
        }
    }

    IEnumerator WaitTime() {
        SetTargetPosition();
        yield return new WaitForSeconds(RandomTime());
        wait = false;
    }

    private float RandomTime() {
        return Random.Range(3f, 5f);
    }
}
