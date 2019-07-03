using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public List<Transform> scenarioTransforms;
    public bool canThrowIten = true;

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
        scenarioTransforms = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SimonActions.simon.simonAnim.GetBool("Alive")) {
            StartCoroutine(SimonActions.simon.Die());
            StartCoroutine(WaitEndGame());
        }
    }
    
    public void CurrentScenario(Scenario currentScenario) {
        foreach (Transform checkpoint in currentScenario.GetCheckPoints()) {
            scenarioTransforms.Add(checkpoint);
        } 
    }

    private IEnumerator WaitEndGame() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Simon_test");
    }

}
