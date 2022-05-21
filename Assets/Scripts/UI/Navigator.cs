using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour
{
    public string reference = "Scene";
    public GameObject selfScreen, referenceScreen;
    public bool SceneNavigation = false;

    public void Navigate()
    {
        if (reference != null && reference != "")
        {
            if (SceneNavigation)
            {
                SceneManager.LoadScene(reference);
            }
            else 
            {
                ChangeScreen();
            }
        }
    }

    private void ChangeScreen()
    {
        selfScreen.SetActive(false);
        referenceScreen.SetActive(true);        
    }
}
