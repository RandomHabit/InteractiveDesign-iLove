using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choiceScript : MonoBehaviour
{
    private int cellNumber;
    private int maxSize = 1;
    private Image mine;
    private int i;
    public int pickedFunction;
    Button butt;
    // Start is called before the first frame update
    void Start()
    {
        
        i = 0;
        butt = GetComponent<Button>();
        butt.onClick.AddListener(madeSelection);
        mine = GetComponent<Image>();
        maxSize = GameObject.Find("startDetector").GetComponent<mainScript>().buttonOptions.Length;
        //shuffleQuestion throws an error on "start" but still works so not gonna question it
        //No errors thrown when runs onEnable, but doesn't shuffle the question on the frist go round if not here
        shuffleOptions();
        assignFunction();
    }

    void OnEnable()
    {
        //This just makes sure OnEnable and onStart don't try to run at the same time causing an error
        if (i < 1)
        {
            return;
        }
        shuffleOptions();
        assignFunction();
    }

    void shuffleOptions()
    {
        cellNumber = Random.Range(0, maxSize);
        mine.sprite = GameObject.Find("startDetector").GetComponent<mainScript>().buttonOptions[cellNumber];
        i++;
    }
    void madeSelection()
    {
        UnityEngine.Debug.Log("hey you clicked me");
        GameObject.Find("startDetector").GetComponent<mainScript>().setPicture(cellNumber);
    }


    //Change the range based on how many functions we've made
    void assignFunction()
    {
        pickedFunction = Random.Range(0, 0);
    }

    //Add a case and call your function here
    public void playCoolFunction()
    {
       switch (pickedFunction)
        {
            case (0):
                vibeVibeWee();
                break;

            case (1):
            //custom vibrations (have to add the vibration script to the button)
                CustomViber();
                break;

            default:
                UnityEngine.Debug.Log("Didn't work");
                break;
        }
    }

    //Add your functions below

    void vibeVibeWee()
    {
        Vibration.Vibrate();
    }

    void CustomViber(){
        //the parameter is the length of the vibration in milliseconds. Right now its set to 5.5 seconds
        Vibration.Vibrate(5500);
    }
}
