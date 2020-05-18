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
    }

    void OnEnable()
    {
        //This just makes sure OnEnable and onStart don't try to run at the same time causing an error
        if (i < 1)
        {
            return;
        }
        shuffleOptions();
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
}
