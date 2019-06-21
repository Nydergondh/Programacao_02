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
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        transformFollow = SimonActions.simon.transform;
        transform.position = new Vector3(transformFollow.position.x, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the player is moving next to a checkpóint (so that the camera stops)
        if (transformFollow.position.x < checkPointRigth.position.x && transformFollow.position.x > checkPointLeft.position.x) {
            transform.position = new Vector3(transformFollow.position.x, 0, -10);
        }
    }
}
