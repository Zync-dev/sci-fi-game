using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public float playerHealth = 100f;
    public float playerImmunityTime = 3.5f;
    public bool isPlayerImmune = false;

    Animator animator;

    GameObject[] enemies;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void DamagePlayer(float damage)
    {
        playerHealth -= damage;
        print(playerHealth);

        if(playerHealth <= 0 )
        {
            animator.SetBool("isTerrified", false);
            animator.SetBool("isDying", true);

            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach(GameObject i in enemies)
            {
                EnemyScript enemyScript = i.GetComponent<EnemyScript>();
                Animator animator2 = i.GetComponent<Animator>();

                animator2.SetBool("isAttacking", false);
                enemyScript.enemyCanAttack = false;
            }
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
}
