using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    float speed;
    public float normalSpeed = 2f; // normal walking speed
    public float runSpeed = 5f; // running speed
    public bool movementDisabled = false;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Animator animator;
    Rigidbody rb;

    PlayerAttack playerAttack;

    public Slider playerStamina;
    public float stamina = 100f;
    public float staminaUseRate = 0.1f;
    public float staminaRegenRate = 0.1f;

    bool isRunning;

    private void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        playerStamina.value = stamina;
    }

    void Update()
    {
        // Player movement and rotation
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f && !movementDisabled)
        {
            if (transform.position.z > -8.5f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                controller.Move(direction * speed * Time.deltaTime);
            } else
            {
                transform.position += new Vector3(0f,0f,0.01f);
            }
        }

        // Animation & speed handling

        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        if (horizontal != 0 || vertical != 0 && !movementDisabled)
        {
            if (!runPressed && !movementDisabled)
            {
                speed = normalSpeed;
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
            }
            else if (runPressed && !isRunning && isWalking && !movementDisabled && stamina > 20)
            {
                StopCoroutine(StaminaDepletion());
                speed = runSpeed;
                animator.SetBool("isRunning", true);
                StartCoroutine(StaminaDepletion());
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

        if (stamina <= 0)
        {
            StopCoroutine(StaminaDepletion());
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
            speed = normalSpeed;
            StartCoroutine(StaminaDepletion());
            stamina += 1;
        }
    }

    public IEnumerator StaminaDepletion()
    {
        while (animator.GetBool("isRunning") && stamina > 0)
        {
            yield return new WaitForSeconds(staminaUseRate);
            stamina--;
            playerStamina.value = stamina;
        }

        while (!animator.GetBool("isRunning") && stamina < 100f)
        {
            yield return new WaitForSeconds(staminaRegenRate);
            stamina++;
            playerStamina.value = stamina;
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
        }
        else if (disable == false)
        {
            movementDisabled = false;
            playerAttack.canAttack = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BOSS_START")
        {
            BossScript bossScript = GameObject.Find("GameManager").GetComponent<BossScript>();
            bossScript.StartBossFight();
        }
    }
}