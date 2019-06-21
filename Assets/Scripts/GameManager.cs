using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public List<Transform> scenarioTransforms;
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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CurrentScenario(Scenario currentScenario) {
        foreach (Transform checkpoint in currentScenario.GetCheckPoints()) {
            scenarioTransforms.Add(checkpoint);
        } 
    }
    
}
