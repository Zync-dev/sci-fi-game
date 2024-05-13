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
        { "Question 1", "Answer 1", "Answer 2", "Answer 3", "Answer 4", "3" /* <---- CORRECT ANSWER IS 3*/},
        { "Question 2", "Answer 1", "Answer 2", "Answer 3", "Answer 4", "2" /* <---- CORRECT ANSWER IS 2*/},
        { "Question 3", "Answer 1", "Answer 2", "Answer 3", "Answer 4", "4" /* <---- CORRECT ANSWER IS 4*/},
        { "Question 4", "Answer 1", "Answer 2", "Answer 3", "Answer 4", "1" /* <---- CORRECT ANSWER IS 1*/}
    };


    // Slider value 1-100
    // start value = 50
    // When player answer correct, value rise
    // When player answer wrong, slider fall
    // When minigame playing, make slider fall a little every second

    public void StartBossFight()
    {
        Fade(0);
    }

    public void Fade(int fadeType)
    {
        if(fadeType == 0)
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
        playerModel.transform.position += new Vector3(-5f, 0f ,0f);
        virtualCamera.GetComponent<Animator>().Play("BossAnimation");

        bossSlider.gameObject.SetActive(true);
    }

    int chosenQuestion;
    public void StartQuiz()
    {
        ShowQuestionPanel(true);
        DisableButtons(true); //Makes buttons interactable (btn.interactable = true)

        chosenQuestion = Random.Range(0, questions.Length/6);

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
    }

    void WrongAnswer()
    {
        correctOrWrongTxt.color = Color.red;
        correctOrWrongTxt.text = "WRONG!";

        questionPanel.GetComponent<Animator>().Play("RevealCorrectWrong");
        
        ChangeSliderValue(-20);
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
        if(show == true)
        {
            questionPanel.SetActive(true);
        } else if (show == false)
        {
            questionPanel.SetActive(false);
        } else 
        {
            print("ERROR!");
        }
    }
}
