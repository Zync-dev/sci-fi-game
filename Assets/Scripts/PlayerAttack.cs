using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerMovement playerMovement;
    Animator animator;

    public float attackTime = 3f;
    public float attackCooldown = 1f;

    bool canAttack = true;

    public GameObject StopModel;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKey(KeyCode.LeftShift))
        {
            Attack();
        }
    }

    public void Attack()
    {
        canAttack = false;

        playerMovement.movementDisabled = true;
        animator.SetBool("isAttacking", true);
        StartCoroutine(AttackCooldown());
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(2f);

        GameObject stopInstance = Instantiate(StopModel);
        stopInstance.transform.position = this.gameObject.transform.position;
        stopInstance.transform.position += new Vector3(0f, 1f, 0f);
        stopInstance.transform.rotation = this.gameObject.transform.rotation;

        yield return new WaitForSeconds(1f);

        playerMovement.movementDisabled = false;
        animator.SetBool("isAttacking", false);

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}
