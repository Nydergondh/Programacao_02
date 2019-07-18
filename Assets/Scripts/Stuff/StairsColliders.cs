using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsColliders : MonoBehaviour
{
    public Stairs stair { get; private set; }
    private BoxCollider2D boxColldier;
    public bool botton;
    public LayerMask simonLayer;

    // Start is called before the first frame update
    void Start()
    {
        boxColldier = GetComponent<BoxCollider2D>();
        stair = GetComponentInParent<Stairs>();
    }

    private void OnTriggerStay(Collider other) {
        if (boxColldier.IsTouchingLayers(simonLayer) && Input.GetKey(KeyCode.UpArrow) && botton) {
            print("GotHere");
            stair.currentStair = 0;
            SimonActions.simon.stair = stair;
            SimonActions.simon.canUseStairs = true;
            SimonActions.simon.rigidbody.gravityScale = 0;
        }
        else if(boxColldier.IsTouchingLayers(simonLayer) && Input.GetKey(KeyCode.DownArrow) && !botton) {
            print("GotHere1");
            stair.currentStair = stair.stairs.Count;
            SimonActions.simon.stair = stair;
            SimonActions.simon.canUseStairs = true;
            SimonActions.simon.rigidbody.gravityScale = 0;
        }
    }
}
