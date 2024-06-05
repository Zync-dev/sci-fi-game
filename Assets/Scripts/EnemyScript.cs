using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    Transform player;
    NavMeshAgent agent;
    Animator animator;

    PlayerMovement playerMovement;
    PlayerHealth playerHealth;
    EscapeMinigame escapeMinigame;

    public bool enemyCanAttack = true;
    bool isEnemyAttacking = false;
    public bool isEnemyDead = false;

    GameObject virtualCamera;

    public float attackCooldown = 2f;

    public float enemyHealth = 1f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        escapeMinigame = GameObject.Find("GameManager").GetComponent<EscapeMinigame>();

        virtualCamera = GameObject.Find("Virtual Camera");

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth > 0 && (this.transform.position.x - player.transform.position.x) <= 15)
        {
            agent.destination = player.position;
        } else
        {
            agent.ResetPath();
        }

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
            if(hit.collider.gameObject.tag == "Player" && enemyCanAttack == true && playerHealth.isPlayerImmune == false && enemyHealth > 0 && !isEnemyDead)
            {
                GameObject[] activeProjectiles;
                activeProjectiles = GameObject.FindGameObjectsWithTag("PlayerProjectile");
                playerMovement.animator.SetBool("isAttacking", false);

                foreach(GameObject projectile in activeProjectiles) 
                {
                    Destroy(projectile);
                }

                animator.SetBool("isAttacking", true);
                playerMovement.DisableAllControls(true);
                playerMovement.animator.SetBool("isTerrified", true);
                enemyCanAttack = false;
                isEnemyAttacking = true;

                this.gameObject.GetComponent<AudioSource>().Play();

                StartCoroutine(DamagePlayer());

                playerHealth.MakePlayerImmune(true);

                escapeMinigame.StartMinigame("Enemy");

                virtualCamera.GetComponent<Animator>().Play("CameraZoom");
            }
        }

        if(enemyHealth <= 0f && !isEnemyDead)
        {
            playerMovement.DisableAllControls(false);
            playerMovement.animator.SetBool("isTerrified", false);
            animator.SetBool("isAttacking", false);
            StartCoroutine(AttackCooldown());
            isEnemyAttacking = false;

            animator.SetBool("isWalking", false);
            animator.SetBool("isDying", true);
            enemyCanAttack = false;

            CapsuleCollider[] collider = GetComponents<CapsuleCollider>();
            foreach (CapsuleCollider collider2 in collider) { collider2.enabled = false; }

            isEnemyDead = true;
        }
    }

    public void StopAttack()
    {
        playerMovement.DisableAllControls(false);
        playerMovement.animator.SetBool("isTerrified", false);
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
