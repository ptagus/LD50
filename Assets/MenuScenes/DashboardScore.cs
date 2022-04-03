using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashboardScore : MonoBehaviour
{
    public Text NewScoreValue;
    int value;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ScoresUpdate()
    {
        CloseButton.scoreValue++;
        value = CloseButton.scoreValue++;
        NewScoreValue.text = value.ToString();
    }
}
