using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{

    public Slider bossSlider;
    public GameObject questionPanel;
    public TMP_Text correctOrWrongTxt;

    public GameObject fadePanel;

    public GameObject playerStamina;
    public GameObject playerHealth;

    public GameObject lostWonScreen;
    public TMP_Text lostWonText;

    public GameObject virtualCamera;

    public TMP_Text questionTxt;
    public Button answer1;
    public TMP_Text answer1txt;
    public Button answer2;
    public TMP_Text answer2txt;
    public Button answer3;
    public TMP_Text answer3txt;
    public Button answer4;
    public TMP_Text answer4txt;

    public GameObject playerModel;
    public GameObject enemyModel;

    public string[,] questions = {
        { "Hvad er den mest effektive metode til at forhindre seksuelt overf�rte infektioner (STI'er) under samleje?", "Brug af tandtr�d", "Brug af kondom", "Brug af antibiotika", "Brug af vitamin C", "2" /* <---- CORRECT ANSWER IS 2*/},
        { "Hvilken type pr�ventionsmiddel er en spiral?", "En pille", "En injektion", "Et implantat", "Et intrauterint system (IUD)", "4" /* <---- CORRECT ANSWER IS 4*/},
        { "Hvilken af f�lgende seksuelt overf�rte infektioner kan forebygges med en vaccine?", "HIV", "Klamydia", "Gonor�", "HPV", "4" /* <---- CORRECT ANSWER IS 4*/},
        { "Hvad betyder begrebet \"seksuel samtykke\"?", "At v�re enig i at g� p� en date", "At give frivillig og entusiastisk tilladelse til seksuel aktivitet", "At k�be pr�ventionsmidler", "At deltage i seksual undervisning", "2" /* <---- CORRECT ANSWER IS 1*/},

        { "Hvad er oftest �rsag til u�nskede graviditeter?", "Fejlslagne pr�ventionsmidler", "Manglende brug af pr�vention", "Forkert brug af pr�vention", "Alle ovenst�ende", "4" /* <---- CORRECT ANSWER IS 4*/},
        { "Hvad er form�let med seksualundervisning i skolerne?", "At reducere antallet af seksuelle partnere", "At informere og uddanne unge om sikker sex, pr�vention og seksuelle rettigheder", "At �ge antallet af b�rn f�dt hvert �r", "At fremme �gteskab blandt unge", "2" /* <---- CORRECT ANSWER IS 2*/},
        { "Hvilket af f�lgende er en seksuelt overf�rt infektion, der kan behandles med antibiotika?", "HIV", "Klamydia", "HPV", "Herpes", "2" /* <---- CORRECT ANSWER IS 2*/},
        { "Hvilken aldersgruppe er mest p�virket af seksuelt overf�rte infektioner?", "B�rn under 10 �r", "Teenagere og unge voksne (15-24 �r)", "Voksne mellem 30-40 �r", "�ldre over 60 �r", "2" /* <---- CORRECT ANSWER IS 2*/},

    };

    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (bossSlider.value >= 100 || bossSlider.value <= 0)
        {
            if(bossSlider.value >= 100)
            {
                lostWonScreen.SetActive(true);
                lostWonScreen.GetComponent<Animator>().Play("LostWonUIShow");
                lostWonText.text = "DU HAR VUNDET OVER DE BER�RENDE ROBOTTER. VERDEN ER NU IGEN FRI!";
            } else
            {
                lostWonScreen.SetActive(true);
                lostWonScreen.GetComponent<Animator>().Play("LostWonUIShow");
                lostWonText.text = "DU HAR TABT OVER DE BER�RENDE ROBOTTER. VERDEN ER FORTSAT BESAT!";
            }
        }
    }

    public void StartBossFight()
    {
        playerMovement.DisableAllControls(true);
        playerHealth.SetActive(false);
        playerStamina.SetActive(false);
        Fade(0);
    }

    public void Fade(int fadeType)
    {
        if (fadeType == 0)
        {
            fadePanel.SetActive(true);
            fadePanel.GetComponent<Animator>().Play("FadeIn");
        }
        else if (fadeType == 1)
        {
            fadePanel.SetActive(true);
            fadePanel.GetComponent<Animator>().Play("FadeOut");
        }
        else
        {
            print("ERROR! (NOT CORRECT FADETYPE)");
        }
    }

    public void AfterFadeIn()
    {
        playerModel.transform.position = enemyModel.transform.position;
        playerModel.transform.position += new Vector3(-5f, 0f, 0f);
        virtualCamera.GetComponent<Animator>().Play("BossAnimation");

        bossSlider.gameObject.SetActive(true);
    }

    int chosenQuestion;
    public void StartQuiz()
    {
        ShowQuestionPanel(true);
        DisableButtons(true); //Makes buttons interactable (btn.interactable = true)

        questionPanel.GetComponent<Animator>().Play("New State");

        chosenQuestion = Random.Range(0, questions.Length / 6);

        print(chosenQuestion);

        questionTxt.text = questions[chosenQuestion, 0];

        answer1txt.text = questions[chosenQuestion, 1];
        answer2txt.text = questions[chosenQuestion, 2];
        answer3txt.text = questions[chosenQuestion, 3];
        answer4txt.text = questions[chosenQuestion, 4];

        // Efter Quiz Start Minigame!
        // Herefter start Quiz igen!
    }

    public void Answer1BtnClicked()
    {
        DisableButtons(false);

        if (int.Parse(questions[chosenQuestion, 5]) == 1)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
    public void Answer2BtnClicked()
    {
        DisableButtons(false);

        if (int.Parse(questions[chosenQuestion, 5]) == 2)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
    public void Answer3BtnClicked()
    {
        DisableButtons(false);

        if (int.Parse(questions[chosenQuestion, 5]) == 3)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
    public void Answer4BtnClicked()
    {
        DisableButtons(false);

        if (int.Parse(questions[chosenQuestion, 5]) == 4)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    void DisableButtons(bool disable)
    {
        answer1.interactable = disable;
        answer2.interactable = disable;
        answer3.interactable = disable;
        answer4.interactable = disable;
    }

    void CorrectAnswer()
    {
        correctOrWrongTxt.color = Color.green;
        correctOrWrongTxt.text = "CORRECT!";

        questionPanel.GetComponent<Animator>().Play("RevealCorrectWrong");

        ChangeSliderValue(20);

        StartCoroutine(StartMinigame(false));

        PlayerAttack playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        playerAttack.Attack();

        playerMovement.DisableAllControls(true);
    }
    void WrongAnswer()
    {
        correctOrWrongTxt.color = Color.red;
        correctOrWrongTxt.text = "WRONG!";

        questionPanel.GetComponent<Animator>().Play("RevealCorrectWrong");

        ChangeSliderValue(-20);

        StartCoroutine(StartMinigame(true));

        enemyModel.GetComponent<Animator>().Play("Mutant Swiping");
    }

    void ChangeSliderValue(float num)
    {
        bossSlider.value += num;
    }

    void HidePlayerUI()
    {
        playerHealth.SetActive(false);
        playerStamina.SetActive(false);
    }

    void ShowQuestionPanel(bool show)
    {
        if (show == true)
        {
            questionPanel.SetActive(true);
        }
        else if (show == false)
        {
            questionPanel.SetActive(false);
        }
        else
        {
            print("ERROR!");
        }
    }

    public IEnumerator StartMinigame(bool lost)
    {
        if(bossSlider.value >= 100 || bossSlider.value <= 0)
        {
            print("GAME DONE!");
        } 
        else
        {
            if (lost)
            {
                yield return new WaitForSeconds(2f);
                EscapeMinigame escapeMinigame = GameObject.Find("GameManager").GetComponent<EscapeMinigame>();
                escapeMinigame.StartMinigame("Boss");

                while (escapeMinigame.spawnedAmout < escapeMinigame.spawnAmount)
                {
                    yield return new WaitForSeconds(1f);
                    ChangeSliderValue(-2f);
                }

                yield return new WaitForSeconds(1f);

                StartQuiz();
            }
            else if (!lost)
            {
                yield return new WaitForSeconds(1.5f);

                StartQuiz();
            }
        }
    }
}
