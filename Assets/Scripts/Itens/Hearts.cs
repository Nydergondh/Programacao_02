using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    public int points;
    public BoxCollider2D collider;
    public LayerMask simonLayer;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        DestroyHeart();
    }

    // Update is called once per frame
    IEnumerator DestroyHeart() {

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D enemy) {
        if (collider.IsTouchingLayers(simonLayer)) {
            //Add points to UI
        }
    }
}
