using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XitaVeia : MonoBehaviour
{
    int xitaSpeed;
    private bool jump;
    private bool isOnPlatform;
    public LayerMask groundLayer;
    public LayerMask simonLayer;
    private new Rigidbody2D rigidbody;
    private BoxCollider2D xitaCollider;
    // Start is called before the first frame update
    void Start()
    {
        isOnPlatform = true;
        jump = false;
        rigidbody = GetComponent<Rigidbody2D>();
        xitaCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, SimonActions.simon.transform.position) <= 3f)
        {
            float xMovement = xitaSpeed * Time.deltaTime;
            if (transform.localScale.x == 1)
            {
                xMovement *= -1;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {

        if (collider.gameObject.layer == groundLayer)
        {
            jump = true;
            rigidbody.velocity = new Vector2(5f,5f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.layer == groundLayer && !isOnPlatform)
        {   
            rigidbody.velocity = new Vector2(5f, 5f);
        }

    }



}
