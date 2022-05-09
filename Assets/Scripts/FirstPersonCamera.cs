using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    
    private GameObject player;
    private float mouse_sensitivity = 5;
    private float verticalRotation = 0;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;
    }

    void FixedUpdate()
    {
        float x, y, vRotM, hRotM;

        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        hRotM = x * mouse_sensitivity;
        vRotM = y * mouse_sensitivity;

        if ((verticalRotation + vRotM) > 90)
        {
            vRotM -= (verticalRotation + vRotM) - 90;
        }
        else if ((verticalRotation + vRotM) < -90)
        {
            vRotM -= (verticalRotation + vRotM) + 90;
        }

        //transform.RotateAround(transform.position, Vector3.up, hRotM);
        transform.RotateAround(Vector3.zero, -transform.right, vRotM);

        //player.transform.RotateAround(player.transform.position, Vector3.up, hRotM);

        player.transform.Rotate(0, hRotM, 0);

        verticalRotation += vRotM;

        FollowPlayer();
    }

    private void FollowPlayer()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
    }
}
