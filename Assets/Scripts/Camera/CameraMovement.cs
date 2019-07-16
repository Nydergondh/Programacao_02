using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private Camera mainCamera;
    private Transform transformFollow;

    public Transform checkPointRigth;
    public Transform checkPointLeft;

    public Transform rigthLimit;
    public Transform leftLimit;

    public bool freeMove;
    // Start is called before the first frame update

    void Start()
    {
        mainCamera = Camera.main;
        transformFollow = SimonActions.simon.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the player is moving next to a checkpóint (so that the camera stops)
        if ((transformFollow.position.x < checkPointRigth.position.x && transformFollow.position.x > checkPointLeft.position.x) || freeMove) { 
            transform.position = new Vector3(transformFollow.position.x, transform.position.y, -10);
        }
    }

    public void ChangeCheckPoints() {
        checkPointLeft = GameManager.gameManager.currentScenario.checkPointLeft;
        checkPointRigth = GameManager.gameManager.currentScenario.checkPointRigth;
    }

    public void ChangeCameraMovement() {
        freeMove = !freeMove;
    }
}
