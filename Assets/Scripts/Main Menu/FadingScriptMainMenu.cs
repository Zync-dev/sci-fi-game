using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingScriptMainMenu : MonoBehaviour
{

    public void OnLoadFinish()
    {
        MainMenuScript mainMenuScript = GameObject.Find("GameManager").GetComponent<MainMenuScript>();

        mainMenuScript.OnLoadInFinish();

        this.gameObject.GetComponent<Animator>().Play("LoadingAnimOut");
    }

    public void OnLoadOutFinish()
    {
        Destroy(this.gameObject);
    }
}
