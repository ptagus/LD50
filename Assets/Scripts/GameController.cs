using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public TMP_Text text;
    int score = 0;
    public Transform floor;
    public GameObject LoseWindow;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {

    }

//condemn to death
    void Update()
    {
        
    }

    public void SetFloorPosition()
    {
        floor.position -= new Vector3(0, 0.1f, 0);
    }

    public void ShowLoseWindow()
    {
        LoseWindow.SetActive(true);
        Time.timeScale = 0;
    }

    public void SetNewScore()
    {
        score++;
        text.text = "Score: " + score; 
        floor.position -= new Vector3(0, 0.1f, 0);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}