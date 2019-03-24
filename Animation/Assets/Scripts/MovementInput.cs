﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour
{

    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed;
    public Animator anim;
    public float speed = 1;
    public float speedInput;
    public float allowPlayerRotation;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;

    public float fallYspeed;

    private float verticalVel;
    private Vector3 moveVector;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude();


        //Debug.Log(controller.isGrounded);
        /*if(!controller.isGrounded)
        {
            moveVector = new Vector3(0, fallYspeed, 0);
            controller.SimpleMove(moveVector);
        }*/
        

        //If you don't need the character grounded then get rid of this part.
        /*isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            verticalVel -= 0;
        }
        else
        {
            verticalVel -= 2;
        }
        moveVector = new Vector3(0, verticalVel, 0);
        controller.Move(moveVector);*/
        //
    }

    void InputMagnitude()
    {
        //Calculate Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        //anim.SetFloat("InputZ", InputZ, 0.0f, Time.deltaTime * 2f);
        //anim.SetFloat("InputX", InputX, 0.0f, Time.deltaTime * 2f);

        //Calculate the Input Magnitude
        speedInput = new Vector2(InputX, InputZ).sqrMagnitude;

        //Physically move player
        if (speedInput > allowPlayerRotation)
        {
            //anim.SetFloat("InputMagnitude", speedInput, 0.0f, Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else if (speedInput < allowPlayerRotation)
        {
            //anim.SetFloat("InputMagnitude", speedInput, 0.0f, Time.deltaTime);
        }
    }

    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;

        if (blockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        }

        float diagonalModifier = 1f;
        if ((Mathf.Abs(desiredMoveDirection.x) + Mathf.Abs(desiredMoveDirection.y) + Mathf.Abs(desiredMoveDirection.z)) > 1.5)
            diagonalModifier = 0.7f;
        else
            diagonalModifier = 1f;

        transform.Translate(Vector3.forward * speedInput * speed * diagonalModifier * Time.deltaTime);
        
    }

    

}