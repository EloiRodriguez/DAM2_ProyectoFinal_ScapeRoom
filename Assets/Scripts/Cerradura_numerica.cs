using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cerradura_numerica :   Interactable
{
    private AudioSource sonido_boton;
    public AudioSource error_code;
    private GameObject texto;
    private TextMeshPro texto_mesh;
    public AbrirCerrarPuertaDerecha acpd;
    public AbrirCerrarCajon acc;
    public int value;
    public int acertijo; 
    
    private void Start() 
    {
        sonido_boton = gameObject.GetComponent<AudioSource>();
        texto = transform.parent.GetChild(0).GetChild(0).gameObject;
        texto_mesh = texto.GetComponent<TextMeshPro>();
    }

    public override void Interact(PlayerBehavior player)
    {
        if (gameObject.tag == "Tecla")
        {
            ButtonAction();
        }
        else if (gameObject.tag == "Boton_submit")
        {
            ButtonSubmitAction();
        }
        else if (gameObject.tag == "Boton_cancel")
        {
            ButtonCancelAction();
        }
    }


    public void ButtonAction()
    {       
        if (texto_mesh.text == "****")
        {
            texto_mesh.text = value.ToString();
        }else
        {
            if (texto_mesh.text.Length <= 3)
            {
                texto_mesh.text += value.ToString();
            }
        }
        
        StartCoroutine(PulsarBoton()); 
    }

    public void ButtonSubmitAction()
    {
        if (acertijo == 1)
        {
            if (texto_mesh.text == "5876")
            {
                acpd.SetPuertaBano(true);
                acpd.startAnimation();
            }
            else
            {
                texto_mesh.text = "****";
                error_code.Play();
            }
        }

        if (acertijo == 2)
        {
            if (texto_mesh.text == "8614")
            {
                acc.Setbloqueocajon(true);
                acc.StartAnimation();
            }
            else
            {
                texto_mesh.text = "****";
                error_code.Play();
            }
        }

        if (acertijo == 3)
        {
            if (texto_mesh.text == "9801")
            {
                acpd.SetPuertaSalida(true);
                acpd.startAnimation();
            }
            else
            {
                texto_mesh.text = "****";
                error_code.Play();
            }
        }
        
        StartCoroutine(PulsarBoton()); 
    }

    public void ButtonCancelAction()
    {
        texto_mesh.text = "****";
        
        StartCoroutine(PulsarBoton()); 
    }

    IEnumerator PulsarBoton()
    {
        sonido_boton.Play();
        yield return new WaitForSeconds(.5f);
    }
}
