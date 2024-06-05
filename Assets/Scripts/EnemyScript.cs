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

    public float enemySoundDistance = 10;
    bool hasStartedSoundCoroutine = false;

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

        StartCoroutine(PlaySounds());
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth > 0 && Vector3.Distance(this.gameObject.transform.position, player.transform.position) <= 15)
        {
            agent.destination = player.position;
        } else
        {
            agent.ResetPath();
        }

        if(agent.velocity.magnitude > 0 && !isEnemyDead)
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

                StartCoroutine(PlayPlayerSounds());

                AudioSource[] sounds = this.gameObject.GetComponents<AudioSource>();
                foreach(AudioSource sound in sounds)
                {
                    sound.Stop();
                }
                sounds[0].Play();

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
            enemyCanAttack = false;

            CapsuleCollider[] collider = GetComponents<CapsuleCollider>();
            foreach (CapsuleCollider collider2 in collider) { collider2.enabled = false; }

            isEnemyDead = true;
            animator.Play("Dying");
        }

        if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) <= enemySoundDistance && !hasStartedSoundCoroutine)
        {
            StartCoroutine(PlaySounds());
            hasStartedSoundCoroutine = true;
        }

        // Make sure player Y doesn't change because of bugs
        if (this.gameObject.transform.position.y < -0.83 || this.gameObject.transform.position.y > -0.83)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, -0.83f, this.gameObject.transform.position.z);
        }
    }

    public void StopAttack()
    {
        playerMovement.DisableAllControls(false);
        playerMovement.animator.SetBool("isTerrified", false);
        virtualCamera.GetComponent<Animator>().Play("CameraZoomOut");
        animator.SetBool("isAttacking", false);
        StartCoroutine(AttackCooldown());
        StopCoroutine(PlayPlayerSounds());
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

    public IEnumerator PlaySounds()
    {
        AudioSource[] sounds = this.gameObject.GetComponents<AudioSource>();
        while(!isEnemyAttacking && !isEnemyDead && Vector3.Distance(this.transform.position, player.transform.position) <= enemySoundDistance)
        {
            yield return new WaitForSeconds(Random.Range(3f,6f));
            if(!isEnemyAttacking && !isEnemyDead)
            {
                sounds[Random.Range(1, sounds.Length)].Play();
            }
            hasStartedSoundCoroutine = false;
        }
    }

    public IEnumerator PlayPlayerSounds()
    {
        AudioSource[] sounds = player.gameObject.GetComponents<AudioSource>();
        while (playerMovement.animator.GetBool("isTerrified") == true)
        {
            yield return new WaitForSeconds(Random.Range(2.5f, 4f));
            if(playerMovement.animator.GetBool("isTerrified") == true)
            {
                sounds[Random.Range(3, sounds.Length)].Play();
            }
        }
    }
}
