using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Slot> slots;
    private int selected = 0;

    private void Awake()
    {
        slots = new List<Slot>();

        for (int i = 0; i < 4; i++)
        {
            slots.Add(transform.Find("slot" + (i + 1)).GetComponent<Slot>());
        }

        slots[selected].Active(true);
    }

    public int Selected
    {
        get { return selected; }
        
        set 
        { 
            int index = value;

            if (index < 0) index = 0;
            else if (index > 3) index = 3;

            selected = index;

            foreach (Slot slot in slots)
            {
                slot.Active(false);
            }

            slots[selected].Active(true);
        }
    }

    public Slot SelectedSlot
    {
        get { return slots[selected]; }
    }

    public bool SelectedEmpty()
    {
        if (slots[selected].Item == null) return true;
        else return false;
    }
}