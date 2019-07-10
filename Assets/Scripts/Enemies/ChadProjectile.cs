using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChadProjectile : MonoBehaviour
{
    public float speed = 3f;
    BoxCollider2D boxCollider2D;
    private Transform parentOrientation;    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        parentOrientation = GetComponentInParent<Transform>();
        if (parentOrientation.localScale.x == 1)
        {
            speed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
        OutOffScreen();
    }

    public void OutOffScreen()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (!renderer.isVisible)
        {
            Destroy(gameObject);
        }
    }
}
