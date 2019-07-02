using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throables : MonoBehaviour {

    public GameObject[] throwables;
    public int currentId;
    private int id;
    public GameObject child { get; private set; }


    // Start is called before the first frame update
    void Start() {
        GameManager.gameManager.DestroyI = NullChild;
        for (int i = 0; i < throwables.Length; i++) {
            id = i;
        }
        child = null;
    }

    // Update is called once per frame
    void Update() {
        if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Input.GetKeyDown(KeyCode.Z) && child == null){
            ThrowItem();
        }
    }
    void ThrowItem() {
        switch (currentId){
            //add all control of animations
            case -1:
                break;
            case 0:
                GameObject knife = Instantiate(throwables[0], transform.position, Quaternion.identity);
                StartCoroutine(ReturnTime(knife));
                break;
            case 1:
                GameObject holyWater = Instantiate(throwables[1], transform.position, Quaternion.identity);
                StartCoroutine(ReturnTime(holyWater));
                break;
            case 2:
                GameObject cross = Instantiate(throwables[2], transform.position, Quaternion.identity);
                StartCoroutine(ReturnTime(cross));
                break;
            case 3:
                GameObject axe = Instantiate(throwables[3], transform.position, Quaternion.identity);
                StartCoroutine(ReturnTime(axe));
                break;
        }
    }
    void NullChild() {
        child = null;
    }

    IEnumerator ReturnTime(GameObject spawn) {
        yield return new WaitForSeconds(0.005f);
        child = spawn;
    }

}
