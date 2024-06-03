using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.GetComponent<Animator>().Play("PanelOut");
        }
    }
    public void CloseBtn()
    {
        this.gameObject.GetComponent<Animator>().Play("PanelOut");
    }

    public void PanelAnimDone()
    {
        this.gameObject.SetActive(false);
    }
}
