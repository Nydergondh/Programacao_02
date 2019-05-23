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
        print(horzMove);

        if (horzMove == 0) { 
            animator.SetBool("IsMoving", false);
            print("Not Moving");
        }
        else {
            //TODO set walking speed to be slower then the running speed. (At the moment only changing the animation)
            animator.SetBool("IsMoving", true);            
            //start run animation
            if (Input.GetButton("Run")) {
                animator.SetBool("IsRunning", true);
                print("Moving and Running");
            }
            else {
                animator.SetBool("IsRunning", false);
                print("Moving and not Running");
            }
            Vector3 horizontal = new Vector3(horzMove * speed, transform.position.y, transform.position.z) * Time.deltaTime;
            transform.Translate(horizontal);
            CheckDiraction(horzMove);
        }

    }

    public void CheckDiraction(float movement) {
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

}
