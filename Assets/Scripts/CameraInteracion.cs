using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInteracion : MonoBehaviour
{
    private new Transform camera;
    private PlayerBehavior player;
    public float raycastDistance = 2;
    private bool interacting = false;
    private bool throwing = false;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.GetComponent<PlayerBehavior>();
        camera = transform.Find("FirstPersonCamera");
    }

    // Update is called once per frame
    void Update()
    {
        Interaction();
        Throwing();    
    }

    private void Interaction()
    {
        Debug.DrawRay(camera.position, camera.forward * raycastDistance, Color.blue);

        RaycastHit hit;

        if (Physics.Raycast(camera.position, camera.forward, out hit, raycastDistance, LayerMask.GetMask("Interactable")))
        {
            Debug.Log("Observing: " + hit.transform.name);

            if (Input.GetKey(KeyCode.E))
            {
                if (!interacting)
                {
                    hit.transform.GetComponent<Interactable>().Interact(player);
                    interacting = true;
                }
            }
            else 
            {
                if (interacting) interacting = false; 
            }
        }
    }

    private void Throwing()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (!throwing)
            {
                player.Drop();
                throwing = true;
            }
        }
        else
        {
            if (throwing) throwing = false;
        }
    }

    public bool IsThrowing
    {
        get { return throwing; }
        set { throwing = value; }
    }
}