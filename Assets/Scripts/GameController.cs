using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public Text text;
    int score = 0;
    public bool ready;
    public Transform floor;

    [Header("Windows")]
    public GameObject LoseWindow;
    public GameObject Pause;
    public Text scoreText;

    [Header("EnemyPrefabs")]
    public int HowMuchEnemies;
    public GameObject[] enemies;
    public Transform[] spawnPoints;

    [Header("Enemies")]
    public float enemySpeed;
    public float enemyJumpPower;
    public float JumpTimerMin;
    public float JumpTimerMax;
    public float StateChengeTimerMin;
    public float StateChengeTimerMax;

    [Header("EnemyAnimation")]
    public float enemyRunSpeed;
    public float enemyIdleSpeed;
    public float enemyJumpSpeed;
    public float enemyStandSpeed;

    [Header("PlayerParams")]
    public float playerSpeed;
    public float PlayerPushPower;
    public float PlayerPushPowerY;
    public float PlayerMass;
    [Header("Floor")]
    public float floorSpeedDown;
    public float endOfFloor;
    public bool end;
    [Header("Hand")]
    public float handSpeed;
    public float handDeltaSpeed;
    public float yBorder;

    [Header("Music")]
    public AudioSource GameSoundes;
    public AudioClip HandTakeABear;
    public AudioClip EatBear;
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
    public void SetReady()
    {
        ready = true;
        CreateBoys();
    }

    void CreateBoys()
    {
        for (int i=0; i < HowMuchEnemies; i++)
        {
            spawnPoints[i].position = new Vector3(spawnPoints[i].position.x, floor.position.y+1, 0);
            int boy = Random.Range(0, enemies.Length);
            Instantiate(enemies[boy], spawnPoints[i].transform.position, Quaternion.identity);
        }
    }

    public void CreateNewBoy()
    {
        int boy = Random.Range(0, enemies.Length);
        int point = Random.Range(0, spawnPoints.Length);
        Instantiate(enemies[boy], spawnPoints[point].transform.position, Quaternion.identity);
    }

   

    public void SetFloorDown()
    {
        GameSoundes.clip = HandTakeABear;
        GameSoundes.Play();
        floor.position -= new Vector3(0, floorSpeedDown, 0);
    }

    public void SetNewScore()
    {
        GameSoundes.clip = EatBear;
        GameSoundes.Play();
        score++;
        text.text = "Score: " + score;
        if (floor.transform.position.y < endOfFloor)
        {
            end = true;
        }
    }

    public float SetJumpTimer()
    {
        return Random.Range(JumpTimerMin, JumpTimerMax);
    }

    public float SetStateTimer()
    {
        return Random.Range(StateChengeTimerMin, StateChengeTimerMax);
    }

    //UIMenu
    public void ShowLoseWindow()
    {
        LoseWindow.SetActive(true);
        scoreText.text = "Score " + score;
        Time.timeScale = 0;
    }

    public void OpenPause()
    {
        Pause.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
