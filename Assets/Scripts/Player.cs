using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //speed
    public float speed = 12.5f;
    public float crouchingSpeed = 8.5f;

    //gravity
    public Vector3 velocity;
    public float gravityModifier;

    public CharacterController myController;

    public float mouseSensitivity = 700f;

    public Transform myCameraHead;
    private float cameraVerticalRotation;

    public GameObject bullet;
    public Transform firePosition;

    public GameObject muzzleFlash, bulletHole, waterLeak;

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
        Shoot();
        Crouching();
    }

    private void Crouching()
    {
        if (Input.GetKeyDown(KeyCode.C))
            StartCrouching();

        if (Input.GetKeyUp(KeyCode.C))
            StopCouching();

    }

    private void StartCrouching()
    {
        myBody.localScale = crouchScale;
        myCameraHead.position -= new Vector3(0, 1f, 0);
        myController.height /= 2; 
        isCrouching = true;
    }

    private void StopCouching()
    {
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
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y) * Time.deltaTime;
        }

        myController.Move(velocity);

    }

    private void Shoot()
    {
        //Checking left mouse button
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;

            if (Physics.Raycast(myCameraHead.position, myCameraHead.forward, out hit, 100f))
            {
                //check distance between firing and object

                if (Vector3.Distance(myCameraHead.position, hit.point) > 2f)
                {
                    firePosition.LookAt(hit.point);
                    if (hit.collider.tag == "Shootable")
                        Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));

                    if (hit.collider.CompareTag("WaterLeak"))
                        Instantiate(waterLeak, hit.point, Quaternion.LookRotation(hit.normal));

                }
                if (hit.collider.CompareTag("Enemy"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
            else
            {
                firePosition.LookAt(myCameraHead.position + (myCameraHead.forward * 50f));
            }

            Instantiate(muzzleFlash, firePosition.position, firePosition.rotation, firePosition);
            Instantiate(bullet, firePosition.position, firePosition.rotation);
        }
    }

    private void MouseMovement()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //we need to invert values because of how unity works(our value is positive, so we are subtracting value to invert it)
        cameraVerticalRotation -= mouseY;
        //Mathf.Clamp - Keeps the value between a certain min and max
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);

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

        if (isCrouching)
        {
            movement = movement * crouchingSpeed * Time.deltaTime;
        }
        else
        {
            movement = movement * speed * Time.deltaTime;
        }
        myController.Move(movement);

        velocity.y += Physics.gravity.y * Mathf.Pow(Time.deltaTime, 2) * gravityModifier;

        if (myController.isGrounded)
        {
            velocity.y = Physics.gravity.y * Time.deltaTime;
        }

        myController.Move(velocity);
    }
}
