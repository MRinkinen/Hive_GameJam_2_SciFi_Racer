using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public bool controlled_by_AI;
    public PlayerInput playerInput;
    public Animator animator;
    public Rigidbody rb;
    public float moveSpeed;
    public float maxSpeed;
    public float turnSpeed;
    public float vehicle_drag;
    public AudioSource vehicleMotor;
    public Light breakLight;
    public int playerIndex;
    public bool throtleOn;
    public ParticleSystem[] paticles;
    public float patricleSpeed;
    // Start is called before the first frame update

    // private void Awake()
    // {
    //     playerInput.gameObject.SetActive(false);
    // }
    void Start()
    {
        // if(!controlled_by_AI)
        //     Destroy(GetComponent<AI_controller>());
        rb.drag = vehicle_drag;
        // foreach(ParticleSystem particle in paticles)
        // {
        //     var main = particle.main;
        // }
        //playerInput.gameObject.SetActive(false);

    }
    void Update()
    {
        if(vehicleMotor)
            HandleSound();
    }
    void FixedUpdate()
    {

        if(!controlled_by_AI)
        {
            if(GameManager.manager.gameStarted)
                HandleMovement();
        }
    }
    void HandleMovement()
    {
        float max_speed_temp;
        float vehicle_drag_temp = vehicle_drag;
        float horizontal = playerInput.actions["Move"].ReadValue<Vector2>().x;
        //float vertical = playerInput.actions["Move"].ReadValue<Vector2>().y;

        float velo = rb.velocity.magnitude;
        float speed_percent = Convert.ToInt32(velo) / maxSpeed * 100;

        if (speed_percent > 80)
            gameObject.transform.Rotate(Vector3.up * (turnSpeed * 0.8f) * horizontal * Time.deltaTime);
        else
            gameObject.transform.Rotate(Vector3.up * turnSpeed * horizontal* Time.deltaTime);
        if(playerInput.actions["Boost"].IsPressed())
            max_speed_temp = maxSpeed * 2;
        else
            max_speed_temp = maxSpeed;
        if(playerInput.actions["Forward"].IsPressed())
        {
            throtleOn = true;
            breakLight.gameObject.SetActive(false);
            rb.AddRelativeForce(Vector3.forward * moveSpeed);
        }
        else if(playerInput.actions["Backward"].IsPressed())
        {
            throtleOn = false;
            breakLight.gameObject.SetActive(true);
            if(speed_percent > 10)
                vehicle_drag_temp = vehicle_drag * 2;
            else
                rb.AddRelativeForce(Vector3.forward * -moveSpeed);
        }
        else
        {
            throtleOn = false;
            breakLight.gameObject.SetActive(true);
        }
        if (rb.velocity.magnitude > max_speed_temp)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        rb.drag = vehicle_drag_temp;
        //Debug.Log(horizontal);
        // if (vertical != 0)
        // {
        //     animator.SetBool("run", true);
        // }
        // else
        // {
        //     animator.SetBool("run", false);
        // }
    }

    void HandleSound()
    {
        if(GameManager.manager.gameStarted)
        {
            if(throtleOn)
            {
                vehicleMotor.pitch = 3;
                foreach(ParticleSystem particle in paticles)
                {
                    var main = particle.main;
                    main.startLifetime = patricleSpeed;
                }
            }
            else
            {
                vehicleMotor.pitch = 1;
                  foreach(ParticleSystem particle in paticles)
                {
                    var main = particle.main;
                    main.startLifetime = patricleSpeed / 2;
                }
            }
        }
    }
    public void AI_Toggle(bool toggle)
    {
        if(toggle)
        {
            controlled_by_AI = true;
        }
        else
        {
            controlled_by_AI = false;
        }
    }
}
