using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{   
    public float lightIntensity = 2;
    private new Light light;

    private bool pressing = false;

    private void Awake()
    {
        light = transform.GetChild(0).GetComponent<Light>();
    }

    public void LightOn()
    {
        light.intensity = lightIntensity;
    }

    public void LightOff()
    {
        light.intensity = 0;
    }

    public void Press()
    {
        Vector3 current_position = transform.localPosition;
        
        current_position.y -= 0.2f;
        
        transform.localPosition = current_position;

        pressing = true;
        
        LightOn();
    }

    public void UnPress()
    {
        Vector3 current_position = transform.localPosition;
        
        current_position.y += 0.2f;
        
        transform.localPosition = current_position;
        
        pressing = false;

        LightOff();
    }

    public bool IsPressing
    {
        get { return pressing; }
    }
}
