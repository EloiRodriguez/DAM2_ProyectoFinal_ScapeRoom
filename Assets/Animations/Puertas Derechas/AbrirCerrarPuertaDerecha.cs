using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirCerrarPuertaDerecha :  Interactable
{
    public Animator AnimPuertaD;
	public bool open;
    private bool PuertaBano = false;
    private bool PuertaSalida = false;

    public AudioSource puerta_cerrada;
    public AudioSource cerrar_puerta;
    public AudioSource abrir_puerta;

    public override void Interact(PlayerBehavior player)
    {
        if (gameObject.tag == "Bano")
        {
            if (PuertaBano == true)
            {
                startAnimation();
            } else
            {
                StartCoroutine(PuertaCerrada());
            }
        }else if (gameObject.tag == "Salida")
        {
            if (PuertaSalida == true)
            {
                startAnimation();
            } else
            {
                StartCoroutine(PuertaCerrada());
            }
        }
        else
        {
            startAnimation();
        }   
    }

    public void startAnimation()
    {
        if (open == false)
        {
            StartCoroutine(abrirPuertaD());
        }
        else
        {
            if (open == true)
            {
                if (gameObject.tag != "Salida")
                {
                   StartCoroutine(cerrarPuertaD()); 
                }
            }
        } 
    }

    IEnumerator abrirPuertaD()
    {
        AnimPuertaD.Play("Abrir_PuertaD");
        abrir_puerta.Play();
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator cerrarPuertaD()
    {
        AnimPuertaD.Play("Cerrar_PuertaD");
        cerrar_puerta.Play();
        open = false;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator PuertaCerrada()
    {
        AnimPuertaD.Play("Puerta_CerradaD");
        puerta_cerrada.Play();
        yield return new WaitForSeconds(.5f);
    }

    public void SetPuertaBano(bool bolean) => this.PuertaBano = bolean;

    public void SetPuertaSalida(bool bolean)
    {
        this.PuertaSalida = bolean;
    }

}
