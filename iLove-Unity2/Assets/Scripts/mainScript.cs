using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainScript : MonoBehaviour
{
    public Sprite[] buttonOptions;
    public int todaysPiction;
    public string todaysAnswer;
    public allUserResponses theAnswers;

    private bool Active;
    private AndroidJavaObject camera1;
    public bool turnOnLight = false;


    // Start is called before the first frame update

    void Start()
    {
        GUILayout.BeginArea(new Rect(Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.3f, Screen.height * 0.1f));

        theAnswers = new allUserResponses();
        if (PlayerPrefs.HasKey("responses"))
         {
            string jsonString = PlayerPrefs.GetString("responses");
            theAnswers = JsonUtility.FromJson<allUserResponses>(jsonString); 
        }

        theAnswers.allUserResponsesList.Add(new userResponseEntry(3, "Howard Stern"));
        theAnswers.allUserResponsesList.Add(new userResponseEntry(6, "Spiderman"));

      

    }
    void Update()
    {
        /**
        if (turnOnLight)
        {
            FL_Start();
        }*/
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

    public void FL_Start()
    {
        AndroidJavaClass cameraClass = new AndroidJavaClass("android.hardware.Camera");
        WebCamDevice[] devices = WebCamTexture.devices;
        camera1 = cameraClass.CallStatic<AndroidJavaObject>("open", 0);

        if (camera1 != null)
        {
            AndroidJavaObject cameraParameters = camera1.Call<AndroidJavaObject>("getParameters");
            cameraParameters.Call("setFlashMode", "torch");
            camera1.Call("setParameters", cameraParameters);
            ///FIX///// 
            camera1.Call("startPreview");
            Active = true;
            turnOnLight = false;
        }
        else
        {
            Debug.LogError("[CameraParametersAndroid] Camera not available");
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
