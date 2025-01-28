using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Globalization;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public int laps;
    public int players;
    public bool gameStarted;
    public GameObject camera_start;
    public GameObject camera_game;
    public GameObject cam;
    public GameObject[] racerStartPositions;
    public GameObject[] racerPrefabs;
    public GameObject[] racerMenuModels;
    public AudioSource backgroundMusic;
    public AudioClip startAudio;
    public AudioClip gameAudio;
    public AudioClip gameOverAudio;
    public AudioClip gameWonAudio;
    public int maxLaps;
    public bool weHaveWinner;
    public GameObject menu;
    public Slider lapSlider;
    int connectedDevices;
    public TMP_Text lapsText;
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        manager = this;
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        backgroundMusic.clip = startAudio;
        backgroundMusic.Play();
        gameStarted = false;
        weHaveWinner = false;
        cam.transform.position = camera_start.transform.position;
        cam.transform.rotation = camera_start.transform.rotation;
        lapsText.text = "Number of laps " + maxLaps;
        // for(int i = 0; i < 4; i++)
        // {
        //     //Destroy(racerMenuModels[i]);
        //     Instantiate(racerPrefabs[i],racerStartPositions[i].transform.position, Quaternion.identity);
        // }

    }
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     StartGame();
        //     // menu.gameObject.SetActive(false);
        //     // lapSlider.gameObject.SetActive(false);
        //     // backgroundMusic.Stop();
        //     // // backgroundMusic.clip = gameAudio;
        //     // // backgroundMusic.Play();
        //     // cam.transform.position = camera_game.transform.position;
        //     // cam.transform.rotation = camera_game.transform.rotation;
        //     // gameStarted = true;
        // }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menu.gameObject.activeInHierarchy)
                menu.gameObject.SetActive(false);
            else if(!menu.gameObject.activeInHierarchy)
                menu.gameObject.SetActive(true);

            //Application.Quit();
        }
        if (connectedDevices < 4)
        {
            // Tässä katsotaan kiinnitettyjen ohjaimien määrä, joka määrittää pelaajien määrän
            int allGamepads = Gamepad.all.Count;
            //int allDevices = InputSystem.devices.Count;


            // foreach (InputDevice inp in InputSystem.devices)
            // {
            //     Debug.Log("Laitteet: " + inp.name);
            // }

            //connectedDevices = allGamepads;
            connectedDevices = allGamepads;
            //Debug.Log("yhdstettyjä: " + connectedDevices);
            // +1, koska tämä laskee vain kiinnitetyt ohjaimet. Testaus ja kehitys vaiheessa käytössä on vielä näppäimistö ja hiiri testaamisen helpottamiseksi
        }
    }

    public void SetLaps()
    {
        if(!gameStarted)
        {
            maxLaps = (int)lapSlider.value;
            lapsText.text = "Number of laps " + maxLaps;
        }
    }
    public void StartGame()
    {
        if(!gameStarted)
        {
            menu.gameObject.SetActive(false);
            lapSlider.gameObject.SetActive(false);
            backgroundMusic.Stop();
            // backgroundMusic.clip = gameAudio;
            // backgroundMusic.Play();
            cam.transform.position = camera_game.transform.position;
            cam.transform.rotation = camera_game.transform.rotation;
            GetComponent<StartLights>().HandleStartLights();
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
            // backgroundMusic.Stop();
            // backgroundMusic.clip = startAudio;
            // //backgroundMusic.Play();
            // gameStarted = false;
            // weHaveWinner = false;
            // cam.transform.position = camera_start.transform.position;
            // cam.transform.rotation = camera_start.transform.rotation;
            // lapsText.text = "Number of laps " + maxLaps;
        }
        //gameStarted = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
