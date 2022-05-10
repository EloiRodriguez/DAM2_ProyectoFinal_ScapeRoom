using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInteracion : MonoBehaviour
{
    private new Transform camera;
    private PlayerBehavior player;
    public float raycastDistance = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.GetComponent<PlayerBehavior>();
        camera = transform.Find("FirstPersonCamera");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(camera.position, camera.forward * 2, Color.blue);

        RaycastHit hit;

        if (Physics.Raycast(camera.position, camera.forward, out hit, raycastDistance, LayerMask.GetMask("Interactable")))
        {
            Debug.Log("Observing: " + hit.transform.name);

            if (Input.GetKey(KeyCode.E))
            {
                hit.transform.GetComponent<Interactable>().Interact(player);
            }
        }
    }
}
