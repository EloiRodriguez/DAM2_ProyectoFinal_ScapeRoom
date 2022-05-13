using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirCerrarCajon : Interactable
{
    public Animator AnimCajon;
    public bool open;

    public override void Interact(PlayerBehavior player)
    {
        if (open == false)
        {
            StartCoroutine(abrirCajon());
        }
        else
        {
            if (open == true)
            {
                StartCoroutine(cerrarCajon());
            }
        }     
    }

    IEnumerator abrirCajon()
    {
        AnimCajon.Play("Abrir_Cajon");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator cerrarCajon()
    {
        AnimCajon.Play("Cerrar_Cajon");
        open = false;
        yield return new WaitForSeconds(.5f);
    }

}
