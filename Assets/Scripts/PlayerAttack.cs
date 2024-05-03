using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerMovement playerMovement;
    Animator animator;

    public float attackTime = 3f;
    public float attackCooldown = 1f;

    public GameObject StopModel;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    bool runPressed = Input.GetKey(KeyCode.LeftShift);
    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0) && !runPressed)
        {
            Attack();
        }
    }

    public void Attack()
    {
        playerMovement.movementDisabled = true;
        animator.SetBool("isAttacking", true);
        StartCoroutine(AttackCooldown());
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(2f);

        GameObject stopInstance = Instantiate(StopModel, this.gameObject.transform);
        stopInstance.transform.position = this.gameObject.transform.position;

        Rigidbody rb = stopInstance.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(10f * Time.deltaTime, 0f, 0f));

        yield return new WaitForSeconds(1f);

        playerMovement.movementDisabled = false;
        animator.SetBool("isAttacking", false);

        yield return new WaitForSeconds(attackCooldown);

        // can attack again
    }
}
