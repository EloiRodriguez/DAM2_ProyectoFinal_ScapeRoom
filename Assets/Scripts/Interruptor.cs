using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : Interactable
{
    public GameObject secret_num1;
    public GameObject secret_num2;
    public GameObject secret_num3;
    public GameObject secret_num4;

    public GameObject Light1;
    public GameObject Light2;
    public GameObject Light3;
    public GameObject Light4;
    private bool open = true;

    private void Start()
    {
        secret_num1.SetActive(false);
        secret_num2.SetActive(false);
        secret_num3.SetActive(false);
        secret_num4.SetActive(false);

        Light1.GetComponent<Light>().color = Color.white;
        Light2.GetComponent<Light>().color = Color.white;
        Light3.GetComponent<Light>().color = Color.white;
        Light4.GetComponent<Light>().color = Color.white;
        Light4.GetComponent<Light>().intensity = 3f;
        Light4.GetComponent<Light>().range = 10f;
    }

    public override void Interact(PlayerBehavior player)
    {
        if (open == false)
        {
            secret_num1.SetActive(false);
            secret_num2.SetActive(false);
            secret_num3.SetActive(false);
            secret_num4.SetActive(false);

            Light1.GetComponent<Light>().color = Color.white;
            Light2.GetComponent<Light>().color = Color.white;
            Light3.GetComponent<Light>().color = Color.white;
            Light4.GetComponent<Light>().color = Color.white;
            Light4.GetComponent<Light>().intensity = 3f;
            Light4.GetComponent<Light>().range = 10f;

            open = true;
        }
        else
        {
            secret_num1.SetActive(true);
            secret_num2.SetActive(true);
            secret_num3.SetActive(true);
            secret_num4.SetActive(true);

            Light1.GetComponent<Light>().color = Color.yellow;
            Light1.GetComponent<Light>().intensity = 12f;

            Light2.GetComponent<Light>().color = Color.green;
            Light2.GetComponent<Light>().intensity = 7f;

            Light3.GetComponent<Light>().color = Color.blue;
            Light3.GetComponent<Light>().intensity = 6f;

            Light4.GetComponent<Light>().color = Color.magenta;
            Light4.GetComponent<Light>().intensity = 10f;
            Light4.GetComponent<Light>().range = 3.3f;

            open = false;
        } 
             
    }
}
