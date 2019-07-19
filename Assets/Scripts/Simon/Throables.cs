using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throables : MonoBehaviour {

    public GameObject[] throwables;
    public int currentId;
    private int id;


    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < throwables.Length; i++) {
            id = i;
        }
    }

    // Update is called once per frame
    void Update() {
        if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Input.GetKeyDown(KeyCode.Z) 
            && GameManager.gameManager.canThrowIten && UI_Manager.ui_Manager.hearts > 0) {
            ThrowItem();
        }
    }
    void ThrowItem() {
        switch (currentId) {
            //add all control of animations
            case -1:
                break;
            case 0:
                GameObject knife = Instantiate(throwables[currentId], transform.position, Quaternion.identity);
                break;
            case 1:
                GameObject holyWater = Instantiate(throwables[currentId], transform.position, Quaternion.identity);
                break;
            case 2:
                GameObject cross = Instantiate(throwables[currentId], transform.position, Quaternion.identity);
                break;
            case 3:
                GameObject axe = Instantiate(throwables[currentId], transform.position, Quaternion.identity);
                break;
        }       
    }

}
