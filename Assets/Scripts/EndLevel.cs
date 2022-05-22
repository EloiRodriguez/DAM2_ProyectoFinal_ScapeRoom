using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{   
    public GameObject pauseManager, endMenu, player;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (pauseManager != null) pauseManager.SetActive(false);
            if (player != null) player.GetComponent<PlayerBehavior>().Freeze = true;
            if (endMenu != null) endMenu.SetActive(true);
            
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
