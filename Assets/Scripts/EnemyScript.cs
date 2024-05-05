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
    EscapeMinigame escapeMinigame;

    public bool enemyCanAttack = true;
    bool isEnemyAttacking = false;

    GameObject virtualCamera;

    public float attackCooldown = 2f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        escapeMinigame = GameObject.Find("GameManager").GetComponent<EscapeMinigame>();

        virtualCamera = GameObject.Find("Virtual Camera");
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
            if(hit.collider.gameObject.tag == "Player" && enemyCanAttack == true && playerHealth.isPlayerImmune == false)
            {
                animator.SetBool("isAttacking", true);
                playerMovement.DisableAllControls(true);
                enemyCanAttack = false;
                isEnemyAttacking = true;

                StartCoroutine(DamagePlayer());

                playerHealth.MakePlayerImmune(true);

                escapeMinigame.StartMinigame();

                virtualCamera.GetComponent<Animator>().Play("CameraZoom");
            }
        }
    }

    public void StopAttack()
    {
        playerMovement.DisableAllControls(false);
        virtualCamera.GetComponent<Animator>().Play("CameraZoomOut");
        animator.SetBool("isAttacking", false);
        StartCoroutine(AttackCooldown());
        isEnemyAttacking = false;
    }


    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        playerHealth.MakePlayerImmune(false);
        enemyCanAttack = true;
    }

    public IEnumerator DamagePlayer()
    {
        while(isEnemyAttacking)
        {
            yield return new WaitForSeconds(1f);
            playerHealth.DamagePlayer(5f);
        }
    }
}
