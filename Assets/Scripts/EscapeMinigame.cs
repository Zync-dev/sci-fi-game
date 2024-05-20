using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeMinigame : MonoBehaviour
{
    public GameObject buttonPrefab;
    private Canvas canvas;

    public int spawnAmount;
    public int spawnedAmout = 0;

    string type;

    GameObject[] enemies;

    void Start()
    {
        spawnAmount = Random.Range(5, 10);

        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
    }

    public void StartMinigame(string type2)
    {
        spawnAmount = Random.Range(5, 10);
        spawnedAmout = 0;

        type = type2;
        SpawnButton();
    }

    // ENEMY MINIGAME
    void SpawnButton()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(100, canvas.pixelRect.width-100),
            Random.Range(100, canvas.pixelRect.height-100)
        );

        GameObject buttonInstance = Instantiate(buttonPrefab, randomPos, Quaternion.identity);
        buttonInstance.transform.SetParent(canvas.transform);

        Button button = buttonInstance.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => button.gameObject.GetComponent<Animator>().SetBool("shouldExit", true));
            button.onClick.AddListener(() => spawnedAmout++);
            button.onClick.AddListener(BtnClick);
            button.onClick.AddListener(() => button.interactable = false);
        }
    }

    private void BtnClick()
    {
        if(type == "Enemy")
        {
            if (spawnedAmout >= spawnAmount)
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (GameObject i in enemies)
                {
                    EnemyScript enemyScript = i.GetComponent<EnemyScript>();
                    enemyScript.StopAttack();
                }
            }
            else
            {
                SpawnButton();
            }
        } else if(type == "Boss")
        {
            if (spawnedAmout >= spawnAmount)
            {
            }
            else
            {
                SpawnButton();
            }
        }
    }
}