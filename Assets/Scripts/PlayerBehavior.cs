using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody _rigidBody;
    public float sprint = 20;
    public float acceleration = 0.5f;
    private float speed = 0;
    public float mouse_sensitivity = 10;
    private float verticalRotation = 0;
    private GameObject player_camera;
    private bool moving;
    private Inventory inventory;
    public AudioClip footsteps_slow, footsteps_fast;
    private AudioSource footsteps;
    private bool running = false;

    void Awake() 
    {
        player_camera = transform.Find("FirstPersonCamera").gameObject;
        
        _rigidBody = gameObject.GetComponent<Rigidbody>();

        footsteps = player_camera.GetComponent<AudioSource>();

        footsteps.clip = footsteps_slow;
        
        footsteps.loop = true;
        
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");

        if (inventory != null) this.inventory = inventory.GetComponent<Inventory>();

        //Debug.Log(invTrans);
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MouseScrolling();
    }

    void FixedUpdate()
    {
        Move();
        RotationControl();
        InventoryControl();
    }

    private void RotationControl()
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

        player_camera.transform.Rotate(vRotM * -1, 0, 0);

        transform.Rotate(0, hRotM, 0);

        verticalRotation += vRotM;
    }

    private void Move()
    {
        bool W, A, S, D;
        float x = 0, z = 0, moderate = 0, delimiter = 0.5f;

        W = Input.GetKey(KeyCode.W);
        A = Input.GetKey(KeyCode.A);
        S = Input.GetKey(KeyCode.S);
        D = Input.GetKey(KeyCode.D);

        moving = W || A || S || D;

        if (W) z += 1;
        if (S) z += -1;
        if (D) x += 1;
        if (A) x += -1;

        if (Input.GetKey(KeyCode.LeftControl)) 
        {
            delimiter = 1;
            
            if (!running)
            {
                footsteps.clip = footsteps_fast;
                running = true;
            }
        }
        else
        {
            if (running)
            {
                footsteps.clip = footsteps_slow;
                running = false;
            }
        }

        if (moving)
        {
            if (speed + acceleration > sprint)
            {
                moderate = speed + acceleration - sprint;
            }

            speed += acceleration - moderate;

            PlaySteps(true);
            CameraBobbing();
        }
        else 
        {
            float stopping = 0;

            stopping = speed / 4;

            if (speed - stopping < speed)
            {
                stopping += speed - stopping;
            }

            speed -= stopping;

            PlaySteps(false);

            BobbingStop();
        }

        Vector3 move = _rigidBody.velocity;

        move.x = x * speed * delimiter;
        move.z = z * speed * delimiter;

        move = Vector3.ClampMagnitude(transform.TransformDirection(move), speed * delimiter);
        move.y = _rigidBody.velocity.y;

        _rigidBody.velocity = move;
    }

    private void InventoryControl()
    {
        int key = inventory.Selected;
        bool A1, A2, A3, A4;

        A1 = Input.GetKey(KeyCode.Alpha1);
        A2 = Input.GetKey(KeyCode.Alpha2);
        A3 = Input.GetKey(KeyCode.Alpha3);
        A4 = Input.GetKey(KeyCode.Alpha4); 

        if (A1 || A2 || A3 || A4)
        {
            if (A1) key = 0;
            if (A2) key = 1;
            if (A3) key = 2;
            if (A4) key = 3;
        }

        if (key != inventory.Selected) inventory.Selected = key;
    }

    private void MouseScrolling()
    {
        int scroll = (int) (Input.GetAxis("Mouse ScrollWheel") * 10);
        int index = inventory.Selected;

        index -= scroll;

        if (index < 0) index = 3;
        if (index > 3) index = 0;

        if (index != inventory.Selected) inventory.Selected = index;

        //Debug.Log("Scrolling: " + scroll);
    }

    private void BobbingStop()
    {
        Animator anim = player_camera.GetComponent<Animator>();
        anim.speed = 0;
    }

    private void CameraBobbing()
    {
        Animator anim = player_camera.GetComponent<Animator>();
        float sp = 1;

        if (running)
        {
            sp = 2;
        }
        
        anim.speed = sp;

    }

    public void Pick(GameObject item)
    {
        if (inventory != null)
        {
            if (!inventory.SelectedEmpty())
            {
                Drop();
            }

            item.SetActive(false);
            inventory.SelectedSlot.SaveItem(item);
        }

        inventory.SetItemName();
    }

    public void Drop()
    {
        if (!inventory.SelectedEmpty())
        {
            GameObject item = inventory.SelectedSlot.DropItem();

            item.transform.position = transform.position + transform.forward * 1;
            item.SetActive(true);

            Rigidbody itemBody = item.GetComponent<Rigidbody>();
            
            if (itemBody != null)
            {
                itemBody.velocity = Vector3.zero;
                itemBody.angularVelocity = Vector3.zero;
            }
        }

        inventory.SetItemName();
    }

    private void PlaySteps(bool play)
    {
        if (!footsteps.isPlaying && play)
        {
            footsteps.Play();
        }
        else if (footsteps.isPlaying && !play)
        {
            footsteps.Pause();
        }
    }
}
