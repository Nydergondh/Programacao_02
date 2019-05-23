using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFox : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horzMove = Input.GetAxis("Horizontal");
        //is is standing still
        if (horzMove == 0) { 
            animator.SetBool("IsMoving", false);
        }
        else {
            //if is moving and running
            if (Input.GetButton("Run")) {
                //check for jumps, otherwise run
                if (Input.GetButtonDown("Jump") && animator.GetBool("IsJumping") == false) {
                    animator.SetBool("IsJumping", true);
                    StartCoroutine(JumpWait());
                }
                //check for jumps, otherwise run
                else if (animator.GetBool("IsJumping") == false) {
                    animator.SetBool("IsRunning", true);
                }
                speed = 10f;
            }
            //is no running, so walk
            else {
                //check for jumps, otherwise walk
                if (Input.GetButtonDown("Jump") && animator.GetBool("IsJumping") == false) {
                    animator.SetBool("IsJumping", true);
                    StartCoroutine(JumpWait());
                }
                //walk
                else if(animator.GetBool("IsJumping") == false) {
                    animator.SetBool("IsRunning", false);
                    animator.SetBool("IsMoving", true);
                }
                speed = 5f;
            }
            //after the correct animation is playing with the correct velocity, move the object
            Vector3 horizontal = new Vector3(horzMove * speed, 0, 0) * Time.deltaTime;
            transform.Translate(horizontal);
            //Check if the caracter is changing direction (flips de X renderer).
            CheckDirection(horzMove);
        }

    }

    public void CheckDirection(float movement) {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        //if was left but now is going rigth
        if (movement <= 1 && movement > 0) {
            renderer.flipX = true;
        }
        //if was rigth but now is going left
        else if (movement >= -1 && movement < 0) {
            renderer.flipX = false;
        }
    }

    IEnumerator JumpWait() {
        yield return new WaitForSeconds(0.7f);
        animator.SetBool("IsJumping", false);
    }

}
