using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{

    private new BoxCollider2D collider;
    public LayerMask simomLayer;
    private Animator passageAnim;

    // Start is called before the first frame update
    void Start() {
        passageAnim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    public void OnChangeScene() {
        passageAnim.SetBool("Close", true);        
    }

}
