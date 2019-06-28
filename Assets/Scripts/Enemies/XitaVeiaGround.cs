using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XitaVeiaGround : MonoBehaviour
{
    private XitaVeia xitaVeia;
    private BoxCollider2D boxCollider;

    void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        xitaVeia = GetComponentInParent<XitaVeia>();
    }

    private void OnTriggerExit2D(Collider2D collider) {

        if (collider.gameObject.layer == 8) {
            xitaVeia.jump = true;
            xitaVeia.isOnPlatform = false;
            if (transform.parent.localScale.x == -1) {
                xitaVeia.rigidbody.velocity = new Vector2(3f, 3f);
            }
            else{
                xitaVeia.rigidbody.velocity = new Vector2(-3f, 3f);
            }

            xitaVeia.xitaAnim.SetBool("Run", false);
            xitaVeia.xitaAnim.SetBool("Jump", true);

            Destroy(gameObject);
        }

    }
}
