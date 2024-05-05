using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeMinigame : MonoBehaviour
{
    public GameObject buttonPrefab;
    private Canvas canvas;

    int spawnAmount;
    int spawnedAmout = 0;

    GameObject[] enemies;

    void Start()
    {
        spawnAmount = Random.Range(5, 10);

        canvas = FindObjectOfType<Canvas>();
    }

    public void StartMinigame()
    {
        SpawnButton();
    }

    void SpawnButton()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(0, canvas.pixelRect.width),
            Random.Range(0, canvas.pixelRect.height)
        );

        GameObject buttonInstance = Instantiate(buttonPrefab, randomPos, Quaternion.identity);
        buttonInstance.transform.SetParent(canvas.transform);

        spawnedAmout++;

        Button button = buttonInstance.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => Destroy(buttonInstance));
            button.onClick.AddListener(BtnClick);
        }
    }

    private void BtnClick()
    {
        if (spawnedAmout >= spawnAmount)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject i in enemies)
            {
                EnemyScript enemyScript = i.GetComponent<EnemyScript>();
                enemyScript.StopAttack();
            }

            spawnAmount = Random.Range(5, 10);
            spawnedAmout = 0;
        } else
        {
            SpawnButton();
        }
    }
}