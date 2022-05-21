using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirCerrarCajon : Interactable
{
    public Animator AnimCajon;
    private bool bloqueocajon = false;
    private AudioSource cajon_cerrado;
    public bool open;

    private void Start()
    {
        cajon_cerrado = transform.GetComponent<AudioSource>();
    }

    public override void Interact(PlayerBehavior player)
    {
        if(gameObject.tag == "cajoncerrado")
        {
            if (bloqueocajon == true)
            {
                StartAnimation();
            }else
            {
                cajon_cerrado.Play();
            }
            
        }else
        {
           StartAnimation();
        }
             
    }

    public void StartAnimation()
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

    public void Setbloqueocajon(bool bolean) => this.bloqueocajon = bolean;

}
