using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    public GameObject Prefab; 
    private new CameraMovement camera;
    public int scenarioId;
   
    private bool canSpawn; //if is allowd to spawn a mod
    private bool waitTime;
    //limiters for movement of spawn  (only MerMan Spawn)
    public Transform limitLeft;
    public Transform limitRigth;
    public Transform enemieParent;
    // types of spawn
    public bool zombieSpawn;
    public bool batSpawn;
    public bool merManSpawn;

    private float spawnSpeed = 1f; // speed in witch the spawn is going to move (only MerMan Spawn)
    public float timeToSpawn = 3f; // time to Spawn a new mob
    
    public float DistanceSpawn = 2f; 
    public float DistanceToStop = 6f;
    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        waitTime = false;
        camera = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update() {
        if (zombieSpawn) {
            ZombieCheck();
        }
        else if (batSpawn) {
            BatCheck();
        }
        else if (merManSpawn) {
            MerManCheck();
        }
    }

    private void BatCheck() {
        if (Vector3.Distance(transform.position, SimonActions.simon.transform.position) > DistanceSpawn &&
            Vector3.Distance(transform.position, SimonActions.simon.transform.position) < DistanceToStop && scenarioId == GameManager.gameManager.i) {

            if (batSpawn && !waitTime) {
                if (transform.childCount == 0) {
                    canSpawn = true;
                }
            }

            if (CheckSpawnerSide() == Direction.Rigth && canSpawn) {
                StartCoroutine(Spawn(Direction.Rigth));
            }
            else if (CheckSpawnerSide() == Direction.Left && canSpawn) {
                StartCoroutine(Spawn(Direction.Left));
            }
            else if (CheckSpawnerSide() == Direction.Middle && canSpawn && batSpawn) {
                StartCoroutine(Spawn(Direction.Middle));
            }
        }
    }

    private void ZombieCheck() {
        if (Mathf.Abs(transform.position.x - SimonActions.simon.transform.position.x) > DistanceSpawn &&
                    Mathf.Abs(transform.position.x - SimonActions.simon.transform.position.x) < DistanceToStop && scenarioId == GameManager.gameManager.i) {

            if (CheckSpawnerSide() == Direction.Rigth && canSpawn) {
                StartCoroutine(Spawn(Direction.Rigth));
            }
            else if (CheckSpawnerSide() == Direction.Left && canSpawn) {
                StartCoroutine(Spawn(Direction.Left));
            }
        }
    }

    private void MerManCheck() {
        if (GameManager.gameManager.i == scenarioId) {
            if (transform.position.x < limitLeft.position.x || transform.position.x > limitRigth.position.x) {
                spawnSpeed *= -1f;
            }
            float xpos = spawnSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + xpos, transform.position.y, transform.position.z);

            if (Vector3.Distance(transform.position, SimonActions.simon.transform.position) > DistanceSpawn &&
               Vector3.Distance(transform.position, SimonActions.simon.transform.position) < DistanceToStop && scenarioId == GameManager.gameManager.i) {


                if (merManSpawn && !waitTime) {
                    if (enemieParent.childCount == 3) {
                        canSpawn = true;
                    }
                }

                if (CheckSpawnerSide() == Direction.Rigth && canSpawn) {
                    StartCoroutine(Spawn(Direction.Rigth));
                }
                else if (CheckSpawnerSide() == Direction.Left && canSpawn) {
                    StartCoroutine(Spawn(Direction.Left));
                }
                else if (CheckSpawnerSide() == Direction.Middle && canSpawn && merManSpawn) {
                    StartCoroutine(Spawn(Direction.Middle));
                }
            }
        }
    }

    Direction CheckSpawnerSide() {
        //return the current direction of the spawner based on its position relative to the camera edges

        if (transform.position.x > camera.rigthLimit.transform.position.x + 1f) {
            return Direction.Rigth;
        }
        else if(transform.position.x < camera.leftLimit.position.x - 1f) {
            return Direction.Left;
        }
        else {
            return Direction.Middle;
        }
    }

    enum Direction {
        Rigth,
        Left,
        Middle
    }

    IEnumerator Spawn(Direction direction) {
        canSpawn = false;
        

        if (zombieSpawn) {
            GameObject obj = Instantiate(Prefab, transform.position, Quaternion.identity, enemieParent);
            if (direction == Direction.Rigth) {
                obj.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction == Direction.Left) {
                obj.transform.localScale = new Vector3(-1, 1, 1);
            }
            yield return new WaitForSeconds(timeToSpawn);
            canSpawn = true;
        }
        else if (batSpawn) {
            waitTime = true;
            Vector3 spawnPos = new Vector3(transform.position.x, SimonActions.simon.transform.position.y, transform.position.z);
            GameObject obj = Instantiate(Prefab, spawnPos, Quaternion.identity, enemieParent);
            yield return new WaitForSeconds(timeToSpawn);
            waitTime = false;
        }
        else if (merManSpawn) {
            waitTime = true;
            Vector3 spawnPos = new Vector3(transform.position.x, SimonActions.simon.transform.position.y, transform.position.z);
            Instantiate(Prefab, transform.position, Quaternion.identity, enemieParent);
            yield return new WaitForSeconds(timeToSpawn);
            waitTime = false;
        }
    }
}
