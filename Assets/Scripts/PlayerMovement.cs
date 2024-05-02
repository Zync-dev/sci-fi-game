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

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            // check if shift key is being held down
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                // set the speed to the running speed
                speed = runSpeed;
                animator.Play("mixamo_com");
            }
            else
            {
                // set the speed to the walking speed
                speed = normalSpeed;
                animator.Play("Walking");
            }

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}