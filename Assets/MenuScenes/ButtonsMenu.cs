using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ButtonsMenu : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject scoresPanel;

    public void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }
    public void ShowScore() 
    {
        scoresPanel.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
