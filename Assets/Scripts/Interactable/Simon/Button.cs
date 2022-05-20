using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{   
    public float lightIntensity = 2;
    private new Light light;
    private bool lightOnPress = true;
    private bool pressing = false;
    public AudioClip clip;
    private AudioSource source;

    private void Awake()
    {
        light = transform.GetChild(0).GetComponent<Light>();
        source = transform.GetComponent<AudioSource>();
        source.clip = clip;
    }

    public void LightOn()
    {
        light.intensity = lightIntensity;
        
        if (source.clip != null)
        {
            source.Play();
            Debug.Log(gameObject.name);
        }
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
        
        if (lightOnPress) LightOn();
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

    public bool LightOnPress
    {
        get { return lightOnPress; }
        set { lightOnPress = value; }
    }
}
