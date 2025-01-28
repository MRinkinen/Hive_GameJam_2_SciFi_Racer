using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LapCounter : MonoBehaviour
{
    public TMP_Text lap;
    public string racerName;
    public int lapsDrived;
    public bool incrementLap;
    public bool lapCheck;
    public bool win;
    public string findTextString;
    Vector3 currentSpeed = Vector3.zero;
    public GameObject cam;
    public Transform behindVehicle;
    public float lertpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //lap = GameObject.Find("TEXTHERE").GetComponent<TMP_Text>();
        //lap = GameObject.FindGameObjectWithTag(findTextString).GetComponent<TMP_Text>();
        lapsDrived = 0;
        incrementLap = false;
        lapCheck = false;
        win = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.manager.gameStarted)
        {
            lap.text = racerName + "\n";
        }
        else
        {
            if(lapsDrived == GameManager.manager.maxLaps -1)
                lap.text = racerName + "\nFINAL LAP";
            else if(lapsDrived < GameManager.manager.maxLaps)
                lap.text = racerName + "\nLaps " + Convert.ToString(lapsDrived);
            else if(lapsDrived == GameManager.manager.maxLaps && !GameManager.manager.weHaveWinner)
            {
                GameManager.manager.weHaveWinner = true;
                lap.text = racerName + "\nWINNER!!!! ";
                win = true;
            }
        }
        if(win)
        {
            // cam.transform.position = behindVehicle.transform.position;
            // cam.transform.rotation = behindVehicle.transform.rotation;
            //cam.transform.position = Vector3.SmoothDamp(cam.transform.position, behindVehicle.transform.position, ref currentSpeed, 0.5f);
            cam.transform.position = Vector3.Lerp(cam.transform.position, behindVehicle.transform.position, lertpSpeed);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, behindVehicle.transform.rotation, lertpSpeed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("finish_line"))
        {
            incrementLap = true;
        }
        if(other.gameObject.CompareTag("lap_check"))
        {
            lapCheck = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        // if(other.gameObject.CompareTag("break_zone"))
        // {
        //     turn_AI(turn_index + 1);
        // }
    }
   void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("finish_line"))
        {
            if(incrementLap && lapCheck)
            {
                lapsDrived++;
                incrementLap = false;
                lapCheck = false;
            }
        }
    }

}
