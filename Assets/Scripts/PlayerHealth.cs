using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    
    public float playerHealth = 100f;
    public float playerImmunityTime = 3.5f;
    public bool isPlayerImmune = false;

    bool isPlayerDead = false;

    public Slider healthSlider;

    Animator animator;

    GameObject[] enemies;

    private void Start()
    {
        animator = GetComponent<Animator>();

        healthSlider.value = playerHealth;
    }

    public void DamagePlayer(float damage)
    {
        if(!isPlayerDead)
        {
            playerHealth -= damage;

            if (playerHealth <= 0)
            {
                animator.SetBool("isTerrified", false);
                animator.SetBool("isDying", true);

                enemies = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (GameObject i in enemies)
                {
                    EnemyScript enemyScript = i.GetComponent<EnemyScript>();
                    Animator animator2 = i.GetComponent<Animator>();

                    animator2.SetBool("isAttacking", false);
                    enemyScript.enemyCanAttack = false;
                }

                AudioSource[] sounds = this.gameObject.GetComponents<AudioSource>();
                foreach (AudioSource sound in sounds)
                {
                    sound.Stop();
                }
                sounds[2].Play();

                GameObject[] hitObjs = GameObject.FindGameObjectsWithTag("MiniGamePrefab");
                foreach(GameObject i in hitObjs)
                {
                    Destroy(i);
                }

                isPlayerDead = true;

                StartCoroutine(PlayerDeathScreen());
            }

            healthSlider.value = playerHealth;
        }
    }

    public void MakePlayerImmune(bool immune)
    {
        if (immune == true)
        {
            isPlayerImmune = true;
        }
        else if (immune == false)
        {
            isPlayerImmune = false;
        }
    }

    public IEnumerator PlayerDeathScreen()
    {
        yield return new WaitForSeconds(3f);
        DeathPanel deathPanel = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<DeathPanel>();

        deathPanel.OpenDeathPanel();
    }
}
