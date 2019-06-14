using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private Camera mainCamera;
    public Transform positionFollow;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        transform.position = new Vector3(positionFollow.position.x, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (positionFollow.position.x, 0, -10);
    }
}
