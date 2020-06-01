using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Notifications.Android;
using UnityEngine.Assertions;


public class choiceScript : MonoBehaviour
{
    private int cellNumber;
    private int maxSize = 1;
    private float sizeIncrement = 0.01f;
    private bool grow = false;
    private Image mine;
    private int i;
    public int pickedFunction;
    private bool rotate = false;
    private bool moving = false;
    
    private bool countdown = false;
    private float timeLeft;
    private float timeToFlash;
    public Color auraFlashColor;

    public GameObject popCorn;

    private bool activeButtons = false;
    private int horizontalDirection;
    private int verticalDirection;
    public Vector2 startPosition;
    private bool ignoreBoundaries = false;
    private int[] dirs = new int[] { -4, 4 };

 
    private float changeTime = 2.5f;
    private float timer = 2.5f;

    Button butt;
    public GameObject selfie;
    GameObject[] allButts;


    // Start is called before the first frame update
    void Start()
    {


        var c = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
        i = 0;
        timeToFlash = -1.0f;
        butt = GetComponent<Button>();
        verticalDirection = dirs[Random.Range(0, 2)];
        horizontalDirection = dirs[Random.Range(0, 2)];
        startPosition = butt.transform.position;
        allButts = GameObject.FindGameObjectsWithTag("butts");
        //butt.onClick.AddListener(madeSelection);
        mine = GetComponent<Image>();
        maxSize = GameObject.Find("startDetector").GetComponent<mainScript>().buttonOptions.Length;
        shuffleOptions();
        assignFunction();
    }

    void Update()
    {
        
        var floatingAround = butt.transform.position;
        floatingAround.x += horizontalDirection;
        floatingAround.y += verticalDirection;
        butt.transform.position = floatingAround;
        
        if (countdown)
        {
            timeLeft -= Time.deltaTime;
            changeMessage(timeLeft.ToString("0.00"));
            if (timeToFlash  < 0)
            {
                GameObject.Find("auraObject").GetComponent<Image>().color = auraFlashColor;
                timeToFlash = 3.0f;
            }
            else
            {
                timeToFlash -= Time.deltaTime;
                GameObject.Find("auraObject").GetComponent<Image>().color = Color.Lerp(GameObject.Find("auraObject").GetComponent<Image>().color, Color.clear, 1 * Time.deltaTime);
            }


            if (timeLeft < 0)
            {
                countdown = false;
                playCoolFunction(Random.Range(0, 8));
            }
        }
        if (rotate)
        {
            butt.transform.Rotate(Vector3.forward * 35.0f * Time.deltaTime);

        }
        if (moving)
        {

            UnityEngine.Debug.Log("supposedly moving");
            ///butt.transform.position += transform.forward* Time.deltaTime* 2.0f;
            var pos = butt.transform.position;
            pos.x += 15;
            butt.transform.position = pos;
        }

        if (grow)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                sizeIncrement = sizeIncrement * -1;
                timer = changeTime;
            }
            Vector3 scale = transform.localScale;
            scale.y += sizeIncrement; 
            scale.x += sizeIncrement;
            transform.localScale = scale;
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

    void OnCollisionEnter2D(Collision2D collidedWith)
    {
        if (ignoreBoundaries) { return; }
        else if (collidedWith.gameObject.tag == "horizontalCollider")
        {
            horizontalDirection = horizontalDirection * -1;
        }
        else if (collidedWith.gameObject.tag == "verticalCollider")
        {
            verticalDirection = verticalDirection * -1;
        }
    }

    void changeMessage(string bill)
    {
        GameObject.Find("choiceText").GetComponent<UnityEngine.UI.Text>().text = bill;
    }
    //Change the range based on how many functions we've made
    void assignFunction()
    {
        pickedFunction = Random.Range(0, 11);
    }

    //Add a case and call your function here
    public void playCoolFunction(int p)
    {
        //StartCoroutine(waiter());
        if (p == -1) { p = pickedFunction; }
        switch (p)
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
                StartCoroutine(growUs());
           
                break;
            case (5):
                shuffleAllFunctions();
                break;
            case (6):
                StartCoroutine(brokeMe());
                break;
            case (7):
                notifyMe();
                break;
            case (8):
                countdown = true;
                timeLeft = 10f;
                break;
            case (9):
               StartCoroutine(flashLightHaha());
               break;

            case (10):
                StartCoroutine(spawnPopCorn());
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
        changeMessage("This is what it feels like to get a text incase you forgot.");
        //the parameter is the length of the vibration in milliseconds. Right now its set to 5.5 seconds
        Vibration.Vibrate(4500);
    }

    /// If this one is called and runs on a button that is currently running
    /// moveUs() than when that button reactivates it will continue moving and never reset
    IEnumerator hideAndSeek()
    {
        changeMessage("Why won't you pick one");
        int times = Random.Range(3, 8);
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
            yield return new WaitForSeconds(1);
            billy.SetActive(true);
        }
        changeMessage("It took you this long? You literally had one job!");
    }
    
    IEnumerator flashLightHaha()
    {
        changeMessage("Turning me off should be easy for you, right?");
        GameObject.Find("startDetector").GetComponent<mainScript>().FL_Start();
        yield return new WaitForSeconds(8);
        //GameObject.Find("startDetector").GetComponent<mainScript>().turnOnLight = false;
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
        changeMessage("Great, now look what you did.");
        yield return new WaitForSeconds(1);
        changeMessage("Give me a second to fix all of this.");
        yield return new WaitForSeconds(3);
        foreach (GameObject b in allButts)
        {
            b.SetActive(true);
        }
        changeMessage("Okay, I saved you. You're welcome.");
        activeButtons = !activeButtons;

    }


    void shuffleAllFunctions()
    {
        changeMessage("Good luck trying to understand me with that thick skull.");
        foreach (GameObject b in allButts)
        {
            b.GetComponent<choiceScript>().assignFunction();
            b.GetComponent<choiceScript>().shuffleOptions();
        }
    }


    IEnumerator rotateWee()
    {
        if (rotate) { yield break; }
        changeMessage("Were you hoping for something else to happen? Too bad.");
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

    void notifyMe()
    {
        for (int i = 0; i < 3; i++)
        {

            var notification = new AndroidNotification();
            notification.Title = "Did you know";
            notification.Text = "I can annoy you here as well?";
            notification.FireTime = System.DateTime.Now;

            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
        changeMessage("This is what your phone would do if you had friends.");
    }
    IEnumerator moveUs()
    {
        if (moving) { yield break; }
       changeMessage("I'm not letting you touch me with those fingers");
       moving = true;
        yield return new WaitForSeconds(5);
        moving = false;
        butt.transform.position = startPosition;
    }

    IEnumerator growUs()
    {
        changeMessage("It's crazy something as simple as this can keep you entertained.");
        grow = true;
        yield return new WaitForSeconds(12);
        grow = false;

    }

    IEnumerator spawnPopCorn()
    {
        changeMessage("You're making me break out with those fingers of yours.");
        int tots = Random.Range(5, 15);
        for (int i = 0; i < tots; i++)
        {
            float spawnY = Random.Range(-700, 700);
            float spawnX = Random.Range(-400, 400);
           /** 
            float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            */
            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            GameObject billyBob = Instantiate(popCorn, spawnPosition, Quaternion.identity);
            //billyBob.transform.parent = GameObject.Find("choiceCanvas_D1").transform;
            billyBob.transform.SetParent(GameObject.Find("choiceCanvas_D1").transform, false);
            yield return new WaitForSeconds(.5f);

        }
    }

}
