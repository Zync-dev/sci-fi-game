using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ESC_UI : MonoBehaviour
{
    Animator animator;

    bool isMenuOpen = false;

    PlayerAttack playerAttack;
    PlayerMovement playerMovement;

    public GameObject ESC_UI_obj;

    private void Start()
    {
        animator = ESC_UI_obj.GetComponent<Animator>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent< PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isMenuOpen)
        {
            OpenMenu(true);
        } else if(Input.GetKeyDown(KeyCode.Escape) && isMenuOpen)
        {
            OpenMenu(false);
        }
    }

    public void OpenMenu(bool shouldOpen)
    {
        if(shouldOpen)
        {
            Time.timeScale = 0f;

            ESC_UI_obj.SetActive(true);
            animator.Play("ShowAnim");
            isMenuOpen = true;

            playerMovement.DisableAllControls(true);
            playerAttack.canAttack = false;
        } else
        {
            Time.timeScale = 1f;

            animator.Play("HideAnim");
            isMenuOpen = false;

            playerMovement.DisableAllControls(false);
            playerAttack.canAttack = true;
        }
    }

    public void TryAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
