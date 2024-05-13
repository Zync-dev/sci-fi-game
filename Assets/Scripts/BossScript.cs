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
    public Button answer2;
    public Button answer3;
    public Button answer4;

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

    void Fade(int fadeType)
    {
        if(fadeType == 0)
        {
            fadePanel.GetComponent<Animator>().Play("FadeIn");
        }
        else if (fadeType == 1)
        {
            fadePanel.GetComponent<Animator>().Play("FadeOut");
        }
        else
        {
            print("ERROR! (NOT CORRECT FADETYPE)");
        }
    }

    void AfterFadeIn()
    {
        playerModel.transform.position = enemyModel.transform.position;
        playerModel.transform.position += new Vector3(-5f, 0f ,0f);
        virtualCamera.GetComponent<Animator>().Play("BossAnimation");
    }

    void MoveCamera()
    {
        // Move camera to behind player looking at enemy
        // AKA play camera animation
    }

    float chosenQuestion;
    void StartQuiz()
    {
        ShowQuestionPanel(true);

        chosenQuestion = Random.Range(0f, questions.Length/6);

        answer1.GetComponentInChildren<TMP_Text>().text = questions[(int)chosenQuestion, 1].ToString();
        answer2.GetComponentInChildren<TMP_Text>().text = questions[(int)chosenQuestion, 2].ToString();
        answer3.GetComponentInChildren<TMP_Text>().text = questions[(int)chosenQuestion, 3].ToString();
        answer4.GetComponentInChildren<TMP_Text>().text = questions[(int)chosenQuestion, 4].ToString();

        // List of questions with answers in UI.
        // Variabel der fortæller hvilket svar der er det korrekte for hvert spørgsmål.
        // Efter Quiz Start Minigame!
        // Herefter start Quiz igen!
    }

    void Answer1BtnClicked()
    {
        if (int.Parse(questions[(int)chosenQuestion, 6]) == 1)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
    void Answer2BtnClicked()
    {
        if (int.Parse(questions[(int)chosenQuestion, 6]) == 2)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
    void Answer3BtnClicked()
    {
        if (int.Parse(questions[(int)chosenQuestion, 6]) == 3)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
    void Answer4BtnClicked()
    {
        if (int.Parse(questions[(int)chosenQuestion, 6]) == 4)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    void CorrectAnswer()
    {
        correctOrWrongTxt.color = Color.green;
        correctOrWrongTxt.text = "CORRECT!";

        ChangeSliderValue(20);
    }

    void WrongAnswer()
    {
        correctOrWrongTxt.color = Color.red;
        correctOrWrongTxt.text = "WRONG!";
        
        ChangeSliderValue(-20);
    }

    void ChangeSliderValue(float num)
    {
        if (num < 0)
        {
            bossSlider.value -= num;
        } else if(num > 0)
        {
            bossSlider.value += num;
        }
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
