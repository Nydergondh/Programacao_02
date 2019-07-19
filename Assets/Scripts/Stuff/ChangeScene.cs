using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(WaitForIntro());
    }
    IEnumerator WaitForIntro() {
        yield return new WaitForSeconds(6.2f);
        //Todo: Change this
        SceneManager.LoadScene("Level");
    }
}
