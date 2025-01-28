using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLights : MonoBehaviour
{
    public GameObject panel;
    public RawImage[] s_light;
    public Color yellow;
    public Color green;
    public float time;
    public AudioSource startLightSound;
    public AudioClip startLightSoundYellow;
    public AudioClip startLightSoundGreen;

    public void HandleStartLights()
    {
        startLightSound.clip = startLightSoundYellow;
        panel.gameObject.SetActive(true);

        StartCoroutine(LightTimer(0, 1, Color.yellow));

        StartCoroutine(LightTimer(1, 2, Color.yellow));

        StartCoroutine(LightTimer(2, 3, Color.yellow));


        StartCoroutine(LightTimer(3, 4, Color.green));
        StartCoroutine(LightOffTimer());
    }

    IEnumerator LightTimer(int i, int wait_time, Color color)
    {
        yield return new WaitForSeconds(wait_time);
        if(wait_time > 3)
            startLightSound.clip = startLightSoundGreen;
        startLightSound.Play();
        s_light[i].color = color;
    }
    IEnumerator LightOffTimer()
    {
        yield return new WaitForSeconds(5);
        GetComponent<GameManager>().backgroundMusic.clip = GetComponent<GameManager>().gameAudio;
        GetComponent<GameManager>().backgroundMusic.Play();
        panel.gameObject.SetActive(false);
        GetComponent<GameManager>().gameStarted = true;
    }
}
