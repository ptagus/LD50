using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeforeStart : MonoBehaviour
{
    GameController gc;
    public Sprite[] numbers;
    public Image img;
    int currentCount;
    void Start()
    {
        gc = GameController.Instance;
        StartCoroutine("TimeForNewNumber");
    }

    IEnumerator TimeForNewNumber()
    {
        yield return new WaitForSeconds(1);
        StartCounter();
    }

    void StartCounter()
    {
        if (currentCount == 2)
        {
            gc.SetReady();
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            img.sprite = numbers[currentCount];
            currentCount++;
            StartCoroutine("TimeForNewNumber");
        }
    }
}
