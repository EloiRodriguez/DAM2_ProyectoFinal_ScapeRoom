using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject player, pause, hud;
    private bool pressing = false;
    private bool paused = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!pressing)
            {
                pressing = true;
                
                if (paused) UnPause();
                else Pause();
            }
        }
        else
        {
            pressing = false;
        }
    }

    public void Pause()
    {
        hud.SetActive(false);
        pause.SetActive(true);
        paused = true;
        Cursor.lockState = CursorLockMode.None;
        player.GetComponent<PlayerBehavior>().Freeze = true;

        //Time.timeScale = 0;
    }

    public void UnPause()
    {
        hud.SetActive(true);
        pause.SetActive(false);
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<PlayerBehavior>().Freeze = false;

        //Time.timeScale = 1;
    }

    public bool Paused 
    {
        get { return paused; }
        set { paused = value; }
    }
}
