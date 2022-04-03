using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public GameObject CloseScoreboardButton;
    public GameObject Score;
    public GameObject HighScore;
    public GameObject ScoreValue;
    public static int scoreValue = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void closeScores() 
    {
        Debug.Log("!");
        Score.SetActive(false);
    }
}
