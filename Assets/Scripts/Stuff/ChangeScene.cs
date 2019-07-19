using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    // Update is called once per frame
    void Start()
    {
        if (SceneManager.GetSceneByName("Intro") == SceneManager.GetActiveScene()) {
            StartCoroutine(WaitForIntro());
        }
    }

    void Update() {
        if (SceneManager.GetSceneByName("Title_Screen") == SceneManager.GetActiveScene()) {
            if (Input.GetKey(KeyCode.Return)) {
                StartCoroutine(WaitForTitle());
            }
        }
    }

    IEnumerator WaitForIntro() {
        yield return new WaitForSeconds(6.2f);
        //Todo: Change this
        SceneManager.LoadScene("Level");
    }

    IEnumerator WaitForTitle() {
        yield return new WaitForSeconds(2f);
        //Todo: Change this
        SceneManager.LoadScene("Intro");
    }
}
