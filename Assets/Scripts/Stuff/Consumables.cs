using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumables : MonoBehaviour
{
    public int idConsumable;
    public LayerMask simonLayer;
    BoxCollider2D boxCollider;

    private void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        GameManager.gameManager.Consumed += GetId;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (boxCollider.IsTouchingLayers(simonLayer)) {
            Destroy(gameObject);
        }
    }

    public int GetId() {
        return idConsumable;
    }
}
