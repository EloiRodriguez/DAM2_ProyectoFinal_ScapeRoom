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
    private List<GameObject> inventory = new List<GameObject>();
    public int inventorySize = 4;
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
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Move();
        RotationControl();
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

        Vector3 move = transform.TransformDirection(new Vector3(x * speed * delimiter, _rigidBody.velocity.y, z * speed * delimiter));

        _rigidBody.velocity = Vector3.ClampMagnitude(move, speed * delimiter);

        //Debug.Log("Speed: " + speed);
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

    public void Pick(GameObject gameObject)
    {
        if (inventory.Count < inventorySize)
        {
            gameObject.SetActive(false);
            inventory.Add(gameObject);
        }
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
