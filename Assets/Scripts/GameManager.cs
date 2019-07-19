using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public Scenario currentScenario;
    public Scenario[] scenarios;

    private AudioSource audioSource;
    public AudioClip gameOver;
    public AudioClip winGame;
    public AudioClip CastelvaniaTheme;
    public AudioClip BossTheme;

    public BigAssBat boss;
    public Transform passage_00;
    public Transform passage_01;


    public bool canThrowIten = true;
    public int i = 0;

    // Start is called before the first frame update
    void Awake() {

        if (gameManager == null) {
            gameManager = this;
        }
        else {
            Destroy(gameManager);
            gameManager = this;
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = CastelvaniaTheme;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SimonActions.simon.simonAnim.GetBool("Alive")) {
            StartCoroutine(SimonActions.simon.Die());
            StartCoroutine(WaitEndGame());
        }
    }

    public void ChangeScene() {
        i += 1;;
        currentScenario = scenarios[i];
    }


    private IEnumerator WaitEndGame() {
        if (SimonActions.simon.simonAnim.GetBool("Alive")) {
            audioSource.clip = winGame;
            audioSource.Play();
            audioSource.loop = false;
        }
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Level");
    }
    public IEnumerator WaitForTransition() {
        //case boss room
        DestroyEnemys();
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
            if (i == 3) {
                print("GotHere1");
                TeleportSimon();
            }
            yield return new WaitForSeconds(3f);

            SimonActions.simon.rigidbody.isKinematic = false;
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

    public void DestroyEnemys() {
        EnemyBat[] enemieBat = FindObjectsOfType<EnemyBat>();
        XitaVeia[] enemieXita = FindObjectsOfType<XitaVeia>();
        MermaidMan[] enemieMerMan = FindObjectsOfType<MermaidMan>();
        EnemyZombie[] enemieZombie = FindObjectsOfType<EnemyZombie>();

        foreach (EnemyBat bat in enemieBat) {
            Destroy(bat.gameObject);
        }
        if (i == 1) {
            foreach (XitaVeia xita in enemieXita) {
                SpriteRenderer spriteRenderer = xita.gameObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer.isVisible) {
                    Destroy(xita.gameObject);
                }
            }
        }

        foreach (MermaidMan merMan in enemieMerMan) {
            Destroy(merMan.gameObject);
        }

        foreach (EnemyZombie zombie in enemieZombie) {
            Destroy(zombie.gameObject);
        }
    }

    public IEnumerator ClosePassage() {
        yield return new WaitForSeconds(0.0f);
        if (i == 2) {
            Passage pass = passage_00.GetComponent<Passage>();
            pass.OnChangeScene();
        }
        else if (i == 3) {
            Passage pass = passage_01.GetComponent<Passage>();
            pass.OnChangeScene();
        }
    }

    public IEnumerator SpawnBoss() {
        yield return new WaitForSeconds(4f);
        audioSource.loop = true;
        audioSource.clip = BossTheme;
        audioSource.Play();
        boss.enabled = true;
    }

    public IEnumerator EndGame() {

        yield return new WaitForSeconds(3f);
        SimonActions.simon.enabled = false;
        StartCoroutine(WaitEndGame());
        //tocar musica se possivel
    }

    public IEnumerator Quit() {
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }

    public void TeleportSimon() {
        print("GotHere");
        SimonActions.simon.rigidbody.velocity = Vector2.zero;
        SimonActions.simon.rigidbody.isKinematic = true;
        SimonActions.simon.transform.position = currentScenario.target.position;
    }
}
