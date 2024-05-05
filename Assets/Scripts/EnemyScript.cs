using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;
    Animator animator;

    PlayerMovement playerMovement;
    PlayerHealth playerHealth;

    bool enemyCanAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;

        if(agent.velocity.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isWalking", false);
        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 1.8f))
        {
            print(hit.collider.name);
            if(hit.collider.gameObject.tag == "Player" && enemyCanAttack == true && playerHealth.isPlayerImmune == false)
            {
                animator.SetBool("isAttacking", true);
                playerMovement.movementDisabled = true;
                enemyCanAttack = false;
                StartCoroutine(AttackCooldown());

                playerHealth.MakePlayerImmune();
            }
        }
    }


    public IEnumerator AttackCooldown()
    {
        playerMovement.DisableAllControls(true);
        yield return new WaitForSeconds(2.5f);
        animator.SetBool("isAttacking", false);
        playerMovement.DisableAllControls(false);
        yield return new WaitForSeconds(3f);
        enemyCanAttack = true;
    }
}
