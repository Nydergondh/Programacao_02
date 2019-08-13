using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAssBat : MonoBehaviour, IDamageable {

    private new Collider2D collider;
    private int health = 16;
    private int damage = 2;
    private int attackDamage;

    public GameObject endGameOrb;
    public Transform endGameOrbPos;

    public Transform wanderLeft;
    public Transform wanderRigth;
    private Vector3 wanderPosition;

    private AudioSource audioSource;
    public AudioClip deathSound;

    public bool wait = true; // Used to wait some time after a attack
    public bool hitWall = false; //Used to check if the bat hit wall (redirect to alt Position)
    public bool gotHit = false; //Used to Check if the bot Got hitted by something
    public bool attack = false; //Used to check if the bat is in attack motion
    public bool goUp = false; // Used to check if the target get to the position when going up
    public bool canWander = true;

    private Animator batAssAnim;
    private Vector3 currentTargetPosition;
    private Vector3 altTargetPosition;
    private Vector3 startPosition;
    private float xDistance;

    public LayerMask simonLayer;
    public LayerMask wallLayer;

    public float batSpeed = 3f;
    public float attackTime = 4f;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        batAssAnim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        StartCoroutine(WaitTime());
        batAssAnim.SetBool("Fly", true);
        WanderSet();
    }

    // Update is called once per frame
    void Update() {
        if (batAssAnim.GetBool("Alive")) {
            if (wait) {
                batSpeed = 1.5f;
                Wander();
            }

            if (!wait && !attack) {
                batSpeed = 2f;
                attack = true;
                altTargetPosition = new Vector3(transform.position.x, transform.position.y + 1, 0);
                SetTargetPosition();
                MoveTargetPosition();
            }
        
            if (attack) {
                MoveTargetPosition();
            }
        }
    }

    public void OnDamage(int damage, GameObject gameObject) {
        health -= damage;
        UI_Manager.ui_Manager.currentWidthEnemie -= damage * 7.875f;
        if (health <= 0) {
            StartCoroutine(DestroyBat());
        }
    }

    public IEnumerator DestroyBat() {
        audioSource.clip = deathSound;
        audioSource.Play();
        batAssAnim.SetBool("Alive", false);
        collider.enabled = false;
        yield return new WaitForSeconds(0.355f);
        Instantiate(endGameOrb,endGameOrbPos.position,Quaternion.identity);
        Destroy(gameObject);
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
        //reached simon going down
        if (!goUp && Vector3.Distance(transform.position, currentTargetPosition) < 0.1f) {

            goUp = true;
            SetTargetPosition();
        }
        //goingreaching target
        else if (Vector3.Distance(transform.position, currentTargetPosition) > 0.1f) {

            transform.position = Vector3.MoveTowards(transform.position, currentTargetPosition, batSpeed * Time.deltaTime);
        }
        //going reach end goal to reach simon
        else if (goUp && Vector3.Distance(transform.position, currentTargetPosition) < 0.1f) {
            goUp = false;
            attack = false;
            wait = true;
            hitWall = false;
            StartCoroutine(WaitTime());
        }
    }

    private void SetTargetPosition() {

        if (!goUp && !hitWall) {
            currentTargetPosition = new Vector3(SimonActions.simon.transform.position.x, SimonActions.simon.transform.position.y, 0);
            xDistance = Mathf.Abs(transform.position.x - SimonActions.simon.transform.position.x);
            if (SimonActions.simon.transform.position.x < transform.position.x) {
                xDistance *= -1f;
            }
        }
        else if(goUp && !hitWall){
            currentTargetPosition = new Vector3(SimonActions.simon.transform.position.x + xDistance, altTargetPosition.y - 1, 0);
        }
        else if (hitWall && goUp) {
            currentTargetPosition = new Vector3(altTargetPosition.x, altTargetPosition.y - 1, 0);
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

    private void OnTriggerEnter2D(Collider2D enemy) {
        if (collider.IsTouchingLayers(simonLayer)) {
            var damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.OnDamage(damage, gameObject);
            }
        }

        if (collider.IsTouchingLayers(wallLayer) && !wait) {
            hitWall = true;
            SetTargetPosition();
            print("HitWall");
        }
    }


    private void Wander() {
        if(Vector3.Distance(transform.position, wanderPosition) > 0.1f) {
            transform.position = Vector3.MoveTowards(transform.position, wanderPosition, batSpeed * Time.deltaTime);
        }
        else if(canWander) {
            StartCoroutine(WaitToWander());
        }
    }

    private void WanderSet() {
        if (wait) {
            wanderPosition = WanderDestination();
        }
    }


    private Vector3 WanderDestination() {
        float minyPos = wanderLeft.position.y;
        float maxyPos = wanderRigth.position.y;
        float minxPos = wanderLeft.position.x;
        float maxxPos = wanderRigth.position.x;
        Vector3 Pos = new Vector3(Random.Range(minxPos, maxxPos), Random.Range(minyPos, maxyPos), transform.position.z);
        return Pos;
    }

    private IEnumerator WaitToWander() {
        canWander = false;
        yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        canWander = true;
        WanderSet();
    }

}
