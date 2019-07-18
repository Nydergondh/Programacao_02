using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    //add checkpoints to define where the camera has to stop following the charactor
    public Transform checkPointLeft;
    public Transform checkPointRigth;
    public Transform target;
    public Vector3 StartCameraPosition;
    public bool isChanging = false;
    public bool holeTransit = false;
    public bool doorTransit = false;
    public bool bossScenario = false;

    public LayerMask simonLayer;

    private EdgeCollider2D edgeCollider2D;

    public BoxCollider2D LeftCollider;
    public BoxCollider2D RigthCollider;

    void Start() {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(edgeCollider2D.IsTouchingLayers(simonLayer) && !isChanging && SimonActions.simon.CanTrasint()) {
            isChanging = true;
            CameraMovement currentCamera = Camera.main.GetComponent<CameraMovement>();
            currentCamera.ChangeCameraMovement();
            if (!bossScenario) {
                LeftCollider.enabled = false;
                RigthCollider.enabled = false;
            }
            else {
                LeftCollider.enabled = true;
                RigthCollider.enabled = true;
            }
            if (doorTransit) {
                currentCamera.transitCamera = true;
            }
            StartCoroutine(GameManager.gameManager.WaitForTransition());
            if (bossScenario) {
                edgeCollider2D.enabled = false;
            }
        }
    }
}
