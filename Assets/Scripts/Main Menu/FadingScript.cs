using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingScript : MonoBehaviour
{

    PlayerMovement playerMovement;
    PlayerAttack playerAttack;

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

        playerMovement.DisableAllControls(true);
        playerAttack.canAttack = false;
    }

    public void OnLoadFinish()
    {
        MainMenuScript mainMenuScript = GameObject.Find("GameManager").GetComponent<MainMenuScript>();

        mainMenuScript.OnLoadInFinish();

        this.gameObject.GetComponent<Animator>().Play("LoadingAnimOut");
    }

    public void OnLoadOutFinish()
    {
        playerMovement.DisableAllControls(false);
        playerAttack.canAttack = true;
        Destroy(this.gameObject);
    }
}
