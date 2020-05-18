using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainScript : MonoBehaviour
{
    public Sprite[] buttonOptions;
    public int todaysPiction;
    public string todaysAnswer;
    public allUserResponses theAnswers;

    // Start is called before the first frame update
    
    void Start()
    {
        theAnswers = new allUserResponses();
        if (PlayerPrefs.HasKey("responses"))
         {
            string jsonString = PlayerPrefs.GetString("responses");
            theAnswers = JsonUtility.FromJson<allUserResponses>(jsonString); 
        }

        theAnswers.allUserResponsesList.Add(new userResponseEntry(3, "Howard Stern"));
        theAnswers.allUserResponsesList.Add(new userResponseEntry(6, "Spiderman"));


    }
    
    public void beamMeUpScotty()
    {
        theAnswers = new allUserResponses();
        if (PlayerPrefs.HasKey("responses"))
        {
            string jsonString = PlayerPrefs.GetString("responses");
            theAnswers = JsonUtility.FromJson<allUserResponses>(jsonString);
        }

        theAnswers.allUserResponsesList.Add(new userResponseEntry(3, "Howard Stern"));
        theAnswers.allUserResponsesList.Add(new userResponseEntry(6, "Spiderman"));
    }

    [System.Serializable]
    public class userResponseEntry
    {
        private int cell;
        private string response;

        public userResponseEntry(int num, string bill)
        {
            cell = num;
            response = bill;
        }

        public int getImage()
        {
            return cell;
        }
        
        public string getReponse()
        {
            return response;
        }
    }
    
    
    public class allUserResponses 
    {
        public List<userResponseEntry> allUserResponsesList;

        public allUserResponses(List<userResponseEntry> hi)
        {
            allUserResponsesList = hi;
        }

        public allUserResponses()
        {
            allUserResponsesList = new List<userResponseEntry> { };
        }
    }

    public void save()
    {
        //allUserResponses fullList = new allUserResponses(allUserReponsesList = theAnswersallUserResponsesList);
        string bob = JsonUtility.ToJson(theAnswers);
        PlayerPrefs.SetString("responses", bob);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("responses"));
    }
    
    private void addToday()
    {
        if (theAnswers.allUserResponsesList.Count == 7)
        {
            theAnswers.allUserResponsesList.RemoveAt(0);
        }
        userResponseEntry billy = new userResponseEntry(todaysPiction, todaysAnswer);
        theAnswers.allUserResponsesList.Add(billy);
        save();
    }

    public void setPicture(int chosenCell)
    {
        todaysPiction = chosenCell;
    }

    public void setAnswer(string userInput)
    {
        todaysAnswer = userInput;
    }
}
