using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throables : MonoBehaviour {

    public GameObject[] throwables;
    public static int currentId;
    private Animator animTrhowable;
    private int id;

    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < throwables.Length; i++) {
            id = i;
        }
    }

    // Update is called once per frame
    void Update() {
        if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Input.GetKeyDown(KeyCode.Z)){
            ThrowItem();
        }
    }
    void ThrowItem() {
        //add all control of animations
        switch (currentId){
            //no item aquired
            case -1:
                break;
            //knife
            case 0:
                GameObject knife = Instantiate(throwables[0], transform.position, Quaternion.identity, transform);                
                break;
            //holy water
            case 1:
                GameObject holyWater = Instantiate(throwables[1], transform.position, Quaternion.identity, transform);
                break;
            //cross
            case 2:
                GameObject cross = Instantiate(throwables[2], transform.position, Quaternion.identity, transform);
                break;

        }
    }

}
