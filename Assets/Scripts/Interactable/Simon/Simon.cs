using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simon : Interactable
{
    private Dictionary<string, Button> buttons;
    private string[] colors = {"red", "green", "yellow", "blue"};
    private new GameObject camera;
    private GameObject player;
    public AudioSource error;
    public AbrirCerrarCajon cajon;
    private bool pressing = false;
    
    //Game algorithm variables;
    private List<string> repeats = new List<string>();
    private int repeats_index = 0;
    private bool player_play = false, cpu_play = true;
    private bool complete = false;

    private void Start()
    {   
        camera = transform.GetChild(4).gameObject;
        buttons = new Dictionary<string, Button>();
        
        for (int i = 0; i < 4; i++) 
        {
            buttons.Add(colors[i], transform.GetChild(i).GetComponent<Button>());
        }


        SetButtonsLight(false);
    }

    private void Update()
    {
        if (camera.activeSelf)
        {
            Interacting();
            Play();
        }
    }

    private void Interacting()
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
            if (buttons["red"].IsPressing) 
            {
                buttons["red"].UnPress();
                AnalizeMove(0);
            }
        }

        if (G)
        {
            if (!buttons["green"].IsPressing) buttons["green"].Press();
        }
        else
        {
            if (buttons["green"].IsPressing)
            {
                buttons["green"].UnPress();
                AnalizeMove(1);
            }
        }

        if (Y)
        {
            if (!buttons["yellow"].IsPressing) buttons["yellow"].Press();
        }
        else
        {
            if (buttons["yellow"].IsPressing) 
            {
                buttons["yellow"].UnPress();
                AnalizeMove(2);
            }
        }

        if (B)
        {
            if (!buttons["blue"].IsPressing) buttons["blue"].Press();
        }
        else
        {
            if (buttons["blue"].IsPressing) 
            {
                buttons["blue"].UnPress();
                AnalizeMove(3);
            }
        }

        if (Input.GetKey(KeyCode.Q) && !pressing) Leave();
    }

    private void Play()
    {
        if (!IsComplete && cpu_play)
        {
            cpu_play = false;
            float time = 0.5f;
            repeats.Add(colors[Random.Range(0, 4)]);

            repeats.ForEach(color => {
                StartCoroutine(ButtonLightOn(color, time));
                time += 0.5f; 
                
                StartCoroutine(ButtonLightOff(color, time));
                time += 0.5f;
            });

            Invoke("AllowPlay", time);
        }
    }

    private void AnalizeMove(int index)
    {
        if (!IsComplete && player_play && repeats.Count > 0)
        {
            if (repeats[repeats_index] == colors[index]) repeats_index++;
            else
            {
                Invoke("ClearGame",3f);
                error.Play();
            }
             

            if (repeats_index == 3)
            {
                FinishGame();
            }
            else if (repeats_index == repeats.Count)
            {
                player_play = false;
                cpu_play = true;
                repeats_index = 0;
                SetButtonsLight(false);
            }
        }
    }

    private void ClearGame()
    {
        repeats_index = 0;
        repeats.Clear();
        player_play = false;
        cpu_play = true;
        SetButtonsLight(false);
    }

    private void FinishGame()
    {
        complete = true;
        Leave();
        cajon.Setbloqueocajon(true);
        cajon.StartAnimation();
    }

    private IEnumerator ButtonLightOn(string button_index, float time)
    {
        yield return new WaitForSeconds(time);
        
        buttons[button_index].LightOn();
    }

    private IEnumerator ButtonLightOff(string button_index, float time)
    {
        yield return new WaitForSeconds(time);
        
        buttons[button_index].LightOff();
    }

    private void AllowPlay()
    {
        player_play = true;
        SetButtonsLight(true);
    }

    private void SetButtonsLight(bool active)
    {
        foreach (string color in colors)
        {
            buttons[color].LightOnPress = active;
        }
    }

    private void AllButtonsLightOff()
    {
        foreach (string color in colors)
        {
            buttons[color].LightOff();
        }
    }

    public override void Interact(PlayerBehavior player)
    {
        base.Interact(player);

        player.gameObject.SetActive(false);
        camera.SetActive(true);
        this.player = player.gameObject;
        ClearGame();
    }

    public void Leave()
    {
        CameraInteracion interacion = player.GetComponent<CameraInteracion>();
        
        interacion.IsThrowing = true;
        player.SetActive(true);
        camera.SetActive(false);
        player = null;
        StopAllCoroutines();
        AllButtonsLightOff();
    }

    public bool IsComplete
    {
        get { return complete; }
    }
}
