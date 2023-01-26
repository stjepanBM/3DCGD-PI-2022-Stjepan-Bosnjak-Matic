using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform myPlayerHead;

    private float startFOV, targetFOV;
    public float FOVSpeed = 1f;
    private Camera myCamera;

    void Start()
    {

        myCamera = GetComponent<Camera>();

        startFOV = myCamera.fieldOfView;
        //Debug.Log();
        targetFOV = startFOV;

        Cursor.lockState = CursorLockMode.Locked;
    }

    //Called after all updates were called(after finishing movement)
    private void LateUpdate()
    {
        transform.position = myPlayerHead.position;
        transform.rotation = myPlayerHead.rotation;

        myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetFOV, FOVSpeed * Time.deltaTime);
        //Debug.Log(myCamera.fieldOfView);
    }

    public void ZoomIn(float targetZoom)
    {
        targetFOV = targetZoom;
    }

    public void ZoomOut()
    {
        targetFOV = startFOV;
    }

}
