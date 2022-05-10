using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirCerrarPuertaIzquierda : Interactable
{
    public Animator AnimPuertaIz;
	public bool open;

    public override void Interact(PlayerBehavior player)
    {
        if (open == false)
        {
            StartCoroutine(abrirPuertaIz());
        }
        else
        {
            if (open == true)
            {
                StartCoroutine(cerrarPuertaIz());
            }
        }    
    }

    IEnumerator abrirPuertaIz()
    {
        AnimPuertaIz.Play("Abrir_PuertaIz");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator cerrarPuertaIz()
    {
        AnimPuertaIz.Play("Cerrar_PuertaIz");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

}
