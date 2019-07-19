using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private new BoxCollider2D collider;
    public LayerMask simomLayer;
    private Animator doorAnim;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collider.IsTouchingLayers(simomLayer) && GameManager.gameManager.currentScenario.isChanging) {
            print("GotHere");
            doorAnim.SetBool("Open", true);
            doorAnim.SetBool("Idle", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        print("GotHere2");
        if (GameManager.gameManager.currentScenario.isChanging) {
            doorAnim.SetBool("Open", false);
            doorAnim.SetBool("Idle", false);
        }
    }

    public void SetDoorIdle() {
        doorAnim.SetBool("Idle", true);
        doorAnim.SetBool("Open", false);
        StartCoroutine(IdleStateStart());
    }

    IEnumerator IdleStateStart() {
        
        yield return new WaitForSeconds(0.518f);
        print("Gothere");
        SetDoorIdle();
    }
}
