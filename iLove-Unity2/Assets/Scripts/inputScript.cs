using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputScript : MonoBehaviour
{
    public void gaveAnswer(string typed)
    {
        GameObject.Find("startDetector").GetComponent<mainScript>().setAnswer(typed);
    }
}
