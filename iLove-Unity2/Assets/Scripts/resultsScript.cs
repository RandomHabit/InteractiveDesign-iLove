using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class resultsScript : MonoBehaviour
{
    private mainScript neededFunctions;
    //private List<T> neededList;
    // Start is called before the first frame update
    void Start()
    {
        // GameObject.Find("startDetector").GetComponent<mainScript>().setAnswer(typed);
        //theAnswers.allUserResponsesList;
        UnityEngine.Debug.Log("This shouldn't run yet");
        neededFunctions = GameObject.Find("startDetector").GetComponent<mainScript>();
       // neededList = GameObject.Find("startDetector").GetComponent<mainScript>().theAnswers.allUsersResponseList;

        for (int i = 0; i < neededFunctions.theAnswers.allUserResponsesList.Count; i++)
       // for (int i = 0; i < GameObject.Find("startDetector").GetComponent<mainScript>().theAnswers.allUserResponsesList.Count; i++)
        {
            int b = i + 1;
            string bill = "result" + b.ToString();
            UnityEngine.Debug.Log(bill);
            //GameObject.Find(bill).GetComponent<Image>.sprite = GameObject.Find("startDetector").GetComponent<mainScript>().buttonOptions[neededList[i].getImage()];
            GameObject.Find(bill).GetComponent<Image>().sprite = neededFunctions.buttonOptions[neededFunctions.theAnswers.allUserResponsesList[i].getImage()];
            //GameObject.Find(bill).GetComponent<Image>.sprite = GameObject.Find("startDetector").GetComponent<mainScript>().buttonOptions[GameObject.Find("startDetector").GetComponent<mainScript>().theAnswers.allUsersResponseList[i].getImage()];
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
