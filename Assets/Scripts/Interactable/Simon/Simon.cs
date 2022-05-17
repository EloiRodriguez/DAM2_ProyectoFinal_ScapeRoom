using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simon : Interactable
{
    private Dictionary<string, Button> buttons;
    private string[] colors = {"red", "green", "yellow", "blue"};
    private new GameObject camera;
    private GameObject player;
    private bool pressing = false;

    private void Start()
    {   
        camera = transform.GetChild(4).gameObject;
        buttons = new Dictionary<string, Button>();
        
        for (int i = 0; i < 4; i++) 
        {
            buttons.Add(colors[i], transform.GetChild(i).GetComponent<Button>());
        }
    }

    private void Update()
    {
        if (camera.activeSelf)
        {
            bool R, G, Y, B;

            R = Input.GetKey(KeyCode.R);
            G = Input.GetKey(KeyCode.G);
            Y = Input.GetKey(KeyCode.Y);
            B = Input.GetKey(KeyCode.B);

            pressing = R || G || Y || B;

            if (R)
            {
                if (!buttons["red"].IsPressing) buttons["red"].Press();
            }
            else
            {
                if (buttons["red"].IsPressing) buttons["red"].UnPress();
            }

            if (G)
            {
                if (!buttons["green"].IsPressing) buttons["green"].Press();
            }
            else
            {
                if (buttons["green"].IsPressing) buttons["green"].UnPress();
            }

            if (Y)
            {
                if (!buttons["yellow"].IsPressing) buttons["yellow"].Press();
            }
            else
            {
                if (buttons["yellow"].IsPressing) buttons["yellow"].UnPress();
            }

            if (B)
            {
                if (!buttons["blue"].IsPressing) buttons["blue"].Press();
            }
            else
            {
                if (buttons["blue"].IsPressing) buttons["blue"].UnPress();
            }

            if (Input.GetKey(KeyCode.Q) && !pressing) Leave();
        }
    }

    public override void Interact(PlayerBehavior player)
    {
        base.Interact(player);

        player.gameObject.SetActive(false);
        camera.SetActive(true);
        this.player = player.gameObject;
    }

    public void Leave()
    {
        CameraInteracion interacion = player.GetComponent<CameraInteracion>();
        
        interacion.IsThrowing = true;
        player.SetActive(true);
        camera.SetActive(false);
    }
}
