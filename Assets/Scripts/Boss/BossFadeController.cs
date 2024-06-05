using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFadeController : MonoBehaviour
{

    BossScript bossScript;

    // Start is called before the first frame update
    void Start()
    {
        bossScript = GameObject.Find("GameManager").GetComponent<BossScript>();
    }

    void AfterFadeIn()
    {
        bossScript.AfterFadeIn();
        bossScript.Fade(1);
    }

    void AfterFadeOut()
    {
        bossScript.StartQuiz();

        this.gameObject.SetActive(false);
    }
}
