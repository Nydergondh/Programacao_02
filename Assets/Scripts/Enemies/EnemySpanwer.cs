using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    public GameObject ZombiesPrefab; 
    private new CameraMovement camera;
    private bool canSpawn;
    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        camera = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckSpawnerSide() == Direction.Rigth && canSpawn) {
            StartCoroutine(SpawnZombie(Direction.Rigth));
        }
        else if (CheckSpawnerSide() == Direction.Left && canSpawn) {
            StartCoroutine(SpawnZombie(Direction.Left));
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

    IEnumerator SpawnZombie(Direction direction) {
        canSpawn = false;

        GameObject zombie = Instantiate(ZombiesPrefab, transform.position, Quaternion.identity, transform);

        if (direction == Direction.Rigth) {
            zombie.transform.localScale = new Vector3(1,1,1);
        }
        else if(direction == Direction.Left) {
            zombie.transform.localScale = new Vector3(-1, 1, 1);
        }

        yield return new WaitForSeconds(3f);
        canSpawn = true;
    }
}
