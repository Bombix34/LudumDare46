using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float lookSpeedH = 2f;
    public float lookSpeedV = 2f;
    public float zoomSpeed = 2f;
    public float dragSpeed = 6f;

    public float keyboardSpeed;

    private float yaw = 0f;
    private float pitch = 0f;

    void Update()
    {
        MouseControl();
        KeyboardControl();
    }

    void MouseControl()
    {
        //LOOK AROUND
        if (Input.GetMouseButton(1))
        {
            yaw += lookSpeedH * Input.GetAxis("Mouse X");
            pitch -= lookSpeedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }

        //DRAG
        if (Input.GetMouseButton(2))
        {
            transform.Translate(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed, -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * dragSpeed, 0);
        }

        //ZOOM IN & ZOOM OUT
        transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
    }

    void KeyboardControl()
    {
        Vector3 heading = Vector3.zero;

        int zoom = 0;
        int leftRight = 0;

        //ZOOM IN & ZOOM OUT
        if (Input.GetKey(KeyCode.Z))
            zoom = 1;
        else if (Input.GetKey(KeyCode.S))
            zoom = -1;

        //LEFT & RIGHT
        if (Input.GetKey(KeyCode.Q))
            leftRight = -1;
        else if (Input.GetKey(KeyCode.D))
            leftRight = 1;

        transform.Translate(new Vector3(leftRight * keyboardSpeed, 0, zoom * keyboardSpeed), Space.Self);
    }

}