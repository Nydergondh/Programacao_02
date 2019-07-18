using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Camera mainCamera;
    private Transform transformFollow;

    public bool testMode = false;
    public bool transitCamera = false;
    private float speedTransit = 2f;

    public Transform checkPointRigth;
    public Transform checkPointLeft;

    public Transform rigthLimit;
    public Transform leftLimit;

    public bool freeMove;
    // Start is called before the first frame update

    void Start() {
        freeMove = false;
        mainCamera = Camera.main;
        transformFollow = SimonActions.simon.transform;
    }

    // Update is called once per frame
    void Update() {
        //checks if the player is moving next to a checkpóint (so that the camera stops)
        if (SimonActions.simon.simonAnim.GetBool("Alive")) {
            if (testMode) {
                transform.position = new Vector3(transformFollow.position.x, transform.position.y, -10);
            }
            else if (!transitCamera) {
                if ((transformFollow.position.x < checkPointRigth.position.x && transformFollow.position.x > checkPointLeft.position.x) && !freeMove && !testMode) {
                    transform.position = new Vector3(transformFollow.position.x, transform.position.y, -10);
                }
            }
            else if (transitCamera) {
                float xpos = speedTransit * Time.deltaTime;
                transform.position = new Vector3(transform.position.x + xpos, transform.position.y, -10);

                if (transform.position.x >= GameManager.gameManager.currentScenario.target.position.x) {
                    transitCamera = false;
                }
            }
        }

    }

    public void ChangeCheckPoints() {
        checkPointLeft = GameManager.gameManager.currentScenario.checkPointLeft;
        checkPointRigth = GameManager.gameManager.currentScenario.checkPointRigth;
    }

    public void ChangeCameraMovement() {
        freeMove = !freeMove;
    }
    public void MoveCameraToPlayer() {
        transform.position = new Vector3(transformFollow.position.x, transform.position.y, -10);
    }
}
