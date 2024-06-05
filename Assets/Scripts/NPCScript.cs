using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCScript : MonoBehaviour
{
    [Header("EDIT THIS")]
    public string NPC_NAME;
    public List<string> DESCRIPTIONS = new List<string>();
    public float npcSoundDistance = 25f;

    [Header("NPC MODAL")]
    public TMP_Text NPC_HEADER;
    public TMP_Text NPC_DESCRIPTION;
    public TMP_Text COUNT_TXT;
    public GameObject NPC_MODAL;

    [Header("NPC TEXT OBJ")]
    public GameObject interactTxt;

    GameObject Player;
    public GameObject playerUI;
    bool isDialogOpen = false;
    int currentDesc = 0;

    bool coroutineHasStarted = false;
    bool hasTalkedWithNPC = false;

    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PlayerModel");
        playerMovement = Player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.gameObject.transform.position, Player.transform.position) <= 3)
        {
            interactTxt.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartNPCDialog();
                hasTalkedWithNPC = true;
                isDialogOpen = true;
            }
        } else
        {
            interactTxt.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isDialogOpen)
        {
            NPC_MODAL.SetActive(false);
            playerUI.SetActive(true);
            playerMovement.DisableAllControls(false);
        }

        COUNT_TXT.text = $"{currentDesc+1}/{DESCRIPTIONS.Count}";

        if(Vector3.Distance(this.gameObject.transform.position, Player.transform.position) < npcSoundDistance && !coroutineHasStarted)
        {
            StartCoroutine(PlaySounds());
            coroutineHasStarted = true;
        }

        // Make sure player Y doesn't change because of bugs
        if (this.gameObject.transform.position.y < -0.1 || this.gameObject.transform.position.y > -0.1)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, -0.1f, this.gameObject.transform.position.z);
        }
    }

    void StartNPCDialog()
    {
        NPC_MODAL.SetActive(true);
        NPC_HEADER.text = NPC_NAME;
        NPC_DESCRIPTION.text = DESCRIPTIONS[currentDesc];

        playerMovement.DisableAllControls(true);
        playerUI.SetActive(false);
    }

    public void NextBtnClick()
    {
        if(currentDesc < DESCRIPTIONS.Count-1)
        {
            currentDesc++;
            NPC_DESCRIPTION.text = DESCRIPTIONS[currentDesc];
        }
        playerMovement.DisableAllControls(true);
    }
    public void BackBtnClick()
    {
        if (currentDesc > 0 && currentDesc < DESCRIPTIONS.Count)
        {
            currentDesc--;
            NPC_DESCRIPTION.text = DESCRIPTIONS[currentDesc];
        }
        playerMovement.DisableAllControls(true);
    }

    public void ExitBtnClick()
    {
        NPC_MODAL.SetActive(false);
        playerMovement.DisableAllControls(false);
        playerUI.SetActive(true);
    }

    public IEnumerator PlaySounds()
    {
        AudioSource[] sounds = this.gameObject.GetComponents<AudioSource>();
        while (!hasTalkedWithNPC && Vector3.Distance(this.gameObject.transform.position, Player.transform.position) <= npcSoundDistance)
        {
            yield return new WaitForSeconds(Random.Range(4f, 5f));
            if(!hasTalkedWithNPC)
            {
                sounds[Random.Range(0, sounds.Length)].Play();
            }
        }
    }
}
