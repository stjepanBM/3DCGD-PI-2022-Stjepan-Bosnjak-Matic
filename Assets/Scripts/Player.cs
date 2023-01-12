using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 12.5f;

    public CharacterController myController;

    public float mouseSensitivity = 700f;

    public Transform myCameraHead;
    private float cameraVerticalRotation;

    public GameObject bullet;
    public Transform firePosition;

    public GameObject muzzleFlash, bulletHole, waterLeak;

    void Start()
    {

    }

    void Update()
    {
        PlayerMovement();
        MouseMovement();
        Shoot();
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

        myController.Move(movement * speed * Time.deltaTime);
    }
}
