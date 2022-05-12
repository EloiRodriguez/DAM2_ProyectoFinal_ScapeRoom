using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : Interactable
{
    public Texture icon;

    public override void Interact(PlayerBehavior player)
    {
        base.Interact(player);
        player.Pick(gameObject);        
    }
}
