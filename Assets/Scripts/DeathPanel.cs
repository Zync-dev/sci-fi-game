using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanel : MonoBehaviour
{

    public GameObject deathPanel;

    public void OpenDeathPanel()
    {
        deathPanel.SetActive(true);
        this.GetComponent<Animator>().Play("ShowPanel");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void TryAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
