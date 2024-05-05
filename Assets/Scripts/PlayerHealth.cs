using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public float playerHealth = 100f;
    public float playerImmunityTime = 3.5f;
    public bool isPlayerImmune = false;

    public void DamagePlayer(float damage)
    {
        playerHealth -= damage;
        print(playerHealth);
    }

    public void MakePlayerImmune()
    {
        isPlayerImmune = true;

        StartCoroutine(PlayerImmunityTime());
    }

    public IEnumerator PlayerImmunityTime()
    {
        yield return new WaitForSeconds(playerImmunityTime);
        isPlayerImmune = false;
    }
}
