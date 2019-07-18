﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public Scenario currentScenario;
    public Scenario[] scenarios;
    public bool canThrowIten = true;
    public int i = 0;

    public delegate int ConsumableConsumed();
    public ConsumableConsumed Consumed;
    // Start is called before the first frame update
    void Awake() {
        if (gameManager == null) {
            gameManager = this;
        }
        else {
            Destroy(gameManager);
            gameManager = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!SimonActions.simon.simonAnim.GetBool("Alive")) {
            StartCoroutine(SimonActions.simon.Die());
            StartCoroutine(WaitEndGame());
        }
    }

    private IEnumerator WaitEndGame() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Level");
    }

    public void ChangeScene() {
        i += 1;;
        currentScenario = scenarios[i];
    }

    public IEnumerator WaitForTransition() {
        //case boss room
        if (currentScenario.bossScenario) {
            yield return new WaitForSeconds(0f);
            currentScenario.isChanging = true;
            currentScenario.LeftCollider.enabled = true;
            currentScenario.RigthCollider.enabled = true;
            ChangeScene();

            CameraMovement currentCamera = Camera.main.GetComponent<CameraMovement>();
            currentCamera.ChangeCheckPoints();
            currentCamera.ChangeCameraMovement();
        }
        //case door transit
        else if (currentScenario.doorTransit && !currentScenario.holeTransit) {
            SimonActions.simon.SetTransition();
            yield return new WaitForSeconds(3f);

            currentScenario.isChanging = true;
            currentScenario.LeftCollider.enabled = true;
            currentScenario.RigthCollider.enabled = true;
            ChangeScene();

            CameraMovement currentCamera = Camera.main.GetComponent<CameraMovement>();
            currentCamera.transform.position = currentScenario.StartCameraPosition;
            currentCamera.ChangeCheckPoints();
            currentCamera.ChangeCameraMovement();

            SimonActions.simon.transitioning = false;
        }
        //case hole transit
        else if (!currentScenario.doorTransit && currentScenario.holeTransit) {
            SimonActions.simon.SetTransition();
            yield return new WaitForSeconds(2f);

            currentScenario.isChanging = true;
            currentScenario.LeftCollider.enabled = true;
            currentScenario.RigthCollider.enabled = true;
            ChangeScene();

            CameraMovement currentCamera = Camera.main.GetComponent<CameraMovement>();
            currentCamera.transform.position = currentScenario.StartCameraPosition;
            currentCamera.ChangeCheckPoints();
            currentCamera.ChangeCameraMovement();

            SimonActions.simon.transitioning = false;
        }
        //case castle transit
        else if (!currentScenario.doorTransit && !currentScenario.holeTransit) {
            SimonActions.simon.SetTransition();
            yield return new WaitForSeconds(3.5f);

            currentScenario.isChanging = true;
            currentScenario.LeftCollider.enabled = true;
            currentScenario.RigthCollider.enabled = true;
            ChangeScene();

            CameraMovement currentCamera = Camera.main.GetComponent<CameraMovement>();
            currentCamera.transform.position = currentScenario.StartCameraPosition;
            currentCamera.ChangeCheckPoints();
            currentCamera.ChangeCameraMovement();

            SimonActions.simon.transitioning = false;
        }

    }

    public void FreezeEnemies() {
        EnemyBat[] enemieBat = FindObjectsOfType<EnemyBat>();
        XitaVeia[] enemieXita = FindObjectsOfType<XitaVeia>();
        MermaidMan[] enemieMerMan = FindObjectsOfType<MermaidMan>();
        EnemyZombie[] enemieZombie = FindObjectsOfType<EnemyZombie>();

        foreach (EnemyBat bat in enemieBat) {
            bat.enabled = false;
        }

        foreach (XitaVeia xita in enemieXita) {
            xita.enabled = false;
        }

        foreach (MermaidMan merMan in enemieMerMan) {
            merMan.enabled = false;
        }

        foreach (EnemyZombie zombie in enemieZombie) {
            zombie.enabled = false;
        }
        
    }

    public void UnFreezeEnemies() {
        EnemyBat[] enemieBat = FindObjectsOfType<EnemyBat>();
        XitaVeia[] enemieXita = FindObjectsOfType<XitaVeia>();
        MermaidMan[] enemieMerMan = FindObjectsOfType<MermaidMan>();
        EnemyZombie[] enemieZombie = FindObjectsOfType<EnemyZombie>();

        foreach (EnemyBat bat in enemieBat) {
            bat.enabled = true;
        }

        foreach (XitaVeia xita in enemieXita) {
            xita.enabled = true;
        }

        foreach (MermaidMan merMan in enemieMerMan) {
            merMan.enabled = true;
        }

        foreach (EnemyZombie zombie in enemieZombie) {
            zombie.enabled = true;
        }
    }
}
