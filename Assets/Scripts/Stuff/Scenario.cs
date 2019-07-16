using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    //add checkpoints to define where the camera has to stop following the charactor
    public Transform checkPointLeft;
    public Transform checkPointRigth;
    public LayerMask simonLayer;
    private EdgeCollider2D edgeCollider2D;

    void Start() {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(edgeCollider2D.IsTouchingLayers(simonLayer)) {
            GameManager.gameManager.ChangeScene();
            CameraMovement currentCamera = Camera.main.GetComponent<CameraMovement>();
            currentCamera.ChangeCameraMovement();
            SimonActions.simon.WaitForTransition();
        }
    }
}
