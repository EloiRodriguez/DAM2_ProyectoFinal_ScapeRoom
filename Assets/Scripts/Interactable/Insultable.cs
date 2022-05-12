using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insultable : Interactable
{
    public override void Interact(PlayerBehavior player)
    {
        base.Interact(player);

        Debug.Log("Perra");
    }
}
