using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //speed
    public float speed = 12.5f;
    public float crouchingSpeed = 8.5f;
    public float runSpeed = 18.5f;

    //gravity
    public Vector3 velocity;
    public float gravityModifier;

    public CharacterController myController;

    public float mouseSensitivity = 700f;

    public Transform myCameraHead;
    private float cameraVerticalRotation;

    //jump
    public float jumpHeight = 2f;
    private bool readyToJump;
    public Transform ground;
    public LayerMask groundLayer;
    public float groundDistance = 0.5f;

    //crouching
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 bodyScale;
    public Transform myBody;
    private float initialControllerHeight;
    private bool isCrouching = false;

    //animations
    public Animator animator;

    //sliding
    public bool isRunning = false, startSliderTimer;
    public float currentSlideTimer, maxSlideTime = 2f;
    public float slideSpeed = 30f;


    void Start()
    {
        bodyScale = myBody.localScale;
        initialControllerHeight = myController.height;
    }

    void Update()
    {
        PlayerMovement();
        MouseMovement();
        Jump();
        Crouching();
        SlideCounter();
    }

    private void Crouching()
    {
        if (Input.GetKeyDown(KeyCode.C))
            StartCrouching();

        if (Input.GetKeyUp(KeyCode.C) || currentSlideTimer > maxSlideTime)
            StopCouching();

    }

    private void StartCrouching()
    {
        myBody.localScale = crouchScale;
        myCameraHead.position -= new Vector3(0, 1f, 0);
        myController.height /= 2;
        isCrouching = true;

        if (isRunning)
        {
            velocity = Vector3.ProjectOnPlane(myCameraHead.transform.forward, Vector3.up).normalized * slideSpeed * Time.deltaTime;
            startSliderTimer = true;
        }
    }

    private void StopCouching()
    {
        currentSlideTimer = 0f;
        velocity = new Vector3(0f, 0f, 0f);
        startSliderTimer = false;

        myBody.localScale = bodyScale;
        myCameraHead.position += new Vector3(0, 1f, 0);

        myController.height = initialControllerHeight;
        isCrouching = false;
    }

    private void Jump()
    {
        //OverlapSphere - only when colliders are touching we will jump(prevent multiple jumps)
        readyToJump = Physics.OverlapSphere(ground.position, groundDistance, groundLayer).Length > 0;

        if (Input.GetButtonDown("Jump") && readyToJump)
        {
            AudioManager.instance.PlaySFX(2);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y) * Time.deltaTime;
        }

        myController.Move(velocity);
    }

    private void MouseMovement()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //we need to invert values because of how unity works(our value is positive, so we are subtracting value to invert it)
        cameraVerticalRotation -= mouseY;
        //Mathf.Clamp - Keeps the value between a certain min and max
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -88f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        //uses vector3 and converts it to rotation
        myCameraHead.localRotation = Quaternion.Euler(cameraVerticalRotation, 0f, 0f);
    }

    void PlayerMovement()
    {
        //horizontal
        float x = Input.GetAxis("Horizontal");

        //vertical
        float z = Input.GetAxis("Vertical");

        Vector3 movement = x * transform.right + z * transform.forward;

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            movement = movement * runSpeed * Time.deltaTime;
            isRunning = true;
        }
        else if (isCrouching)
        {
            movement = movement * crouchingSpeed * Time.deltaTime;
        }
        else
        {
            movement = movement * speed * Time.deltaTime;
            isRunning = false;
        }

        animator.SetFloat("PlayerSpeed", movement.magnitude);

        myController.Move(movement);

        velocity.y += Physics.gravity.y * Mathf.Pow(Time.deltaTime, 2) * gravityModifier;

        if (myController.isGrounded)
        {
            velocity.y = Physics.gravity.y * Time.deltaTime;
        }

        myController.Move(velocity);
    }

    private void SlideCounter()
    {
        if (startSliderTimer)
        {
            currentSlideTimer += Time.deltaTime;
        }
    }

}
