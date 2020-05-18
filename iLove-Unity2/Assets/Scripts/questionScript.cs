using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questionScript : MonoBehaviour
{
    private string[] questions;
    private int i;
    //string userAnswer;
    Button butt;
    // Start is called before the first frame update
    void Start()
    {
       //userAnswer = "";
       questions = new string[] {
               "What is one thing you love about your face today?",
               "What is one thing you love about your body today?",
               "What is one thing you love about your mind today?",
               "What do you like about your day today?",
               "What is your favorite sense?",
               "What color do you love today?",
               "What is one thing you love about the outdoors?",
               "What do you love about the world today?",
               "What is one thing you love to do?",
               "What smell do you love?",
               "Which season do you love?",
               "What memory do you love?",
               "Who do you love?",
               "What do you love about your personality?",
               "What is your favorite outfit?",
               "When do you feel your best?"
               };
        i = 0;
        butt = GameObject.Find("submitQuestionAnswer").GetComponent<Button>();
        butt.onClick.AddListener(gaveAnswer);

        shuffleQuestion();
    }

    void OnEnable()
    {
        if (i < 1)
        {
            return;
        }
        shuffleQuestion();
    }

    void shuffleQuestion()
    {
        GameObject.Find("questionText").GetComponent<UnityEngine.UI.Text>().text = questions[Random.Range(0, questions.Length)];
        i++;
    }

    public void gaveAnswer()
    {
        string typed = "hi Im daisy";
        typed = GameObject.Find("questionInput").GetComponent<InputField>().text;
        if (typed == null)
        {
            typed = "come back here";
        }
        GameObject.Find("startDetector").GetComponent<mainScript>().setAnswer(typed);
        GameObject.Find("QuestionCanvas_D1").SetActive(false);
        GameObject.Find("QuestionCanvas_D2").SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
