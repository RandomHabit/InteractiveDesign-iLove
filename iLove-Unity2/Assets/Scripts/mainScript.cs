using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainScript : MonoBehaviour
{
    public Sprite[] buttonOptions;
    public int todaysPiction;
    private int todaysAnswer;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPicture(int chosenCell)
    {
        todaysPiction = chosenCell;
    }
}
