using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        targetFOV = startFOV;
        Debug.Log(targetFOV);

        Cursor.lockState = CursorLockMode.Locked;
    }

    //Called after all updates were called(after finishing movement)
    private void LateUpdate()
    {
        transform.position = myPlayerHead.position;
        transform.rotation = myPlayerHead.rotation;

        myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetFOV, FOVSpeed * Time.deltaTime);
    }

    public void ZoomIn(float targetZoom)
    {
        targetFOV = targetZoom;
    }

    public void ZoomOut()
    {
        targetFOV = startFOV;
    }

    public void OpenScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
