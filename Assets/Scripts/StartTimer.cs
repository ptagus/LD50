using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    public GameObject panelNumber;
    public GameObject currentNumber;
    public GameObject[] numbersMassive;
    int curNumberInMassive = 0;
    bool readyToChange = false;
   
   // bool readyToChange = false;
    // Start is called before the first frame update
    void Start()
    {

        currentNumber = numbersMassive[curNumberInMassive];
        ChangeNumber();
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((readyToChange == true) && (curNumberInMassive <4))
        {
            readyToChange = false;
            Debug.Log(readyToChange);
            Debug.Log(curNumberInMassive);
            ChangeNumber();
        }
    }

     void ChangeNumber()
    {
        currentNumber = numbersMassive[curNumberInMassive++];
        panelNumber.GetComponent<SpriteRenderer>().sprite = currentNumber.GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(TimerCoroutine());
        
        
    }
   IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(2);
        readyToChange = true;
        Debug.Log(readyToChange);
    }
}
