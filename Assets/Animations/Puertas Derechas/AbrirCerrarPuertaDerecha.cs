using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirCerrarPuertaDerecha :  Interactable
{
    public Animator AnimPuertaD;
	public bool open;

    public override void Interact(PlayerBehavior player)
    {
        if (open == false)
        {
            StartCoroutine(abrirPuertaD());
        }
        else
        {
            if (open == true)
            {
                StartCoroutine(cerrarPuertaD());
            }
        }    
    }

    IEnumerator abrirPuertaD()
    {
        AnimPuertaD.Play("Abrir_PuertaD");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator cerrarPuertaD()
    {
        AnimPuertaD.Play("Cerrar_PuertaD");
        open = false;
        yield return new WaitForSeconds(.5f);
    }

}
