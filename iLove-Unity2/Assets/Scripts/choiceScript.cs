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
    private bool rotate = false;
    private bool moving = false;
    private bool activeButtons = false;
    public Vector2 startPosition;

    Button butt;
    public GameObject selfie;
    GameObject[] allButts;
    // Start is called before the first frame update
    void Start()
    {
        
        i = 0;
        butt = GetComponent<Button>();

        startPosition = butt.transform.position;
        allButts = GameObject.FindGameObjectsWithTag("butts");
        butt.onClick.AddListener(madeSelection);
        mine = GetComponent<Image>();
        maxSize = GameObject.Find("startDetector").GetComponent<mainScript>().buttonOptions.Length;
        //shuffleQuestion throws an error on "start" but still works so not gonna question it
        //No errors thrown when runs onEnable, but doesn't shuffle the question on the frist go round if not here
        shuffleOptions();
        assignFunction();
    }

    void Update()
    {
        if (rotate)
        {
            butt.transform.Rotate(Vector3.forward * 35.0f * Time.deltaTime);

        }

        if (moving)
        {

            UnityEngine.Debug.Log("supposedly moving");
            ///butt.transform.position += transform.forward* Time.deltaTime* 2.0f;
            var pos = butt.transform.position;
            pos.x += 12;
            butt.transform.position = pos;
        }
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
        //UnityEngine.Debug.Log("hey you clicked me");
        GameObject.Find("startDetector").GetComponent<mainScript>().setPicture(cellNumber);
    }



    void changeMessage(string bill)
    {
        GameObject.Find("choiceText").GetComponent<UnityEngine.UI.Text>().text = bill;
    }
    //Change the range based on how many functions we've made
    void assignFunction()
    {
        pickedFunction = Random.Range(0, 6);
    }

    //Add a case and call your function here
    public void playCoolFunction()
    {
        //StartCoroutine(waiter());

        switch (pickedFunction)
        {
            case (0):
                //custom vibrations (have to add the vibration script to the button)
                CustomViber();
                break;

            case (1):
                StartCoroutine(hideAndSeek());
                break;

            case (2):
                StartCoroutine(rotateWee());
                break;
            case (3):
                StartCoroutine(moveUs());
                break;

            case (4):
                StartCoroutine(brokeMe());
                break;
            case (5):
                shuffleAllFunctions();
                break;

            default:
                UnityEngine.Debug.Log("Didn't work");
                break;
        }
    }

    //Add your functions below

    /**
     * Wait function example
     * 
    IEnumerator waiter()
    {
        changeMessage("hey it worked");
        yield return new WaitForSeconds(5);
        changeMessage("Ok i waited");
    }
*/
    void vibeVibeWee()
    {
        Vibration.Vibrate();
    }

    void CustomViber(){
        changeMessage("buzz buzz buddy");
        //the parameter is the length of the vibration in milliseconds. Right now its set to 5.5 seconds
        Vibration.Vibrate(5500);
    }

    //This one is bugged still. Need to have it not do it to the button that called it
    //Can't seem to figure out how
    IEnumerator hideAndSeek()
    {
        changeMessage("It would be a shame if, everything started disappearing");
        int times = Random.Range(1, 6);
        for (int i = 0; i < times; i++)
        {
            GameObject billy = allButts[Random.Range(0, allButts.Length)];

            while (GameObject.ReferenceEquals(billy.GetComponent<Button>(), butt))
            {
                UnityEngine.Debug.Log("they are equal");
                billy = allButts[Random.Range(0, allButts.Length)];

            }
            yield return new WaitForSeconds(1);
            billy.SetActive(false);
            yield return new WaitForSeconds(Random.Range(1,4));
            billy.SetActive(true);
        }
        changeMessage("Ok that was fun");
    }
    IEnumerator brokeMe()
    {
        if (activeButtons) { yield break; }
        activeButtons = !activeButtons;
        foreach (GameObject b in allButts)
        {
            if (!GameObject.ReferenceEquals(b.GetComponent<Button>(), butt))
            {
                b.SetActive(false);
            }
        }
        changeMessage("Oh look, you broke me. Imagine that.");
        yield return new WaitForSeconds(1);
        changeMessage("Hang on I'm rebooting.");
        yield return new WaitForSeconds(3);
        foreach (GameObject b in allButts)
        {
            b.SetActive(true);
        }
        changeMessage("Try not to mess it up again");
        activeButtons = !activeButtons;

    }
    void shuffleAllFunctions()
    {
        changeMessage("You thought you had me memorized? Well not anymore!");
        foreach (GameObject b in allButts)
        {
            b.GetComponent<choiceScript>().assignFunction();
            b.GetComponent<choiceScript>().shuffleOptions();
        }
    }
    IEnumerator rotateWee()
    {
        if (rotate) { yield break; }
        changeMessage("Try not to get dizzy");
        foreach (GameObject b in allButts)
        {
            b.GetComponent<Button>().GetComponent<choiceScript>().rotate = true;
        }
        yield return new WaitForSeconds(6);
        foreach (GameObject b in allButts)
        {
            b.GetComponent<Button>().GetComponent<choiceScript>().rotate = false;
        }

    }
    IEnumerator moveUs()
    {
        if (moving) { yield break; }
       changeMessage("Catch me if you can");
       moving = true;
        yield return new WaitForSeconds(5);
        moving = false;
        butt.transform.position = startPosition;
    }

}
