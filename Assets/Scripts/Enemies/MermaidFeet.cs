using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermaidFeet : MonoBehaviour
{
    private MermaidMan mermaid;
    private BoxCollider2D boxCollider;

    void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        mermaid = GetComponentInParent<MermaidMan>();
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.layer == 8) {
            mermaid.collider.enabled = true;
            mermaid.walk = true;
            mermaid.jump = false;
            Destroy(gameObject);
        }
    }
}
