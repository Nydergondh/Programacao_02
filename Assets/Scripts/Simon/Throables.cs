using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throables : MonoBehaviour {

    public GameObject[] throwables;
    public int currentId;
    private Animator animTrhowable;
    private int id;
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start() {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
        for (int i = 0; i < throwables.Length; i++) {
            id = i;
        }
    }

    // Update is called once per frame
    void Update() {
        if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Input.GetKeyDown(KeyCode.Z)){
            renderer.enabled = true;
            ThrowItem();
        }
    }
    void ThrowItem() {
        switch (currentId){
            //add all control of animations
            case 1:
                animTrhowable = throwables[currentId].GetComponent<Animator>();
                break;
            case 2:
                animTrhowable = throwables[currentId].GetComponent<Animator>();
                break;
            case 3:
                animTrhowable = throwables[currentId].GetComponent<Animator>();
                break;

        }
    }

}
