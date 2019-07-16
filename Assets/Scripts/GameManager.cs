using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public Scenario currentScenario;
    public Scenario[] scenarios;
    public bool canThrowIten = true;
    private int i = 0;

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
        SceneManager.LoadScene("Simon_test");
    }

    public void ChangeScene() {
        i += 1;
        currentScenario = scenarios[i];
    }

}
