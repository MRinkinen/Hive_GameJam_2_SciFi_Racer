using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_controller : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Animator animator;
    public Rigidbody rb;
    public float moveSpeed;
    public float brakeZoneSpeed;
    public float maxSpeed;
    public float turnSpeed;
    public float vehicle_drag;
    public int turn_index = 0;
    public int turns_amount = 0;
    public bool breaking;
    public float stopCounter;
    public GameObject[] turns;
    void Start()
    {
        animator = playerMovement.animator;
        rb = playerMovement.rb;
        moveSpeed = playerMovement.moveSpeed;
        //turnSpeed = playerMovement.turnSpeed;
        vehicle_drag = playerMovement.vehicle_drag;
        maxSpeed = playerMovement.maxSpeed;
        breaking = false;
        playerMovement.breakLight.gameObject.SetActive(false);
        //brakeZoneSpeed = moveSpeed * 0.2f;
        //turns = GameObject.FindGameObjectsWithTag("turn");
    }

    void Update()
    {
        if(GameManager.manager.gameStarted && playerMovement.controlled_by_AI)
            handle_AI();
    }

    void turn_AI(int index)
    {
        Vector3 targetDirection = turns[index].transform.position - transform.position;
        float singleStep = 50 * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
    void handle_AI()
    {
        if(!breaking)
        {
            playerMovement.throtleOn = true;
            playerMovement.breakLight.gameObject.SetActive(false);
            rb.AddRelativeForce(Vector3.forward * moveSpeed);
        }
        else
        {
            playerMovement.throtleOn = false;
            playerMovement.breakLight.gameObject.SetActive(true);
            rb.AddRelativeForce(Vector3.forward * moveSpeed * brakeZoneSpeed);
        }
        //Debug.Log(rb.velocity.magnitude);
        if(rb.velocity.magnitude < 2)
        {
            stopCounter += Time.deltaTime;
            if(stopCounter > 2)
            {
                if(turn_index > 0)
                    turn_AI(turn_index -1);
                else
                    turn_AI(7); //Hardcoded index!!!!!
            }
        }
        else
            stopCounter = 0;

        turn_AI(turn_index);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("break_zone"))
        {
            if(rb.velocity.magnitude > 5)
                breaking = true;
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
        if(other.gameObject.CompareTag("break_zone"))
        {
            breaking = false;
            turn_index = other.GetComponent<TurnIndex>().turnIndex;
        }
    }
}
