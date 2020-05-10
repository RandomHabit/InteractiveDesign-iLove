using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choiceScript : MonoBehaviour
{
    private int cellNumber;
    private int maxSize = 1;
    private Image mine;
    Button butt;
    // Start is called before the first frame update
    void Start()
    {
        butt = GetComponent<Button>();
        butt.onClick.AddListener(madeSelection);
        mine = GetComponent<Image>();
        maxSize = GameObject.Find("startDetector").GetComponent<mainScript>().buttonOptions.Length;
        cellNumber = Random.Range(0, maxSize);
        //UnityEngine.Debug.Log(cellNumber);
        mine.sprite = GameObject.Find("startDetector").GetComponent<mainScript>().buttonOptions[cellNumber];
    }

    void madeSelection()
    {
        UnityEngine.Debug.Log("hey you clicked me");
        GameObject.Find("startDetector").GetComponent<mainScript>().setPicture(cellNumber);
    }
}
