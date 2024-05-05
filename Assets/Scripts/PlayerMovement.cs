using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    float speed;
    public float normalSpeed = 2f; // normal walking speed
    public float runSpeed = 5f; // running speed
    public bool movementDisabled = false;
    public bool attackDisabled = false;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Animator animator;
    Rigidbody rb;

    PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Player movement and rotation
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f && !movementDisabled)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }

        // Animation & speed handling

        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        if (horizontal != 0 || vertical != 0 && !movementDisabled)
        {
            if (runPressed && !isRunning && isWalking && !movementDisabled)
            {
                speed = runSpeed;
                animator.SetBool("isRunning", true);
            }
            else if (!runPressed && !movementDisabled)
            {
                speed = normalSpeed;
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
            }
            else if (!isWalking && !movementDisabled)
            {
                speed = normalSpeed;
                animator.SetBool("isWalking", true);
            }
        }
        else if (isWalking)
        {
            animator.SetBool("isWalking", false);
        }
        else if (isRunning)
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void DisableAllControls(bool disable)
    {
        if(disable == true)
        {
            movementDisabled = true;
            playerAttack.canAttack = false;

            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isTerrified", true);
        } 
        else if (disable == false)
        {
            movementDisabled = false;
            playerAttack.canAttack = true;

            animator.SetBool("isTerrified", false);
        }


    }
}