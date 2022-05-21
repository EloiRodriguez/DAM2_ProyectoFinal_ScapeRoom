using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirCerrarPuertaIzquierda : Interactable
{
    public Animator AnimPuertaIz;
    private GameObject item;
	public bool open;
    private bool PuertaDormitorio = false;
    private bool PuertaHabitacion1 = false;
    private bool PuertaHabitacion2 = false;

    public AudioSource puerta_cerrada;
    public AudioSource cerrar_puerta;
    public AudioSource abrir_puerta;

    public override void Interact(PlayerBehavior player)
    {
        Inventory inventory = player.GetInventory();
        item = inventory.GetSelectedItem();

        if (gameObject.tag == "Dormitorio")
        {
            if(item != null)
            {
                if (item.tag == "llave_dormitorio")
                {
                    PuertaDormitorio = true;
                    inventory.SelectedSlot.DropItem();
                    Destroy(item);
                }
            }
            
            if (PuertaDormitorio == true)
            {
                startAnimation();
            } else
            {
                StartCoroutine(PuertaCerrada());
            }

        }else if (gameObject.tag == "Habitacion1")
        {
            if(item != null)
            {
               if (item.tag == "llave_habitacion1")
                {
                    PuertaHabitacion1 = true;
                    inventory.SelectedSlot.DropItem();
                    Destroy(item);
                } 
            }

            if (PuertaHabitacion1 == true)
            {
                startAnimation();
            } else
            {
                StartCoroutine(PuertaCerrada());
            }

        }else if (gameObject.tag == "Habitacion2")
        {
            if(item != null)
            {
                if (item.tag == "llave_habitacion2")
                {
                    PuertaHabitacion2 = true;
                    inventory.SelectedSlot.DropItem();
                    Destroy(item);
                }
            }

            if (PuertaHabitacion2 == true)
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

    private void startAnimation()
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

        if(gameObject.tag == "Dormitorio")
        {
            abrir_puerta.Play();
        }else if (gameObject.tag == "Habitacion1")
        {
            abrir_puerta.Play();
        }else if (gameObject.tag == "Habitacion2")
        {
            abrir_puerta.Play();
        }
        
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator cerrarPuertaIz()
    {
        AnimPuertaIz.Play("Cerrar_PuertaIz");

        if(gameObject.tag == "Dormitorio")
        {
            cerrar_puerta.Play();
        }else if (gameObject.tag == "Habitacion1")
        {
            cerrar_puerta.Play();
        }else if (gameObject.tag == "Habitacion2")
        {
            cerrar_puerta.Play();
        }
        
        open = false;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator PuertaCerrada()
    {
        AnimPuertaIz.Play("Puerta_CerradaIz");

        if(gameObject.tag == "Dormitorio")
        {
            puerta_cerrada.Play();
        }else if (gameObject.tag == "Habitacion1")
        {
            puerta_cerrada.Play();
        }else if (gameObject.tag == "Habitacion2")
        {
            puerta_cerrada.Play();
        }
        
        yield return new WaitForSeconds(.5f);
    }

}
